﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bloom.Book;
using Bloom.Collection;
using CommandLine;
using SIL.IO;
using SIL.Progress;

namespace Bloom.CLI
{
	/// <summary>
	/// This command needs to
	/// * set the xmatter to something appropriate
	/// * set the layout to something appropriate
	/// * set the L1, L2, and L3 languages
	/// * Spread all the above around, as if the book had been loaded in Bloom
	/// * Make sure it has all the needed stylesheets
	/// </summary>
	class HydrateBookCommand
	{
		public static int Handle(HydrateParameters options)
		{
			if(!Directory.Exists(options.Path))
			{
				Console.Error.WriteLine("Could not find "+options.Path);
				return 1;
			}
			Console.WriteLine("Starting Hydrating.");

			var layout = new Layout()
			{
				SizeAndOrientation = SizeAndOrientation.FromString(options.SizeAndOrientation)
			};
			var nameOfXMatterPack = "Video";

			var collectionSettings = new CollectionSettings()
			{
				XMatterPackName = "Video",
				Language1Iso639Code = options.VernacularIsoCode
				//			collectionSettings.Language2Iso639Code = options.NationalLanguage1IsoCode;
				//			collectionSettings.Language3Iso639Code = options.NationalLanguage2IsoCode;
			};

			XMatterPackFinder xmatterFinder = new XMatterPackFinder(new[] { BloomFileLocator.GetInstalledXMatterDirectory() });
			var locator = new BloomFileLocator(collectionSettings, xmatterFinder, ProjectContext.GetFactoryFileLocations(), ProjectContext.GetFoundFileLocations(), ProjectContext.GetAfterXMatterFileLocations());

			var bookInfo = new BookInfo(options.Path, true);
			var book = new Book.Book(bookInfo,new BookStorage(options.Path, locator, new BookRenamedEvent(), collectionSettings), null, collectionSettings, null, null, new BookRefreshEvent());

			//we might change this later, or make it optional, but for now, this will prevent surprises to processes
			//running this CLI... the folder name won't change out from under it.
			book.LockDownTheFileAndFolderName = true;

			book.SetLayout(layout);
			book.BringBookUpToDate(new NullProgress());
			Console.WriteLine("Finished Hydrating.");
			return 0;
		}
	}
}

// Used with https://github.com/gsscoder/commandline, which we get via nuget.
// (using the beta of commandline 2.0, as of Bloom 3.8)

[Verb("hydrate", HelpText = "Apply XMatter, Page Size/Orientation, and Languages. Used by automated converters and app makers.")]
public class HydrateParameters
{
	private string _sizeAndOrientation;

	[Option("bookpath", HelpText = "path to the book", Required = true)]
	public string Path { get; set; }

	// When a book is opened in a collection, Bloom gathers the vernacular, national, and regional languages
	// from the collection settings and makes changes to the html so that, for example, the current vernacular
	// shows on each page, rather than in the source bubbles. It does that by adding classes such as "content1".
	// The command being defined here can do that, too. This is needed for cases where, for example, the user
	// selects a book from BloomLibrary and wants to make an app out of it for his language. But his language
	// might not be the one that was "l1" when the book was uploaded. Using these parameters, the program making
	// him an app can specify that this language should be the l1.

	[Option("VernacularIsoCode", HelpText = "iso code of primary language", Required = true)]
	public string VernacularIsoCode { get; set; }

	[Option("NationalLanguage1IsoCode", HelpText = "iso code of secondary language", Default="", Required = false)]
		public string NationalLanguage1IsoCode { get; set; }

	[Option("NationalLanguage2IsoCode", HelpText = "iso code of tertiary language", Default = "", Required = false)]
		public string NationalLanguage2IsoCode { get; set; }
	

	[Option("preset", HelpText = "alternative to specifying layout and xmatter. Currently only supported value is 'app'", Required = true /*will be false when we implement the indivdual options below*/)]
	public string Preset { get; set; }


	[Option("sizeandorientation", HelpText = "desired size & orientation", Required = false)]
	public string SizeAndOrientation
	{
		get
		{
			if(string.IsNullOrEmpty(_sizeAndOrientation))
			{
				return "Device16x9Landscape";
			}

			return _sizeAndOrientation;
		}
		set { _sizeAndOrientation = value; }
	}

//	
//	[Option("xmatter", HelpText = "front/back matter pack to apply. E.g. 'Video', 'Factory'", Required = true)]
//	public string XMatter { get; set; }

	/*
	[Option("multilinguallevel", HelpText = "value of either 1, 2, or 3 (monolingual, bilingual, trilingual)", Required = false)]
	public int MultilingualLevel { get; set; }
	*/
}


//Enhance: someday we could gather more info. For now, we assume the caller
//wants to make an app. We assume Bloom can look inside the app to determine
//if a video-style or book-style xmatter is appropriate.