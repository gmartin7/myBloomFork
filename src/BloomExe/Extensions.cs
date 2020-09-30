﻿using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Bloom.Api;

namespace Bloom
{
	public static class Extensions
	{
		[Obsolete("Use ToLocalhostForBloomPlayer() instead, which should happen more punctuation characters correctly.")]
		public static string ToLocalhost(this string fileName)
		{
			// don't do this if it is done already
			if (fileName.StartsWith(BloomServer.ServerUrlWithBloomPrefixEndingInSlash)) return fileName;

			return BloomServer.ServerUrlWithBloomPrefixEndingInSlash + fileName.EscapeCharsForHttp().Replace(System.IO.Path.DirectorySeparatorChar, '/');
		}

		private static readonly char[] kDirectorySeparators = new char[] { '\\', '/' };

		/// <summary>
		/// Returns a localhost URL to a file
		/// Honestly, I think this would be a good generic implementation of ToLocalhost,
		/// but that function name is already used with slightly different implementation and had 37 references,
		/// so I just made a new one rather than affect so many different places.
		/// It is preferable to use this version of the function unless you specifically need the result to be
		/// decoded by UnescapeCharsForHttp (which we hope to retire in version 5.0) and not by Uri.UnescapeDataString / JS's decodeUriComponent
		/// </summary>
		/// <param name="fileName">The raw filename used by the operating system</param>
		/// <returns>A well-formed, singly-encoded URL</returns>
		public static string ToLocalhostProperlyEncoded(this string fileName)
		{
			var escapedPathComponents = fileName.Split(kDirectorySeparators).Select(Uri.EscapeDataString);
			string escapedFileName = String.Join("/", escapedPathComponents);
			string url = BloomServer.ServerUrlWithBloomPrefixEndingInSlash + escapedFileName;
			return url;
		}

		public static string FromLocalhost(this string uri)
		{
			if (uri.StartsWith(BloomServer.ServerUrlWithBloomPrefixEndingInSlash))
				uri = uri.Substring(BloomServer.ServerUrlWithBloomPrefixEndingInSlash.Length).UnescapeCharsForHttp();
			return uri;
		}

		/// <summary>
		/// Escapes a number of characters that need it for our url/http processing.
		/// </summary>
		/// <remarks>
		/// Note that calls to EscapeCharsForHttp() must be matched by an equal number of
		/// subsequent calls to UnescapeCharsForHttp().  (Normally each is called once.)
		/// </remarks>
		public static string EscapeCharsForHttp(this string fileName)
		{
			fileName = fileName.Replace("%","%25");

			// BL-117, PH: With the newer xulrunner, javascript code with parenthesis in the URL is not working correctly.
			fileName = fileName.Replace("(", "%28").Replace(")", "%29");

			return fileName.Replace(":", "%3A").Replace("#","%23").Replace("?","%3F");
		}

		/// <summary>
		/// Remove the escaping of characters that need it for our url/http processing to restore
		/// a valid file pathname.
		/// </summary>
		/// <remarks>
		/// Note that calls to UnescapeCharsForHttp() must be matched by an equal number of
		/// previous calls to EscapeCharsForHttp().  (Normally each is called once.)
		/// </remarks>
		public static string UnescapeCharsForHttp(this string uri)
		{
			// Include the quoting for space in case someone wants to unescape a raw url string.
			return uri.Replace("%20", " ").Replace("%3A", ":").Replace("%23","#").Replace("%3F","?").Replace("%28","(").Replace("%29",")").Replace("%25","%");
		}

		public static int ToInt(this bool value)
		{
			if (value) return 1;
			return 0;
		}

		public static void AppendLineFormat(this StringBuilder sb, string format, params object[] args)
		{
			sb.AppendLine(string.Format(format, args));
		}

		public static void SizeTextRectangleToText(this ToolStripItemTextRenderEventArgs args)
		{
			var textSize = TextRenderer.MeasureText(args.Graphics, args.Text, args.TextFont);
			const int padding = 2;

			var rc = args.TextRectangle;
			var changed = false;

			// adjust the rectangle to fit the calculated text size
			if (rc.Width < textSize.Width + padding)
			{
				var diffX = textSize.Width + 2 - rc.Width;
				rc.X -= diffX / 2;
				rc.Width += diffX;
				changed = true;
			}

			if (rc.Height < textSize.Height + padding)
			{
				var diffY = textSize.Height + 2 - rc.Height;
				rc.Y -= diffY / 2;
				rc.Height += diffY;
				changed = true;
			}

			// if nothing changed, return now
			if (!changed) return;

			args.TextRectangle = rc;
		}
	}
}
