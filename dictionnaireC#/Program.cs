using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;

class ArticleDictionary
{
	private Dictionary<string, HashSet<string>> wordDictionary = new Dictionary<string, HashSet<string>>();

	
	public void LoadWordsFromXmlFiles(List<string> filePaths)
	{
		foreach (string filePath in filePaths)
		{
			if (!File.Exists(filePath))
			{
				Console.WriteLine($"not found: {filePath}");
				continue;
			}

			XmlDocument xmlDoc = new XmlDocument();
			try
			{
				
				XmlReaderSettings settings = new XmlReaderSettings
				{
					DtdProcessing = DtdProcessing.Ignore, 
					XmlResolver = null 
				};
				using (XmlReader reader = XmlReader.Create(filePath, settings))
				{
					xmlDoc.Load(reader);
				}

				Console.WriteLine($"Processing file: {filePath}");

				
				XmlNodeList articleNodes = xmlDoc.GetElementsByTagName("article");
				foreach (XmlNode articleNode in articleNodes)
				{
				
					var words = articleNode.InnerText
						.ToLower() 
						.Split(new[] { ' ', ',', '.', '!', '?', ';', ':', '(', ')', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries);

					foreach (var word in words)
					{
						
						if (!wordDictionary.ContainsKey(word))
						{
							wordDictionary[word] = new HashSet<string>();
						}
						wordDictionary[word].Add(filePath);
					}
				}
			}
			catch (XmlException ex)
			{
				Console.WriteLine($"error loading file {filePath}: {ex.Message}");
			}
			catch (IOException ex)
			{
				Console.WriteLine($"file error {filePath}: {ex.Message}");
			}
		}
	}

	
	public HashSet<string> SearchWord(string word)
	{
		string normalizedWord = word.ToLower();
		if (wordDictionary.TryGetValue(normalizedWord, out var fileLocations))
		{
			return fileLocations;
		}
		else
		{
			return new HashSet<string>();
		}
	}

	static void Main(string[] args)
	{
		ArticleDictionary articleDict = new ArticleDictionary();

		
		List<string> filePaths = new List<string>
		{
			@"C:\Users\radoi\source\repos\dictionnaireC#\dictionnaireC#\file1.xml",
			@"C:\Users\radoi\source\repos\dictionnaireC#\dictionnaireC#\file2.xml",
			@"C:\Users\radoi\source\repos\dictionnaireC#\dictionnaireC#\file3.xml",
            
        };

		articleDict.LoadWordsFromXmlFiles(filePaths);

		while (true) 
		{
			Console.WriteLine("enter a word to search (or type 'exit' to quit):");
			string searchWord = Console.ReadLine();
			if (searchWord?.ToLower() == "exit") break;

			var locations = articleDict.SearchWord(searchWord);
			if (locations.Count > 0)
			{
				Console.WriteLine($"the word '{searchWord}' is found in the following files:");
				foreach (var file in locations)
				{
					Console.WriteLine($"- {file}");
				}
			}
			else
			{
				Console.WriteLine($"the word '{searchWord}' was not found in any files.!!!");
			}
		}
	}
}
