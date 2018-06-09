using System;
using System.Net;
using System.Net.Http;
using System.IO;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Search
{
    class Program
    {
        /// <summary>
        /// Bing search API Key
        /// </summary>
        private const string accessKey = "14665e7cdbc24c67a42a581407e11902";

        /// <summary>
        /// Bing search API URI
        /// </summary>
        private const string uriBase = "https://api.cognitive.microsoft.com/bing/v7.0/search";

        // After scanning through this - there are few things to address here. Generally you don't want to use WebRequest any more (it's pretty outdated) - and you're doing some things that are generally considered a no-no when it comes to async :)
        // I'll just include a loop in the first example, and then kick a few more things at you in a later PR.
        static void Main(string[] args)
        {
            Console.WriteLine("Searchy! v1.0 - Powered by Bing");
            Console.WriteLine("Enter search term");

            // Get search term from user
            var SearchTerm = Console.ReadLine();

            // Construct the URI of the search request
            var uriQuery = uriBase + "?q=" + Uri.EscapeDataString(SearchTerm);

            // Create the Web request
            Console.WriteLine("Fetching results...");
            WebRequest request = HttpWebRequest.Create(uriQuery);

            // Set the access key header
            request.Headers["Ocp-Apim-Subscription-Key"] = accessKey;

            // Send the request
            HttpWebResponse response = (HttpWebResponse)request.GetResponseAsync().Result;

            // Read the response
            string json = new StreamReader(response.GetResponseStream()).ReadToEnd();

            // TONY: This is kind of a hard way to read this JSON - I'll leave this one in, and then show you another example of how to do some of this.
            JObject ParsedJSON = JObject.Parse(json);
            JObject resultsContainer = JObject.Parse(ParsedJSON["webPages"].ToString());

            var estimatedResults = (string)(resultsContainer["totalEstimatedMatches"]);
            var searchResults = (JArray)resultsContainer["value"];

            // Loop through each item in SearchResults and output details
            Console.WriteLine($"Showing  {searchResults.Count} results out of an estimated {estimatedResults}");

            // Method one: Use a "foreach" loop to iterate over an enumerable - in this loop, every iteration "item" will be replaced with the value from the loop.
            Console.WriteLine("===== METHOD ONE =====");
            int i = 0;
            foreach (var item in searchResults)
            {
                // Increment our counter so we can maintain a result count.
                i += 1;

                // Output (using the clever $"" dotnet formatting syntax) the count and then the url. This is equivalent to `i + " - " + item["url"]`.
                Console.WriteLine($"{i} - {item["url"]}");
                Console.WriteLine("    " + item["snippet"]);
            }
            Console.WriteLine();

            // Method two: Use a for loop, more indexing, perhpas a bit clearer in this context.
            Console.WriteLine("===== METHOD TWO=====");
            // Probably unneccessary, but back before the dawn of time I was taught to pre-compute things where possible, rather than leaving the accessor in the loop (which will get called multiple times)
            int resultCount = searchResults.Count;
            // Note: I'm using `j` because I already used `i` in this scope above. If I was doing this for real, I would probably do some refactoring.
            for (int j = 0; j < resultCount; ++j)
            {
                // Save off the item we're accessing to avoid mistakes throughout the rest of the loop, also improves readability.
                var item = searchResults[j];

                // FROM HERE the code is nearly identical, with the exception of needing to increase j by one to make our numbering prettier.
                // Output (using the clever $"" dotnet formatting syntax) the count and then the url. This is equivalent to `i + " - " + item["url"]`.
                Console.WriteLine($"{j + 1} - {item["url"]}");
                Console.WriteLine("    " + item["snippet"]);
            }
            Console.WriteLine();
        }

    }

}



