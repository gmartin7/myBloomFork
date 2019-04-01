﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Bloom.Book;
using System.Windows.Forms;
using System.Xml;
using SIL.Xml;
using Bloom.Publish.Epub;
using SIL.Progress;

namespace Bloom.Publish
{
	public class PublishHelper : IDisposable
	{
		public Control ControlForInvoke { get; set; }
		Browser _browser = new Browser();
		// The only reason this isn't just ../* is performance. We could change it.  It comes from the need to actually
		// remove any elements that the style rules would hide, because epub readers ignore visibility settings.
		private const string kSelectThingsThatCanBeHidden = ".//div | .//img";

		public void RemoveUnwantedContent(HtmlDom dom, Book.Book book, EpubMaker epubMaker = null)
		{
			// Removing unwanted content involves a real browser really navigating. I'm not sure exactly why,
			// but things freeze up if we don't do it on the UI thread.
			if (ControlForInvoke != null)
			{
				// Linux/Mono can choose a toast as the ActiveForm.  When it closes, bad things can happen
				// trying to use it to Invoke.
				if (ControlForInvoke.IsDisposed)
					ControlForInvoke = Form.ActiveForm;
				ControlForInvoke.Invoke((Action)(() => RemoveUnwantedContentInternal(dom, book, epubMaker)));
			}
			else
				RemoveUnwantedContentInternal(dom, book, epubMaker);
		}

		private void RemoveUnwantedContentInternal(HtmlDom dom, Book.Book book, EpubMaker epubMaker)
		{
			// The ControlForInvoke can be null for tests.  If it's not null, we better not need an Invoke!
			Debug.Assert(ControlForInvoke==null || !ControlForInvoke.InvokeRequired); // should be called on UI thread.
			Debug.Assert(dom != null && dom.Body != null);

			// Collect all the page divs.
			var pageElts = new List<XmlElement>();
			if (epubMaker != null)
			{
				pageElts.Add((XmlElement)dom.Body.FirstChild);	// already have a single-page dom prepared for export
			}
			else
			{
				foreach (XmlElement page in book.GetPageElements())
					pageElts.Add(page);
			}

			HtmlDom displayDom = null;
			foreach (XmlElement page in pageElts)
			{
				EnsureAllThingsThatCanBeHiddenHaveIds(page);
				if (displayDom == null)
				{
					displayDom = book.GetHtmlDomWithJustOnePage(page);
				}
				else
				{
					var pageNode = displayDom.RawDom.ImportNode(page, true);
					displayDom.Body.AppendChild(pageNode);
				}
			}
			if (displayDom == null)
				return;
			if (epubMaker != null)
				epubMaker.AddEpubVisibilityStylesheetAndClass(displayDom);

			_browser.NavigateAndWaitTillDone(displayDom, 10000, "publish");

			var toBeDeleted = new List<XmlElement> ();
			// Deleting the elements in place during the foreach messes up the list and some things that should be deleted aren't
			// (See BL-5234). So we gather up the elements to be deleted and delete them afterwards.
			foreach (XmlElement page in pageElts)
			{
				foreach (XmlElement elt in page.SafeSelectNodes(kSelectThingsThatCanBeHidden))
				{
					if (!IsDisplayed(elt))
						toBeDeleted.Add(elt);
				}
				foreach (var elt in toBeDeleted)
				{
					elt.ParentNode.RemoveChild(elt);
				}
				RemoveTempIds(page); // don't need temporary IDs any more.
				toBeDeleted.Clear();
			}

			// Remove any left-over bubbles
			foreach (XmlElement elt in dom.RawDom.SafeSelectNodes("//label"))
			{
				if (HasClass(elt, "bubble"))
					elt.ParentNode.RemoveChild(elt);
			}
			// Remove page labels and descriptions.  Also remove pages (or other div elements) that users have
			// marked invisible.  (The last mimics the effect of bookLayout/languageDisplay.less for editing
			// or PDF published books.)
			foreach (XmlElement elt in dom.RawDom.SafeSelectNodes("//div"))
			{
				if (HasClass (elt, "pageLabel"))
					elt.ParentNode.RemoveChild (elt);
				if (HasClass (elt, "pageDescription"))
					elt.ParentNode.RemoveChild (elt);
				// REVIEW: is this needed now with the new strategy?
				if (HasClass (elt, "bloom-editable") && HasClass (elt, "bloom-visibility-user-off"))
					elt.ParentNode.RemoveChild (elt);
			}
			// Our recordingmd5 attribute is not allowed
			foreach (XmlElement elt in HtmlDom.SelectAudioSentenceElementsWithRecordingMd5(dom.RawDom.DocumentElement))
			{
				elt.RemoveAttribute ("recordingmd5");
			}
			// Users should not be able to edit content of published books
			foreach (XmlElement elt in dom.RawDom.SafeSelectNodes ("//div[@contenteditable]")) {
				elt.RemoveAttribute ("contenteditable");
			}

			foreach (var div in dom.Body.SelectNodes("//div[@role='textbox']").Cast<XmlElement>())
			{
				div.RemoveAttribute("role");				// this isn't an editable textbox in an ebook
				div.RemoveAttribute("aria-label");			// don't want this without a role
				div.RemoveAttribute("spellcheck");			// too late for spell checking in an ebook
				div.RemoveAttribute("content-editable");	// too late for editing in an ebook
			}

			// Clean up img elements (BL-6035/BL-6036)
			foreach (var img in dom.Body.SelectNodes("//img").Cast<XmlElement>())
			{
				// Ensuring a proper alt attribute is handled elsewhere
				var src = img.GetOptionalStringAttribute("src", null);
				if (String.IsNullOrEmpty(src))
				{
					// If the image file doesn't exist, we want to find out about it.  But if there is no
					// image file, epubcheck complains and it doesn't do any good anyway.
					img.ParentNode.RemoveChild(img);
				}
				else
				{
					var parent = img.ParentNode as XmlElement;
					parent.RemoveAttribute("title");	// We don't want this in published books.
					img.RemoveAttribute("title");	// We don't want this in published books.  (probably doesn't exist)
					img.RemoveAttribute("type");	// This is invalid, but has appeared for svg branding images.
				}
			}

			if (epubMaker != null)
			{
				// epub-check doesn't like these attributes (BL-6036).  I suppose BloomReader might find them useful.
				foreach (var div in dom.Body.SelectNodes("//div[contains(@class, 'split-pane-component-inner')]").Cast<XmlElement>())
				{
					div.RemoveAttribute("min-height");
					div.RemoveAttribute("min-width");
				}
			}

			// These elements are inserted and supposedly removed by the ckeditor javascript code.
			// But at least one book created by our test team still has one output to an epub.  If it
			// exists, it probably has a style attribute (position:fixed) that epubcheck won't like.
			// (fixed position way off the screen to hide it)
			foreach (var div in dom.Body.SelectNodes("//*[@data-cke-hidden-sel]").Cast<XmlElement>())
			{
				div.ParentNode.RemoveChild(div);
			}
		}

