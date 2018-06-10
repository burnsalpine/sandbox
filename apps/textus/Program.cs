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
            Console.WriteLine("### Textus Textual Analyzer v1.0 ###");;
            Console.WriteLine("Enter path to source text:");
			
            // Get path for source text
            string filename = Console.ReadLine();

			string inputString = File.ReadAllText(filename);
			
            Console.WriteLine("Loading " + filename + "...");

			// Convert text to lowercase
			inputString = inputString.ToLower();        

			// Strip unwanted characters from text
			string[] stripChars = { ";", ",", ".", "_", "^", "(", ")", "[", "]", "'", "?",
						"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "\n", "\t", "\r" };
			foreach (string character in stripChars)
			{
				inputString = inputString.Replace(character, "");
			}
			
			// Convert to list of strings, delimiting on spaces
			List<string> wordList = inputString.Split(' ').ToList();

			// Define and remove stopwords
			string[] stopwords = new string[] { "and", "the", "she", "for", "this", "you", "but" };
			foreach (string word in stopwords)
			{
				// While there's still an instance of a stopword in the wordList, remove it.
				// If we don't use a while loop on this each call to Remove simply removes a single
				// instance of the stopword from our wordList, and we can't call Replace on the
				// entire string (as opposed to the individual words in the string) as it's
				// too indiscriminate (i.e. removing 'and' will turn words like 'bandage' into 'bdage'!)
				while ( wordList.Contains(word) )
				{
					wordList.Remove(word);
				}
			}
			
			// Create a new Dictionary object
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
			

		} // End of Main method

	} // End of Program class

} // End of namespace