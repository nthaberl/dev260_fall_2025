using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using static System.Console;

namespace Assignment8
{
    /// <summary>
    /// Core spell checker class that uses HashSet<string> for efficient word lookups and text analysis.
    /// This class demonstrates key HashSet concepts including fast Contains() operations,
    /// automatic uniqueness enforcement, and set-based text processing.
    /// </summary>
    public class SpellChecker
    {
        // Core HashSet for dictionary storage - provides O(1) word lookups
        private HashSet<string> dictionary;

        // Text analysis results - populated after analyzing a file
        private List<string> allWordsInText;
        private HashSet<string> uniqueWordsInText;
        private HashSet<string> correctlySpelledWords;
        private HashSet<string> misspelledWords;
        private string currentFileName;

        public SpellChecker()
        {
            dictionary = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            allWordsInText = new List<string>();
            uniqueWordsInText = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            correctlySpelledWords = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            misspelledWords = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            currentFileName = "";
        }

        /// <summary>
        /// Gets the number of words in the loaded dictionary.
        /// </summary>
        public int DictionarySize => dictionary.Count;

        /// <summary>
        /// Gets whether a text file has been analyzed.
        /// </summary>
        public bool HasAnalyzedText => !string.IsNullOrEmpty(currentFileName);

        /// <summary>
        /// Gets the name of the currently analyzed file.
        /// </summary>
        public string CurrentFileName => currentFileName;

        /// <summary>
        /// Gets basic statistics about the analyzed text.
        /// </summary>
        public (int totalWords, int uniqueWords, int correctWords, int misspelledWords) GetTextStats()
        {
            return (
                allWordsInText.Count,
                uniqueWordsInText.Count,
                correctlySpelledWords.Count,
                misspelledWords.Count
            );
        }

        /// <summary>
        /// Load words from the specified file into the dictionary HashSet.
        /// Requirements:
        /// - Read all lines from the file
        /// - Normalize each word (trim whitespace, convert to lowercase)
        /// - Add normalized words to the dictionary HashSet
        /// - Handle file not found gracefully
        /// - Return true if successful, false if file cannot be loaded
        /// 
        /// Key Concepts:
        /// - HashSet automatically handles duplicates
        /// - StringComparer.OrdinalIgnoreCase for case-insensitive operations
        /// - File I/O with proper error handling
        /// </summary>
        public bool LoadDictionary(string filename)
        {
            try
            {
                //create array of strings and use ReadAllLines to load each word into the array
                string[] dictionaryWords = File.ReadAllLines(filename);

                foreach (string word in dictionaryWords)
                {

                    //iterating through array and normalizing each word with .Trim() and .ToLowerInvariant()
                    string normalizedWord = word.Trim().ToLowerInvariant();

                    //dictionary.Add() will automatically handle duplicates
                    //adding each word to dictionary HashSet after normalization
                    dictionary.Add(normalizedWord);
                }

                return true;
            }
            catch (FileNotFoundException)
            {
                WriteLine("❌ File not found");
                return false;
            }
        }

        /// <summary>
        /// Load and analyze a text file, populating all internal collections.
        /// Requirements:
        /// - Read the entire file content
        /// - Tokenize into words (split on whitespace and punctuation)
        /// - Normalize each token consistently
        /// - Populate allWordsInText with all tokens (preserving duplicates)
        /// - Populate uniqueWordsInText with unique tokens
        /// - Return true if successful, false if file cannot be loaded
        /// 
        /// Key Concepts:
        /// - Text tokenization and normalization
        /// - List<T> for preserving order and duplicates
        /// - HashSet<T> for automatic uniqueness
        /// - Regex for advanced text processing (stretch goal)
        /// </summary>
        public bool AnalyzeTextFile(string fileName)
        {
            // Hint: Use File.ReadAllText() to read entire file
            // Hint: Split on char[] { ' ', '\t', '\n', '\r' } for simple tokenization
            // Hint: Use Regex.Replace to remove punctuation: @"[^\w\s]" -> ""
            // Hint: Filter out empty strings after processing
            try
            {
                //clearing the list/hashset so only one text file is analyzed at a time
                allWordsInText.Clear();
                uniqueWordsInText.Clear();

                //HasAnalyzedText is using 'currentFileName', not 'fileName', ran into issues while testing
                currentFileName = fileName;

                //all text is stored into a string
                string textBlock = File.ReadAllText(currentFileName);

                //array of separators
                char[] separators = new char[] { ' ', '\t', '\n', '\r' };

                //splitting words based on established separators
                string[] words = textBlock.Split(separators);

                foreach (string word in words)
                {
                    //normalizing words with regex, trim(), and toLowerInvariant()
                    string simplifiedWord = Regex.Replace(word.Trim().ToLowerInvariant(), @"[^\w\s]", "");

                    //only storing words in list if it is not null/empty
                    if (!string.IsNullOrEmpty(simplifiedWord))
                    {
                        allWordsInText.Add(simplifiedWord);
                        uniqueWordsInText.Add(simplifiedWord);
                    }
                }

                return true;
            }
            catch (FileNotFoundException)
            {
                WriteLine("❌ File not found");
                return false;
            }
        }