		private bool IsDisplayed(XmlElement elt)
		{
			var id = elt.Attributes["id"].Value;
			var display = _browser.RunJavaScript ("getComputedStyle(document.getElementById('" + id + "'), null).display");
			return display != "none";
		}

		internal const string kTempIdMarker = "PublishTempIdXXYY";
		private static int s_count = 1;
		public static void EnsureAllThingsThatCanBeHiddenHaveIds(XmlElement pageElt)
		{
			foreach (XmlElement elt in pageElt.SafeSelectNodes(kSelectThingsThatCanBeHidden))
			{
				if (elt.Attributes["id"] != null)
					continue;
				elt.SetAttribute("id", kTempIdMarker + s_count++);
			}
		}

		public static void RemoveTempIds(XmlElement pageElt)
		{
			foreach (XmlElement elt in pageElt.SafeSelectNodes(kSelectThingsThatCanBeHidden))
			{
				if (elt.Attributes["id"] != null && elt.Attributes["id"].Value.StartsWith(kTempIdMarker))
					elt.RemoveAttribute("id");
			}
		}

		public static bool HasClass(XmlElement elt, string className)
		{
			if (elt == null)
				return false;
			var classAttr = elt.Attributes ["class"];
			if (classAttr == null)
				return false;
			return ((" " + classAttr.Value + " ").Contains (" " + className + " "));
		}

		/// <summary>
		/// tempFolderPath is where to put the book. Note that a few files (e.g., customCollectionStyles.css)
		/// are copied into its parent in order to be in the expected location relative to the book,
		/// so that needs to be a folder we can write in.
		/// </summary>
		/// <param name="book"></param>
		/// <param name="bookServer"></param>
		/// <param name="tempFolderPath"></param>
		/// <returns></returns>
		public static Book.Book MakeDeviceXmatterTempBook(Book.Book book, BookServer bookServer, string tempFolderPath)
		{
			BookStorage.CopyDirectory(book.FolderPath, tempFolderPath);
			// We will later copy these into the book's own folder and adjust the style sheet refs.
			// But in some cases (at least, where the book's primary stylesheet does not provide
			// the information SizeAndOrientation.GetLayoutChoices() is looking for), we need them
			// to exist in the originally expected lcoation: the book's parent directory for
			// BringBookUpToDate to succeed.
			BookStorage.CopyCollectionStyles(book.FolderPath, Path.GetDirectoryName(tempFolderPath));
			var bookInfo = new BookInfo(tempFolderPath, true);
			bookInfo.XMatterNameOverride = "Device";
			var modifiedBook = bookServer.GetBookFromBookInfo(bookInfo);
			modifiedBook.BringBookUpToDate(new NullProgress(), true);
			modifiedBook.AdjustCollectionStylesToBookFolder();
			modifiedBook.RemoveNonPublishablePages();
			modifiedBook.Save();
			modifiedBook.Storage.UpdateSupportFiles();
			// Copy the possibly modified stylesheets after UpdateSupportFiles so that they don't
			// get replaced by the factory versions.
			BookStorage.CopyCollectionStyles(book.FolderPath, tempFolderPath);
			return modifiedBook;
		}

		#region IDisposable Support
		// This code added to correctly implement the disposable pattern.
		private bool _isDisposed = false; // To detect redundant calls

		protected virtual void Dispose(bool disposing)
		{
			if (!_isDisposed)
			{
				if (disposing)
				{
					if (_browser != null)
					{
						_browser.Invoke((Action) (() => _browser.Dispose()));
					}

					_browser = null;
				}
				_isDisposed = true;
			}
		}

		public void Dispose()
		{
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			Dispose(true);
		}
		#endregion

	}
}