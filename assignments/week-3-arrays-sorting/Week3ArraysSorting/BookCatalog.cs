using System;
using System.Collections.Generic;
using System.IO;

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
                
                // TODO: Handle exit condition
                if (string.IsNullOrEmpty(query) || query.ToLowerInvariant() == "exit")
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
            QuickSort(normalizedTitles, originalTitles, 0, bookCount - 1);
            Console.WriteLine("Sorting Complete!");
        }
        
        /// <summary>
        /// Build multi-dimensional index over sorted data
        /// TODO: Create 26x26 index for first two letters
        /// </summary>
        private void BuildMultiDimensionalIndex()
        {
            // Initialize arrays with -1
            for (int i = 0; i < 26; i++)
            {
                for (int j = 0; j < 26; j++)
                {
                    startIndex[i, j] = -1;
                    endIndex[i, j] = -1;
                }
            }

            for (int i = 0; i < normalizedTitles.Length; i++)
            {
                string title = normalizedTitles[i];

                // Skip empty or very short titles
                if (title.Length < 2)
                    continue;

                int first = title[0] - 'A';
                int second = title[1] - 'A';

                // Skip non-letter characters
                if (first < 0 || first > 25 || second < 0 || second > 25)
                    continue;

                // Mark start if first time seen
                if (startIndex[first, second] == -1)
                    startIndex[first, second] = i;

                // Always update end index to current + 1
                endIndex[first, second] = i + 1;
            }

            Console.WriteLine("2D letter index built successfully!");
        }
        
        /// <summary>
        /// Perform lookup with exact match and suggestions
        /// TODO: Implement indexed lookup with binary search
        /// </summary>
        /// <param name="query">User's search query</param>
        private void PerformLookup(string query)
        {
            string normalizedQuery = NormalizeTitle(query);

            if (string.IsNullOrWhiteSpace(normalizedQuery))
            {
                Console.WriteLine("Please enter a valid title.");
                return;
            }

            // Get first two uppercase letters for indexing
            char firstChar = normalizedQuery.Length > 0 ? normalizedQuery[0] : 'A';
            char secondChar = normalizedQuery.Length > 1 ? normalizedQuery[1] : 'A';

            // Convert A–Z to indices 0–25
            int firstIndex = char.ToUpper(firstChar) - 'A';
            int secondIndex = char.ToUpper(secondChar) - 'A';

            // Handle out-of-range chars (non-letters)
            if (firstIndex < 0 || firstIndex > 25 || secondIndex < 0 || secondIndex > 25)
            {
                Console.WriteLine($"No indexed books found for titles starting with '{firstChar}{secondChar}'.");
                return;
            }

            int start = startIndex[firstIndex, secondIndex];
            int end = endIndex[firstIndex, secondIndex];

            // If there’s no range for this pair
            if (start == -1 || end == -1)
            {
                Console.WriteLine($"No books found for '{query}'. Try another title or check spelling.");
                return;
            }

            Console.WriteLine($"Found index range: {start} to {end - 1} for '{firstChar}{secondChar}'");

            // Step 2: binary search within this slice (coming next)
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

        private void Swap(int i, int j)
        {
            // Swap in normailzed array
            string tempNorm = normalizedTitles[i];
            normalizedTitles[i] = normalizedTitles[j];
            normalizedTitles[j] = tempNorm;

            // Swawp in original array to keep in sync
            string tempOrig = originalTitles[i];
            originalTitles[i] = originalTitles[j];
            originalTitles[j] = tempOrig;
        }
        

        
        private int Partition(string[] normalizedArray, string[] originalArray, int low, int high)
        {
            // use the last element as a pivot
            string pivot = normalizedArray[high];
            int i = low - 1; // i tracks the "smaller" section

            for (int j = low; j < high; j++)
            {
                if (string.Compare(normalizedArray[j], pivot, StringComparison.Ordinal) < 0)
                {
                    i++;
                    Swap(i, j);
                }
            }

            // Place pivot in correct position
            Swap(i + 1, high);

            return i + 1; // Return pivot's final index

        }
        
        /// <summary>
        /// QuickSort implementation (Option 1)
        /// TODO: Implement if you choose QuickSort
        /// </summary>
        private void QuickSort(string[] normalizedArray, string[] originalArray, int low, int high)
        {
            if (low < high)
            {
                // Partition the array
                int pivotIndex = Partition(normalizedArray, originalArray, low, high);

                // Recursively sort the two halves
                QuickSort(normalizedArray, originalArray, low, pivotIndex - 1);
                QuickSort(normalizedArray, originalArray, pivotIndex + 1, high);
            }
        }
        
        /// <summary>
        /// MergeSort implementation (Option 2)  
        /// TODO: Implement if you choose MergeSort
        /// </summary>
        // private void MergeSort(string[] normalizedArray, string[] originalArray, int left, int right)
        // {
        //     // TODO: Implement recursive MergeSort
        //     // TODO: Handle O(n) extra space requirement
        //     // TODO: Ensure both arrays stay synchronized
        // }
        
        // TODO: Add helper methods as needed
        // Examples:
        // - GetLetterIndex(char letter) - Convert A-Z to 0-25
        // - BinarySearchInRange(string query, int start, int end)
        // - FindSuggestions(string query, int maxSuggestions)
        // - SwapElements(int index1, int index2) - For QuickSort
        // - MergeArrays(...) - For MergeSort
    }
}