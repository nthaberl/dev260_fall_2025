using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters;
using System.Text.RegularExpressions;
using static System.Console;

namespace Week3ArraysSorting
{
    /// <summary>
    /// Book Catalog implementation for Assignment 2 Part B
    /// Demonstrates recursive sorting and multi-dimensional indexing for fast lookups
    /// 
    /// Learning Focus:
    /// - Recursive sorting algorithms (QuickSort or MergeSort)
    /// - Multi-dimensional array indexing for performance
    /// - String normalization and binary search
    /// - File I/O and CLI interaction
    /// </summary>
    public class BookCatalog
    {
        #region Data Structures

        // Book storage arrays - parallel arrays that stay synchronized
        private string[] originalTitles;    // Original book titles for display
        private string[] normalizedTitles;  // Normalized titles for sorting/searching

        // Multi-dimensional index for O(1) lookup by first two letters (A-Z x A-Z = 26x26)
        private int[,] startIndex;  // Starting position for each letter pair in sorted array
        private int[,] endIndex;    // Ending position for each letter pair in sorted array

        // Book count tracker
        private int bookCount;

        #endregion

        /// <summary>
        /// Constructor - Initialize the book catalog
        /// Sets up data structures for book storage and multi-dimensional indexing
        /// </summary>
        public BookCatalog()
        {
            // Initialize arrays (will be resized when books are loaded)
            originalTitles = Array.Empty<string>();
            normalizedTitles = Array.Empty<string>();

            // Initialize multi-dimensional index arrays (26x26 for A-Z x A-Z)
            startIndex = new int[26, 26];
            endIndex = new int[26, 26];

            // Initialize all index ranges as empty (-1 indicates no books for that letter pair)
            for (int i = 0; i < 26; i++)
            {
                for (int j = 0; j < 26; j++)
                {
                    startIndex[i, j] = -1;  // -1 means no books start with this letter pair
                    endIndex[i, j] = -1;    // -1 means no books end with this letter pair
                }
            }

            // Reset book count
            bookCount = 0;

            Console.WriteLine("BookCatalog initialized - Ready to load books and build index");
        }

