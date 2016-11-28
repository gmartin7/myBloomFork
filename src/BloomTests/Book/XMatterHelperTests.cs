﻿using System.IO;
using System.Xml;
using Bloom;
using Bloom.Book;
using NUnit.Framework;

using SIL.IO;

namespace BloomTests.Book
{
	[TestFixture]
	public class XMatterHelperTests
	{
		private HtmlDom _dom;
		private DataSet _dataSet;

		[SetUp]
		public void Setup()
		{
			_dom = new HtmlDom(@"<html><head> <link href='file://blahblah\\a5portrait.css' type='text/css' /></head><body><div id='bloomDataDiv'></div><div id ='firstPage' class='bloom-page'>1st page</div></body></html>");
			_dataSet = new DataSet();
			_dataSet.WritingSystemAliases.Add("V","xyz");
			_dataSet.WritingSystemAliases.Add("N1", "fr");
			_dataSet.WritingSystemAliases.Add("N2", "en");
		}
		private XMatterHelper CreatePaperSaverHelper(string xmatterName="PaperSaver")
		{
			if(xmatterName == "PaperSaver")
				xmatterName = "Factory";

			var factoryXMatter = BloomFileLocator.GetInstalledXMatterDirectory();
			return new XMatterHelper(_dom, xmatterName, new FileLocator(new string[] { factoryXMatter }));
		}

		[Test]
		public void PathToXMatterHtml_AllDefaults_Correct()
		{
			string pathToXMatterHtml = CreatePaperSaverHelper().PathToXMatterHtml;
			Assert.IsTrue(File.Exists(pathToXMatterHtml), pathToXMatterHtml);
		}


		[Test]
		public void GetStyleSheetFileName_AllDefaults_Correct()
		{
			Assert.AreEqual("Factory-XMatter.css",CreatePaperSaverHelper().GetStyleSheetFileName());
		}

		[Test]
		public void InjectXMatter_AllDefaults_Inserts3PagesBetweenDataDivAndFirstPage()
		{
			CreatePaperSaverHelper().InjectXMatter(_dataSet.WritingSystemAliases, Layout.A5Portrait);
			AssertThatXmlIn.Dom(_dom.RawDom).HasSpecifiedNumberOfMatchesForXpath("//body/div[1][@id='bloomDataDiv']", 1);
			AssertThatXmlIn.Dom(_dom.RawDom).HasSpecifiedNumberOfMatchesForXpath("//body/div[2][contains(@class,'cover')]", 1);
			AssertThatXmlIn.Dom(_dom.RawDom).HasSpecifiedNumberOfMatchesForXpath("//body/div[3][contains(@class,'credits')]", 1);
			AssertThatXmlIn.Dom(_dom.RawDom).HasSpecifiedNumberOfMatchesForXpath("//body/div[4][contains(@class,'titlePage')]", 1);
			AssertThatXmlIn.Dom(_dom.RawDom).HasSpecifiedNumberOfMatchesForXpath("//body/div[5][@id='firstPage']", 1);
			AssertThatXmlIn.Dom(_dom.RawDom).HasSpecifiedNumberOfMatchesForXpath("//body/div[6][contains(@class,'bloom-backMatter')]", 1);
			AssertThatXmlIn.Dom(_dom.RawDom).HasSpecifiedNumberOfMatchesForXpath("//body/div[7][contains(@class,'bloom-backMatter')]", 1);
		}


		/// <summary>
		/// Initially, we were re-using "74731b2d-18b0-420f-ac96-6de20f659810" for every book,
		/// making the htmlthumbnailer's caching system totally messed up.
		/// </summary>
		[Test]
		public void InjectXMatter_AllDefaults_FirstPageHasNewIdInsteadOfCopying()
		{
			CreatePaperSaverHelper().InjectXMatter(_dataSet.WritingSystemAliases, Layout.A5Portrait);
			var id1 = ((XmlElement) _dom.SelectSingleNode("//div[contains(@class,'cover')]")).GetAttribute("id");
			Setup(); //reset for another round
			CreatePaperSaverHelper().InjectXMatter(_dataSet.WritingSystemAliases, Layout.A5Portrait);
			var id2 = ((XmlElement)_dom.SelectSingleNode("//div[contains(@class,'cover')]")).GetAttribute("id");

			Assert.AreNotEqual(id1,id2);
		}

