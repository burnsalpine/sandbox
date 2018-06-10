using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// Features to add
// Number of unique words

namespace textus
{
	class Program
	{
		static void Main(string[] args)
		{
            Console.WriteLine("####################################");
            Console.WriteLine("### Textus Textual Analyzer v1.0 ###");
            Console.WriteLine("####################################");

            // List available books
            Console.WriteLine("");
            Console.WriteLine("Books:");
            string[] BookList = Directory.GetFiles(@"D:\media\books"); 
            
            foreach (string Book in BookList)
            {
                Console.WriteLine(Book);
            }

            Console.WriteLine("");
            Console.WriteLine("Enter path to source text:");
			
            // Get path for source text
            string filename = Console.ReadLine();

			string inputString = File.ReadAllText(filename);
			
            Console.WriteLine("");
            Console.WriteLine("Loading " + filename + "...");

            var Encoding = GetEncoding(filename);

            Console.WriteLine("File Encoding: " + Encoding);

			// Convert text to lowercase
			inputString = inputString.ToLower();        

			// Strip unwanted characters from text
                Regex reg_exp = new Regex("[^a-zA-Z0-9]");
                inputString = reg_exp.Replace(inputString, " ");
			//string[] stripChars = { ";", ",", ".", "_", "^", "(", ")", "[", "]", "'", "?",
			//			"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "\n", "\t", "\r" };
			//foreach (string character in stripChars)
			//{
			//	inputString = inputString.Replace(character, "");
			//}
			
			// Convert to list of strings, delimiting on spaces
			List<string> wordList = inputString.Split(' ').ToList();
			
			// Create a new Dictionary object to store the words
			Dictionary<string, int> dictionary = new Dictionary<string, int>();

			// Loop over all over the words in our wordList...
			foreach (string word in wordList)
			{
				// If the length of the word is at least three letters...
				if (word.Length >= 5) 
				{
					// ...check if the dictionary already has the word.
					if ( dictionary.ContainsKey(word) )
					{
						// If we already have the word in the dictionary, increment the count of how many times it appears
						dictionary[word]++;
					}
					else
					{
						// Otherwise, if it's a new word then add it to the dictionary with an initial count of 1
						dictionary[word] = 1;
					}

				} // End of word length check

			} // End of loop over each word in our input

			// Create a dictionary sorted by: Word Frequency
			var sortedDict = (from entry in dictionary orderby entry.Value descending select entry).ToDictionary(pair => pair.Key, pair => pair.Value);

            // Write Output
            Console.WriteLine("Performing textual analysis...");
            Console.WriteLine("Word Count: " + wordList.Count);

            // Number of unique words
            var UniqueWords = (
                from string word in wordList
                orderby word select word).Distinct();

            string[] result = UniqueWords.ToArray();

            Console.WriteLine("Unique Word Count: " + result.Length);

            var finalValue = wordList.OrderByDescending(n=> n.Length).First();
            Console.WriteLine("Longest Word: " + finalValue);

			// Loop through the sorted dictionary and output the top 10 most frequently occurring words
			int count = 1;
			Console.WriteLine("Most Frequent Words: ");
			Console.WriteLine();
			foreach (KeyValuePair<string, int> pair in sortedDict)
			{
				// Output the most frequently occurring words and the associated word counts
				Console.WriteLine(count + "\t" + pair.Key + "\t" + pair.Value);
				count++;

				// Only display the top 10 words then break out of the loop!
				if (count > 25)
				{
					break;
				}
			}

        Console.WriteLine("Analysis Complete.");

		} // End of Main method

            // Determine file encoding
            public static Encoding GetEncoding(string filename)
            {
                // Read the BOM
                var bom = new byte[4];
                using (var file = new FileStream(filename, FileMode.Open, FileAccess.Read))
                {
                    file.Read(bom, 0, 4);
                }

                // Analyze the BOM
                if (bom[0] == 0x2b && bom[1] == 0x2f && bom[2] == 0x76) return Encoding.UTF7;
                if (bom[0] == 0xef && bom[1] == 0xbb && bom[2] == 0xbf) return Encoding.UTF8;
                if (bom[0] == 0xff && bom[1] == 0xfe) return Encoding.Unicode; //UTF-16LE
                if (bom[0] == 0xfe && bom[1] == 0xff) return Encoding.BigEndianUnicode; //UTF-16BE
                if (bom[0] == 0 && bom[1] == 0 && bom[2] == 0xfe && bom[3] == 0xff) return Encoding.UTF32;
                return Encoding.ASCII;
            }

	} // End of Program class

} // End of namespace