        /// <summary>
        /// After analyzing text, categorize unique words into correct and misspelled.
        /// Requirements:
        /// - Iterate through uniqueWordsInText
        /// - Use dictionary.Contains() to check each word
        /// - Add words to correctlySpelledWords or misspelledWords accordingly
        /// - Clear previous categorization before processing
        /// 
        /// Key Concepts:
        /// - HashSet.Contains() provides O(1) membership testing
        /// - Set partitioning based on criteria
        /// - Defensive programming (clear previous results)
        /// </summary>
        public void CategorizeWords()
        {
            correctlySpelledWords.Clear();
            misspelledWords.Clear();


            foreach (string word in uniqueWordsInText)
            {
                //if the word is in the dictionary, it is spelled correctly
                if (dictionary.Contains(word))
                {
                    correctlySpelledWords.Add(word);
                }
                else
                {
                    //it not in the dictionary, it is misspelled
                    misspelledWords.Add(word);
                }
            }
        }

        /// <summary>
        /// Check if a specific word is in the dictionary and/or appears in analyzed text.
        /// Requirements:
        /// - Normalize the input word consistently
        /// - Check if word exists in dictionary
        /// - If text has been analyzed, check if word appears in text
        /// - If word appears in text, count occurrences in allWordsInText
        /// - Return comprehensive information about the word
        /// 
        /// Key Concepts:
        /// - Consistent normalization across all operations
        /// - Multiple HashSet lookups for comprehensive analysis
        /// - LINQ or manual counting for frequency analysis
        /// </summary>
        public (bool inDictionary, bool inText, int occurrences) CheckWord(string word)
        {
            // Hint: Normalize the word using the same method as other operations
            // Hint: Use dictionary.Contains() and uniqueWordsInText.Contains()
            // Hint: Use allWordsInText.Count(w => w.Equals(normalizedWord, StringComparison.OrdinalIgnoreCase))

            //normalizing word before checking if it is in any list or set
            string normalizedWord = Regex.Replace(word.Trim().ToLowerInvariant(), @"[^\w\s]", "");

            bool inDictionary = dictionary.Contains(normalizedWord);
            bool inText = uniqueWordsInText.Contains(normalizedWord);
            int occurrences = allWordsInText.Count(w => w.Equals(normalizedWord, StringComparison.OrdinalIgnoreCase));

            return (inDictionary, inText, occurrences);
        }

        /// <summary>
        /// Return a sorted list of all misspelled words from the analyzed text.
        /// Requirements:
        /// - Return words from misspelledWords HashSet
        /// - Sort alphabetically for consistent display
        /// - Limit results if there are too many (optional)
        /// - Return empty collection if no text analyzed
        /// 
        /// Key Concepts:
        /// - Converting HashSet to sorted List
        /// - LINQ for sorting and limiting results
        /// - Defensive programming for uninitialized state
        /// </summary>
        public List<string> GetMisspelledWords(int maxResults = 50)
        {
            // Hint: Convert misspelledWords to List, then use OrderBy()
            // Hint: Use Take(maxResults) to limit results if needed
            // Hint: Return empty list if no text has been analyzed

            //if text has not been analyzed, return an empty set 
            if (!HasAnalyzedText)
            {
                return new List<string>();
            }

            //misspelledWords is being ordered, then only the maxResults are being stored in a list and returned
            List<string> topSortedMisspelledList = misspelledWords.OrderBy(word => word).Take(maxResults).ToList();

            return topSortedMisspelledList;
        }

        /// <summary>
        /// Return a sample of unique words found in the analyzed text.
        /// Requirements:
        /// - Return words from uniqueWordsInText HashSet
        /// - Sort alphabetically for consistent display
        /// - Limit to specified number of results
        /// - Return empty collection if no text analyzed
        /// 
        /// Key Concepts:
        /// - HashSet enumeration and conversion
        /// - LINQ for data manipulation
        /// - Sampling large datasets
        /// </summary>
        public List<string> GetUniqueWordsSample(int maxResults = 20)
        {
            
            if (!HasAnalyzedText)
            {
                return new List<string>();
            }

            //unique words does not store categorized words, so a mixture of correct and misspelled words will be returned
            //similar implementation as GetMisspelledWords()
            List<string> topUniqueWordList = uniqueWordsInText.OrderBy(word => word).Take(maxResults).ToList();

            return topUniqueWordList;
        }

        // Helper method for consistent word normalization
        private string NormalizeWord(string word)
        {
            if (string.IsNullOrWhiteSpace(word))
                return "";

            // Remove punctuation and convert to lowercase
            word = Regex.Replace(word.Trim(), @"[^\w]", "");
            return word.ToLowerInvariant();
        }
    }
}