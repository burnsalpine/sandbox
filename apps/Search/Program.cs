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
        static void Main(string[] args)
        {
            Console.WriteLine("Searchy! v1.0 - Powered by Bing");;
            Console.WriteLine("Enter search term");

            // Get search term from user
            var SearchTerm = Console.ReadLine();
            
            // Bing search API Key
            const string accessKey = "14665e7cdbc24c67a42a581407e11902";

            // Bing search API URI
            const string uriBase = "https://api.cognitive.microsoft.com/bing/v7.0/search";

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

            // Parse the JSON into something more readable
            //var ParsedJson = JsonConvert.DeserializeObject(json);

            JObject ParsedJSON = JObject.Parse(json);
            JObject SearchResults = JObject.Parse(ParsedJSON["webPages"].ToString());
            
            // Loop through each item in SearchResults and output details
            foreach (var item in SearchResults)
            {
                Console.WriteLine(SearchResults["value"][1]["url"]);
            }

            // Explicitly output details for first 3 items
            // Need to figure out how to add this to loop
            Console.WriteLine("1 - " + SearchResults["value"][0]["url"]);
            Console.WriteLine("    " + SearchResults["value"][0]["snippet"]);
            
            Console.WriteLine("2 - " + SearchResults["value"][1]["url"]);
            Console.WriteLine("    " + SearchResults["value"][1]["snippet"]);
            
            Console.WriteLine("3 - " + SearchResults["value"][2]["url"]);
            Console.WriteLine("    " + SearchResults["value"][2]["snippet"]);

        }
       
    }

}



