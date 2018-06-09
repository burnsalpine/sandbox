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

namespace textus
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("### Textus Textual Analyzer v1.0 ###");;
            Console.WriteLine("Enter path to source text:");

            // Get path for source text
            string SourcePath = Console.ReadLine();

            // Load text into memory
            Console.WriteLine("Loading " + SourcePath + "...");
            string SourceText = File.ReadAllText(SourcePath);

            // Convert text to lowercase
            SourceText = SourceText.ToLower();

            // Convert to list of strings, delimiting on spaces
            List<string> WordList = SourceText.Split(' ').ToList();

            Console.WriteLine("Performing textual analysis...");
            Console.WriteLine("Word Count: " + WordList.Count);
        }
    }
}