		[Test]
		public void InjectXMatter_SpanWithNameOfLanguage2_GetsLang()
		{
			var frontMatterDom = new XmlDocument();
			frontMatterDom.LoadXml(@"<html><head> <link href='file://blahblah\\a5portrait.css' type='text/css' /></head><body>
						 <div class='bloom-page cover coverColor bloom-frontMatter' data-page='required'>
						 <span data-collection='nameOfLanguage' lang='N2'  class=''>{Regional}</span>
						</div></body></html>");
			var helper = CreatePaperSaverHelper();
			helper.XMatterDom = frontMatterDom;

			helper.InjectXMatter( _dataSet.WritingSystemAliases, Layout.A5Portrait);
			AssertThatXmlIn.Dom(_dom.RawDom).HasSpecifiedNumberOfMatchesForXpath("//div/span[@lang='en']", 1);
			//NB: it's not this class's job to actually fill in the value (e.g. English, in this case). Just to set it up so that a future process will do that.
		}

		[Test]
		public void InjectXMatter_HasBackMatter_BackMatterInjectedAtEnd()
		{
			var xMatterDom = new XmlDocument();
			xMatterDom.LoadXml(@"<html><head> <link href='file://blahblah\\a5portrait.css' type='text/css' /></head><body>
						 <div class='bloom-page cover coverColor bloom-frontMatter' data-page='required'>
						 <span data-collection='nameOfLanguage' lang='N2'  class=''>{Regional}</span>
						</div>
						<div class='bloom-page cover coverColor bloom-backMatter insideBackCover' data-page='required'>
						 <span data-collection='nameOfLanguage' lang='N2'  class=''>{Regional}</span>
						</div>
						<div class='bloom-page cover coverColor bloom-backMatter outsideBackCover' data-page='required'>
						 <span data-collection='nameOfLanguage' lang='N2'  class=''>{Regional}</span>
						</div>
						</body></html>");
			var helper = CreatePaperSaverHelper();
			helper.XMatterDom = xMatterDom;

			helper.InjectXMatter(_dataSet.WritingSystemAliases, Layout.A5Portrait);
			AssertThatXmlIn.Dom(_dom.RawDom).HasSpecifiedNumberOfMatchesForXpath("//body/div[1][@id='bloomDataDiv']", 1);
			AssertThatXmlIn.Dom(_dom.RawDom).HasSpecifiedNumberOfMatchesForXpath("//body/div[2][contains(@class,'cover')]", 1);
			AssertThatXmlIn.Dom(_dom.RawDom).HasSpecifiedNumberOfMatchesForXpath("//body/div[3][@id='firstPage']", 1);
			AssertThatXmlIn.Dom(_dom.RawDom).HasSpecifiedNumberOfMatchesForXpath("//body/div[4][contains(@class,'bloom-backMatter')]", 1);
			AssertThatXmlIn.Dom(_dom.RawDom).HasSpecifiedNumberOfMatchesForXpath("//body/div[5][contains(@class,'bloom-backMatter')]", 1);
			//NB: it's not this class's job to actually fill in the value (e.g. English, in this case). Just to set it up so that a future process will do that.
		}


		[Test]
		public void SuperPaperSaver_BookRequiresFacingPages_FlyleafInserted()
		{
			RunHelperForBookStartingWithSpread("SuperPaperSaver");
			AssertThatXmlIn.Dom(_dom.RawDom).HasSpecifiedNumberOfMatchesForXpath("//body/div[3][@id and contains(@class, 'bloom-flyleaf')]", 1);
		}

		[Test]
		public void Traditional_BookRequiresFacingPages_FlyleafInserted()
		{
			RunHelperForBookStartingWithSpread("Traditional");
			AssertThatXmlIn.Dom(_dom.RawDom).HasSpecifiedNumberOfMatchesForXpath("//body/div[5][contains(@class, 'bloom-flyleaf')]", 1);
		}

		[Test]
		public void PaperSaver_BookDoesNotRequireFacingPages_FlyleafNotInserted()
		{
			CreatePaperSaverHelper().InjectXMatter(_dataSet.WritingSystemAliases, Layout.A5Portrait);
			AssertThatXmlIn.Dom(_dom.RawDom).HasSpecifiedNumberOfMatchesForXpath("//body/div[contains(@class, 'bloom-flyleaf')]", 0);
		}

		//for paper saver, we already place the first book page facing the second, so we should not insert a flyleaf
		[Test]
		public void SuperPaperSaver_BookRequiresFacingPages_FlyleafNotInserted()
		{
			RunHelperForBookStartingWithSpread("PaperSaver");
			AssertThatXmlIn.Dom(_dom.RawDom).HasSpecifiedNumberOfMatchesForXpath("//body/div[contains(@class, 'bloom-flyleaf')]", 0);
		}

		private void RunHelperForBookStartingWithSpread(string xmatterPackName)
		{
			_dom = new HtmlDom(@"<html><body>
									 <div class='bloom-page' data-page='required spread-start'>
									 <span data-collection='nameOfLanguage' lang='N2'  class=''>{Regional}</span>
									</div>
									</body></html>");

			var helper = CreatePaperSaverHelper(xmatterPackName);
			helper.InjectXMatter(_dataSet.WritingSystemAliases, Layout.A5Portrait);
		}



		//		TODO: at the moment, we'd have to creat a whole xmatter folder
		/// <summary>
		//		/// NB: It's not clear what the behavior should eventually be... how do we know it isn't supposed to be in english?
		//		/// But for now, this gives us the behavior we want on the title page
		//		/// </summary>
		//		[Test]
		//		public void CreateBookOnDiskFromTemplate_HasParagraphMarkedV_ConvertsToVernacular()//??????????????
		//		{
		//			_starter.TestingSoSkipAddingXMatter = true;
		//			var body = @"<div class='bloom-page'>
		//                        <p id='bookTitle' lang='en' data-book='bookTitle'>Book Title</p>
		//                    </div>";
		//			string sourceTemplateFolder = GetShellBookFolder(body);
		//			var path = GetPathToHtml(_starter.CreateBookOnDiskFromTemplate(sourceTemplateFolder, _projectFolder.Path));
		//			AssertThatXmlIn.HtmlFile(path).HasSpecifiedNumberOfMatchesForXpath("//p[@lang='xyz']", 1);
		//		}


		//TODO: tests with a different paper size

		//TODO: tests with a custom pack, with images

		//TODO: test with custom pack and a paper size/orientation that we're missing a css for, should fall back to factory

		//TODO: test with defaults but a paper size/orientation that we're missing a css for, should warn and use a5portrait
	}
}