        /// <summary>
        /// Load books from file and build sorted index
        /// </summary>
        /// <param name="filePath">Path to books.txt file</param>
        public void LoadBooks(string filePath)
        {
            try
            {
                Console.WriteLine($"Loading books from: {filePath}");

                // Step 1 - Load books from file
                LoadBooksFromFile(filePath);

                // TODO: Step 2 - Sort using recursive algorithm
                SortBooksRecursively();

                // TODO: Step 3 - Build multi-dimensional index
                BuildMultiDimensionalIndex();

                Console.WriteLine($"Successfully loaded and indexed {bookCount} books.");
                Console.WriteLine("Index built for A-Z x A-Z (26x26) letter pairs.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading books: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Start interactive lookup session
        /// TODO: Implement the CLI loop
        /// </summary>
        public void StartLookupSession()
        {
            Console.Clear();
            Console.WriteLine("=== BOOK CATALOG LOOKUP (Part B) ===");
            Console.WriteLine();

            // TODO: Check if books are loaded
            if (bookCount == 0)
            {
                Console.WriteLine("No books loaded! Please load a book file first.");
                return;
            }

            DisplayLookupInstructions();

            // TODO: Implement lookup loop
            bool keepLooking = true;

            while (keepLooking)
            {
                Console.WriteLine();
                Console.Write("Enter a book title (or 'exit'): ");
                string? query = Console.ReadLine();

                //ensure the query isn't blank
                if (query == null || query.Length == 0)
                {
                    WriteLine("Please enter a valid search term or type 'exit' to quit the program");
                    continue;
                }
                // TODO: Handle exit condition
                if (query.ToLowerInvariant() == "exit")
                {
                    keepLooking = false;
                    continue;
                }

                // TODO: Perform lookup
                PerformLookup(query);
            }

            Console.WriteLine("Returning to main menu...");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        /// <summary>
        /// Load book titles from text file
        /// </summary>
        /// <param name="filePath">Path to the books file</param>
        private void LoadBooksFromFile(string filePath)
        {
            // Check if file exists
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Book file not found: {filePath}");
            }

            Console.WriteLine($"Reading book titles from: {filePath}");

            try
            {
                // Read all lines from file
                string[] lines = File.ReadAllLines(filePath);

                // Filter out empty lines and whitespace-only lines
                var validLines = new List<string>();
                foreach (string line in lines)
                {
                    string trimmedLine = line.Trim();
                    if (!string.IsNullOrEmpty(trimmedLine))
                    {
                        validLines.Add(trimmedLine);
                    }
                }

                // Initialize arrays with the correct size
                bookCount = validLines.Count;
                originalTitles = new string[bookCount];
                normalizedTitles = new string[bookCount];

                // Store both original and normalized versions
                for (int i = 0; i < bookCount; i++)
                {
                    originalTitles[i] = validLines[i]; // Keep original formatting for display
                    normalizedTitles[i] = NormalizeTitle(originalTitles[i]); // Normalized for sorting/indexing
                }

                Console.WriteLine($"Successfully loaded {bookCount} book titles.");

            }
            catch (IOException ex)
            {
                throw new IOException($"Error reading file '{filePath}': {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Unexpected error loading books from '{filePath}': {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Normalize book title for consistent sorting and indexing
        /// </summary>
        /// <param name="title">Original book title</param>
        /// <returns>Normalized title for sorting/indexing</returns>
        private string NormalizeTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return "";
            }

            // Step 1: Trim whitespace and convert to uppercase
            string normalized = title.Trim().ToUpperInvariant();

            // Step 2: Optional - Remove leading articles for better sorting
            // This helps group books by their main title rather than article
            string[] articles = { "THE ", "A ", "AN " };

            foreach (string article in articles)
            {
                if (normalized.StartsWith(article))
                {
                    normalized = normalized.Substring(article.Length).Trim();
                    break; // Only remove the first article found
                }
            }

            // Step 3: Handle edge case where title was only articles
            if (string.IsNullOrEmpty(normalized))
            {
                return title.Trim().ToUpperInvariant(); // Return original if normalization results in empty
            }

            return normalized;
        }

        /// <summary>
        /// Sort books using recursive algorithm (QuickSort OR MergeSort)
        /// TODO: Choose ONE recursive sorting algorithm to implement
        /// </summary>
        private void SortBooksRecursively()
        {
            // Console.WriteLine("TODO: Implement recursive sorting algorithm");
            // Console.WriteLine("- Document Big-O time/space complexity in README");

            // TODO: Call your chosen sorting algorithm
            // Example: QuickSort(normalizedTitles, originalTitles, 0, bookCount - 1);
            MergeSort(normalizedTitles, originalTitles, 0, bookCount - 1);

            //ensure sort worked successfully 
            // PrintSortedTitles();
        }

        // <summary>
        // Converting

        /// <summary>
        /// Build multi-dimensional index over sorted data
        /// TODO: Create 26x26 index for first two letters
        /// </summary>
        private void BuildMultiDimensionalIndex()
        {
            // Console.WriteLine("TODO: Build multi-dimensional index");
            // Console.WriteLine("Requirements:");
            // Console.WriteLine("- Create int[,] startIndex and int[,] endIndex arrays (26x26)");
            // Console.WriteLine("- Map A-Z to indices 0-25");
            // Console.WriteLine("- Handle non-letter starts (map to index 0 or create 27th bucket)");
            // Console.WriteLine("- Scan sorted array once to record [start,end) ranges");
            // Console.WriteLine("- Empty ranges should have start > end or start = -1");

            // TODO: Initialize index arrays
            //although initialized in constructor, clears any previous index data
            //initializing 2D array with -1 indicating empty fields
            for (int i = 0; i < 26; i++)
            {
                for (int j = 0; j < 26; j++)
                {
                    startIndex[i, j] = -1;
                    endIndex[i, j] = -1;
                }
            }

            // TODO: Scan sorted titles and record boundaries for each letter pair
            for (int bookIndex = 0; bookIndex < bookCount; bookIndex++)
            {
                string title = normalizedTitles[bookIndex];

                //grabbing first and second letter of titles
                char firstLetter = title[0];
                char secondLetter = title[1];

                //mapping letters to indices
                //non-letters treated as 'A'/[0]
                int firstIndex = (firstLetter >= 'A' && firstLetter <= 'Z') ? firstLetter - 'A' : 0;
                int secondIndex = (secondLetter >= 'A' && secondLetter <= 'Z') ? secondLetter - 'A' : 0;

                //where to store a new two letter prefix
                if (startIndex[firstIndex, secondIndex] == -1)
                {
                    startIndex[firstIndex, secondIndex] = bookIndex;
                }

                endIndex[firstIndex, secondIndex] = bookIndex + 1;

            }
        }

        /// <summary>
        /// Perform lookup with exact match and suggestions
        /// TODO: Implement indexed lookup with binary search
        /// </summary>
        /// <param name="query">User's search query</param>
        private void PerformLookup(string query)
        {
            // TODO: Normalize query same way as indexing
            string normalizedQuery = NormalizeTitle(query);

            // Console.WriteLine($"TODO: Perform lookup for '{query}'");
            // Console.WriteLine("1. Get first 1-2 letters of normalized query");
            char firstLetter = normalizedQuery.Length >= 1 ? normalizedQuery[0] : 'A';
            char secondLetter = normalizedQuery.Length >= 2 ? normalizedQuery[1] : 'A';

            //Console.WriteLine("2. Look up [start,end) range from 2D index in O(1)");
            int firstIndex = (firstLetter >= 'A' && firstLetter <= 'Z') ? firstLetter - 'A' : 0;
            int secondIndex = (secondLetter >= 'A' && secondLetter <= 'Z') ? secondLetter - 'A' : 0;

            int start = startIndex[firstIndex, secondIndex];
            int end = endIndex[firstIndex, secondIndex];

            //Console.WriteLine("3. If empty range, show suggestions from nearby ranges");
            if (start == -1)
            {
                WriteLine("No exact match found.");
                WriteLine("Here are some suggestions:");

                //extract first letter of user's query
                //if empty, default to 'A', which should happen since queries can not be empty
                char queryFirst = normalizedQuery.Length > 0 ? normalizedQuery[0] : 'A';

                // Building list of potential suggestions
                //comparing first letter of every title to query's first letter 
                //and measure how far apart they are in the alphabet
                var suggestions = new List<(string title, int distance)>();
                for (int i = 0; i < bookCount; i++)
                {
                    char titleFirst = normalizedTitles[i].Length > 0 ? normalizedTitles[i][0] : 'A';
                    int distance = Math.Abs(titleFirst - queryFirst);
                    suggestions.Add((originalTitles[i], distance));
                }

                // Sort suggestions by distance in the alphabet
                //ensuring titles beginning with nearby letters (not just letters after) appear on the list
                suggestions.Sort((a, b) => a.distance.CompareTo(b.distance));

                // Show up to 5 suggestions
                int numSuggestions = Math.Min(5, suggestions.Count);
                for (int i = 0; i < numSuggestions; i++)
                {
                    WriteLine($"- {suggestions[i].title}");
                }

                return;
            }

            //Console.WriteLine("4. If non-empty range, binary search within slice");
            // Binary search for exact match within slice
            int left = start; //left boundary of seach range
            int right = end; //right boundary
            bool exactMatch = false;
            int matchIndex = -1;

            while (left <= right)
            {
                int midpoint = (left + right) / 2;

                //matching normalized titles to normalize query
                if (normalizedTitles[midpoint].CompareTo(normalizedQuery) == 0)
                {
                    exactMatch = true;
                    matchIndex = midpoint;
                    WriteLine($"Exact match found: {originalTitles[matchIndex]}");
                    break;
                }
                //if midpoint comes before the query alphabetically, narrow search to right side
                else if (normalizedTitles[midpoint].CompareTo(normalizedQuery) < 0)
                {
                    left = midpoint + 1;
                }
                else
                //otherwise search left side
                {
                    right = midpoint - 1;
                }
            }

            if (!exactMatch)
            {
                WriteLine("No exact match found. Here are the closest titles:");

                int neighborBefore = right; // points to last element 'smaller' than query
                int neighborAfter = left;   // points to first element 'larger' than query

                // Prevents from moving out of bounds
                if (neighborBefore < start) neighborBefore = start;
                if (neighborAfter > end) neighborAfter = end;

                // Ensure two distinct suggestions
                if (neighborBefore == neighborAfter)
                {
                    if (neighborAfter + 1 <= end)
                        neighborAfter = neighborAfter + 1;
                    else if (neighborBefore - 1 >= start)
                        neighborBefore = neighborBefore - 1;
                }

                // Print titles alphabetically closest to query
                if (neighborBefore >= start && neighborBefore <= end)
                    WriteLine($"- {originalTitles[neighborBefore]}");

                if (neighborAfter >= start && neighborAfter <= end && neighborAfter != neighborBefore)
                    WriteLine($"- {originalTitles[neighborAfter]}");
            }

            // TODO: Extract first two letters for indexing
            // TODO: Get start/end range from 2D index
            // TODO: If range is empty, find suggestions
            // TODO: If range exists, binary search for exact match
            // TODO: Display results using original titles
        }


        /// <summary>
        /// Display lookup instructions
        /// TODO: Customize instructions for your implementation
        /// </summary>

        private void DisplayLookupInstructions()
        {
            Console.WriteLine("BOOK LOOKUP INSTRUCTIONS:");
            Console.WriteLine("- Enter any book title to search");
            Console.WriteLine("- Exact matches will be shown if found");
            Console.WriteLine("- Suggestions provided for partial/no matches");
            Console.WriteLine("- Type 'exit' to return to main menu");
            Console.WriteLine();
            Console.WriteLine($"Catalog contains {bookCount} books, sorted and indexed for fast lookup.");
        }

        // TODO: Add your sorting algorithm methods
        /// <summary>
        /// MergeSort implementation (Option 2)  
        /// TODO: Implement if you choose MergeSort
        /// </summary>
        private void MergeSort(string[] normalizedArray, string[] originalArray, int left, int right)
        {
            // TODO: Implement recursive MergeSort
            //Time Complexity: always (n log n), Space Complexity: O(n)

            //base case: single element is inherently sorted
            if (left >= right)
            {
                return;
            }

            //split range into 2 halves, recursively sort each half
            int midpoint = (left + right) / 2;

            MergeSort(normalizedArray, originalArray, left, midpoint);
            MergeSort(normalizedArray, originalArray, midpoint + 1, right);

            //merge the two sorted array halves
            MergeArrays(normalizedArray, originalArray, left, midpoint, right);
            // TODO: Handle O(n) extra space requirement
            // TODO: Ensure both arrays stay synchronized
        }

        private void MergeArrays(string[] normalizedArray, string[] originalArray, int left, int midpoint, int right)
        {
            var leftArrayLength = midpoint - left + 1;
            var rightArrayLength = right - midpoint;

            var leftNormTempArray = new string[leftArrayLength];
            var rightNormTempArray = new string[rightArrayLength];
            var leftOrigTempArray = new string[leftArrayLength];
            var rightOrigTempArray = new string[rightArrayLength];

            //copying data into temp arrays
            for (int i = 0; i < leftArrayLength; i++)
            {
                leftNormTempArray[i] = normalizedArray[left + i];
                leftOrigTempArray[i] = originalArray[left + i];
            }

            for (int j = 0; j < rightArrayLength; j++)
            {
                rightNormTempArray[j] = normalizedArray[midpoint + 1 + j];
                rightOrigTempArray[j] = originalArray[midpoint + 1 + j];
            }

            int leftSide = 0, rightSide = 0;
            int k = left;

            //merging back into the main arrays
            while (leftSide < leftArrayLength && rightSide < rightArrayLength)
            {
                if (leftNormTempArray[leftSide].CompareTo(rightNormTempArray[rightSide]) <= 0)
                {
                    normalizedArray[k] = leftNormTempArray[leftSide];
                    originalArray[k] = leftOrigTempArray[leftSide];
                    leftSide++;
                }
                else
                {
                    normalizedArray[k] = rightNormTempArray[rightSide];
                    originalArray[k] = rightOrigTempArray[rightSide];
                    rightSide++;
                }
                k++;
            }

            //copying any elements from leftTemp arrays and the rightTemp arrays into the merged array
            while (leftSide < leftArrayLength)
            {
                normalizedArray[k] = leftNormTempArray[leftSide];
                originalArray[k] = leftOrigTempArray[leftSide];
                leftSide++;
                k++;
            }

            while (rightSide < rightArrayLength)
            {
                normalizedArray[k] = rightNormTempArray[rightSide];
                originalArray[k] = rightOrigTempArray[rightSide];
                rightSide++;
                k++;
            }
        }

        // TODO: Add helper methods as needed
        // Examples:
        // - BinarySearchInRange(string query, int start, int end)
        // - FindSuggestions(string query, int maxSuggestions)
        // - SwapElements(int index1, int index2) - For QuickSort

        //helper method to ensure book titles are sorted
        private void PrintSortedTitles()
        {
            if (bookCount == 0)
            {
                WriteLine("No books to display");
            }

            WriteLine("=== Book Catalog ===");
            for (int i = 0; i < bookCount; i++)
            {
                WriteLine($"{i + 1}. {originalTitles[i]} // {normalizedTitles[i]}");
            }

            WriteLine("=== End of Catalog ===");
        }
    }
}