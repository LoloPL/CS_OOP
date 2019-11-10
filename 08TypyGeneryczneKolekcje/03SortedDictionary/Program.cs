﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03SortedDictionary
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create a new SortedDictionary of strings, with string keys 
			// and a case-insensitive comparer for the current culture.
			SortedDictionary<string, string> openWith =
				new SortedDictionary<string, string>(
					StringComparer.CurrentCultureIgnoreCase)
				{
					{"txt", "notepad.exe"}, 
					{"bmp", "paint.exe"}, 
					{"DIB", "paint.exe"}, 
					{"rtf", "wordpad.exe"}
				};

			// Add some elements to the dictionary.

			// Try to add a fifth element with a key that is the same 
			// except for case; this would be allowed with the default
			// comparer.
			try
			{
				openWith.Add("BMP", "paint.exe");
			}
			catch (ArgumentException)
			{
				Console.WriteLine("\nBMP is already in the dictionary.");
			}

			// List the contents of the sorted dictionary.
			Console.WriteLine();
			foreach (var kvp in openWith)
			{
				Console.WriteLine($"Key = {kvp.Key}, Value = {kvp.Value}");
			}
		}
	}
}
