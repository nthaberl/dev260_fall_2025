using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace FileSystemNavigator
{
    /// <summary>
    /// Binary Search Tree implementation for File System Navigation
    /// 
    /// STUDENT ASSIGNMENT: Implement the TODO methods in this class
    /// This class demonstrates BST concepts through a practical file system simulation
    /// 
    /// Learning Objectives:
    /// - Apply BST operations to hierarchical data
    /// - Implement complex search and filtering operations  
    /// - Practice file system concepts through tree structures
    /// - Build practical navigation and management tools
    /// </summary>
    public class FileSystemBST
    {
        private TreeNode? root;
        private int operationCount;
        private DateTime sessionStart;

        public FileSystemBST()
        {
            root = null;
            operationCount = 0;
            sessionStart = DateTime.Now;

            Console.WriteLine("🗂️  File System Navigator Initialized!");
            Console.WriteLine("📁 BST-based file system ready for operations.\n");
        }

        // ============================================
        // 🚀 STUDENT TODO METHODS - IMPLEMENT THESE
        // ============================================

        /// <summary>
        /// Requirements:
        /// - Insert file into BST maintaining proper ordering
        /// - Use file name for BST comparison (case-insensitive)
        /// - Handle duplicate file names (return false if exists)
        /// - Set appropriate file metadata (size, dates, extension)
        /// 
        /// BST Learning: Insertion with custom comparison logic
        /// Real-World: File creation in operating systems
        /// </summary>
        /// <param name="fileName">Name of file to create (e.g., "readme.txt")</param>
        /// <param name="size">File size in bytes (default 1024)</param>
        /// <returns>True if file created successfully, false if already exists</returns>
        public bool CreateFile(string fileName, long size = 1024)
        {
            operationCount++;

            //looking for duplicates, return false if there is one
            if (FindFile(fileName) != null)
            {
                return false;
            }
            if (size <= 0)
            {
                Console.WriteLine("❌ Please provide a valid file size");
                return false;
            }

            //creating FileNode as a File type
            var newFile = new FileNode(fileName, FileType.File, size);

            //inserting into BST with helper method
            root = InsertNode(root, newFile);
            return true;
        }

        /// <summary>
        /// Requirements:
        /// - Insert directory into BST with FileType.Directory
        /// - Directories should sort before files with same name
        /// - Set size to 0 for directories (automatic in FileNode constructor)
        /// - Handle duplicate directory names
        /// 
        /// BST Learning: Custom comparison for different node types
        /// Real-World: Directory creation and organization
        /// </summary>
        /// <param name="directoryName">Name of directory to create (e.g., "Documents")</param>
        /// <returns>True if directory created successfully, false if already exists</returns>
        public bool CreateDirectory(string directoryName)
        {
            operationCount++;

            //return false if duplicate found
            if (FindFile(directoryName) != null)
            {
                return false;
            }

            //creating FileNode as Directory type
            var newDirectory = new FileNode(directoryName, FileType.Directory);
            root = InsertNode(root, newDirectory);

            return true;
        }

        /// <summary>
        /// Requirements:
        /// - Search BST efficiently using file name as key
        /// - Case-insensitive search
        /// - Return FileNode if found, null if not found
        /// - Use recursive BST search pattern
        /// 
        /// BST Learning: O(log n) search operations
        /// Real-World: File lookup in operating systems
        /// </summary>
        /// <param name="fileName">Name of file to find (not full path)</param>
        /// <returns>FileNode if found, null otherwise</returns>
        public FileNode? FindFile(string fileName)
        {
            operationCount++;

            // 1. Use SearchNode helper method with recursive approach
            // 2. Compare file names case-insensitively
            // 3. Return the FileNode.FileData if found

            //SearchNode help functions handles all these steps, so only need to call the function
            return SearchNode(root, fileName);
        }

        /// <summary>
        /// Requirements:
        /// - Traverse entire BST collecting files with matching extension
        /// - Case-insensitive extension comparison (.txt = .TXT)
        /// - Return List of FileNode objects
        /// - Use in-order traversal for consistent ordering
        /// 
        /// BST Learning: Tree traversal with filtering
        /// Real-World: File type searches (find all .cs files)
        /// </summary>
        /// <param name="extension">File extension to search for (.txt, .cs, etc.)</param>
        /// <returns>List of files with matching extension</returns>
        public List<FileNode> FindFilesByExtension(string extension)
        {
            operationCount++;

            //ensuring the extension starts with leading .
            if (!extension.StartsWith("."))
            {
                extension = "." + extension;
            }

            //list to store matching files
            var matchList = new List<FileNode>();

            //using helper method to collect files that match the extension, is type File, case-insensitive
            TraverseAndCollect(root, matchList, fileNode =>
            fileNode.Type == FileType.File &&
            string.Equals(fileNode.Extension, extension, StringComparison.OrdinalIgnoreCase));

            return matchList;
        }

        /// <summary>
        /// Requirements:
        /// - Search for files between minSize and maxSize (inclusive)
        /// - Only include FileType.File items (not directories)
        /// - Return files sorted by name (in-order traversal)
        /// - Handle edge cases (minSize > maxSize)
        /// 
        /// BST Learning: Range queries and filtered traversal
        /// Real-World: Find large files for cleanup, small files for compression
        /// </summary>
        /// <param name="minSize">Minimum file size in bytes</param>
        /// <param name="maxSize">Maximum file size in bytes</param>
        /// <returns>List of files within size range</returns>
        public List<FileNode> FindFilesBySize(long minSize, long maxSize)
        {
            operationCount++;

            //list to store files that match parameters
            var matchList = new List<FileNode>();

            //validating input parameters
            if (minSize > maxSize)
            {
                Console.WriteLine("❌Minimum size cannnot be greater than maximum size!");
                return matchList;
            }

            TraverseAndCollect(root, matchList, fileNode =>
            fileNode.Type == FileType.File &&
            fileNode.Size >= minSize &&
            fileNode.Size <= maxSize);

            return matchList;
        }

        /// <summary>
        /// Requirements:
        /// - Collect all files and sort by size (descending)
        /// - Return top N largest files
        /// - Handle case where N > total file count
        /// - Only include FileType.File items
        /// 
        /// BST Learning: Tree traversal with post-processing
        /// Real-World: Disk cleanup utilities, storage analysis
        /// </summary>
        /// <param name="count">Number of largest files to return</param>
        /// <returns>List of largest files, sorted by size descending</returns>
        public List<FileNode> FindLargestFiles(int count)
        {
            operationCount++;

            var results = new List<FileNode>();

            //handling edge case however FileSystemNavigator already handles this case :)
            if (count <= 0)
            {
                Console.WriteLine("❌ Count must be greater than 0");
                return results;
            }

            //traverse and collect all files of File type
            TraverseAndCollect(root, results, fileNode => fileNode.Type == FileType.File);

            //using LINQ to order results
            return results.OrderByDescending(file => file.Size)
            .Take(count)
            .ToList();
        }

        /// <summary>
        /// TODO #7: Calculate total size of all files and directories
        /// 
        /// Requirements:
        /// - Traverse entire BST and sum all file sizes
        /// - Include both files and directories in count
        /// - Use recursive traversal approach
        /// - Return total size in bytes
        /// 
        /// BST Learning: Aggregation through tree traversal
        /// Real-World: Disk usage analysis, storage reporting
        /// </summary>
        /// <returns>Total size of all files in bytes</returns>
        public long CalculateTotalSize()
        {
            operationCount++;

            //hints said to use recursive helper method so created one
            //see CalculateTotalSizeRecursive to see implementation

            return CalculateTotalSizeRecursive(root);
        }

        /// <summary>

        /// Requirements:
        /// - Remove item from BST maintaining tree structure
        /// - Handle all three deletion cases (no children, one child, two children)
        /// - Return true if deleted, false if not found
        /// - Use standard BST deletion algorithm
        /// 
        /// BST Learning: Complex deletion maintaining tree structure
        /// Real-World: File deletion in operating systems
        /// </summary>
        /// <param name="fileName">Name of file or directory to delete</param>
        /// <returns>True if deleted successfully, false if not found</returns>
        public bool DeleteItem(string fileName)
        {
            operationCount++;

            if (root == null)
            {
                return false; // Tree is empty, cannot delete
            }

            TreeNode? parent = null;
            TreeNode? current = root;
            bool found = false;

            var searchNode = new FileNode(fileName, FileType.File);

            // Find the node to delete and its parent
            while (current != null)
            {
                int comparison = CompareFileNodes(searchNode, current.FileData);

                //comparison found, break out of loop
                if (comparison == 0)
                {
                    found = true;
                    break;
                }

                parent = current;
                if (comparison < 0)
                {
                    current = current.Left;
                }
                else
                {
                    current = current.Right;
                }
            }

            if (!found)
            {
                return false; // node not found
            }

            // Case 1: no children 
            if (current.Left == null && current.Right == null)
            {
                if (parent == null) // Deleting root node
                {
                    root = null;
                }
                else if (parent.Left == current)
                {
                    parent.Left = null;
                }
                else
                {
                    parent.Right = null;
                }
            }

            // Case 2: Node has one child (only right child)
            else if (current.Left == null)
            {
                if (parent == null)
                {
                    root = current.Right;
                }
                else if (parent.Left == current)
                {
                    parent.Left = current.Right;
                }
                else
                {
                    parent.Right = current.Right;
                }
            }
            // Case 2: Node has one child (only left child)
            else if (current.Right == null)
            {
                if (parent == null)
                {
                    root = current.Left;
                }
                else if (parent.Left == current)
                {
                    parent.Left = current.Left;
                }
                else
                {
                    parent.Right = current.Left;
                }
            }
            // Case 3: Node has two children
            else
            {
                // Find in-order successor (smallest in right subtree)
                TreeNode successorParent = current;
                TreeNode successor = current.Right;

                while (successor.Left != null)
                {
                    successorParent = successor;
                    successor = successor.Left;
                }

                // Copying successor's FileData to current node
                current.FileData = successor.FileData;

                // Delete successor node
                if (successorParent.Left == successor)
                {
                    successorParent.Left = successor.Right;
                }
                else
                {
                    successorParent.Right = successor.Right;
                }
            }

            return true; 

        }

        // ============================================
        // 🔧 HELPER METHODS FOR TODO IMPLEMENTATION
        // ============================================

        /// <summary>
        /// Helper method for BST insertion
        /// Students should use this in CreateFile and CreateDirectory
        /// </summary>
        private TreeNode? InsertNode(TreeNode? node, FileNode fileData)
        {
            //base case
            if (node == null)
            {
                return new TreeNode(fileData);
            }

            int comparison = CompareFileNodes(fileData, node.FileData);

            //navigate left
            if (comparison < 0)
            {
                node.Left = InsertNode(node.Left, fileData);
            }
            //navigate right
            else if (comparison > 0)
            {
                node.Right = InsertNode(node.Right, fileData);
            }

            return node;
        }

        /// <summary>
        /// Helper method for BST searching
        /// Students should use this in FindFile
        /// </summary>
        private FileNode? SearchNode(TreeNode? node, string fileName)
        {

            // Base case: if node is null, return null
            if (node == null)
            {
                return null;
            }

            var searchNode = new FileNode(fileName, FileType.File);
            int comparison = CompareFileNodes(searchNode, node.FileData);

            //if names match, return node.fileData
            if (comparison == 0)
            {
                return node.FileData;
            }
            //navigate left
            else if (comparison < 0)
            {
                return SearchNode(node.Left, fileName);
            }
            //or navigate right
            else
            {
                return SearchNode(node.Right, fileName);
            }
        }

        /// <summary>
        /// Helper method for collecting nodes during traversal
        /// Students should use this for FindFilesByExtension, FindFilesBySize, etc.
        /// </summary>
        private void TraverseAndCollect(TreeNode? node, List<FileNode> collection, Func<FileNode, bool> filter)
        {
            //base case
            if (node == null)
            {
                return;
            }

            //traverse left subtree first
            TraverseAndCollect(node.Left, collection, filter);

            //process current, only add if true
            if (filter(node.FileData))
            {
                collection.Add(node.FileData);
            }

            //traverse right
            TraverseAndCollect(node.Right, collection, filter);
        }

        private long CalculateTotalSizeRecursive(TreeNode? node)
        {
            //empty tree case
            if (node == null)
            {
                return 0;
            }

            //sum of Size of current node + left + right 
            return node.FileData.Size + CalculateTotalSizeRecursive(node.Left) + CalculateTotalSizeRecursive(node.Right);
        }

        /// <summary>
        /// Custom comparison method for file system ordering
        /// Directories come before files, then alphabetical by name
        /// </summary>
        private int CompareFileNodes(FileNode a, FileNode b)
        {
            // Directories sort before files
            if (a.Type != b.Type)
                return a.Type == FileType.Directory ? -1 : 1;

            // Then alphabetical by name (case-insensitive)
            return string.Compare(a.Name, b.Name, StringComparison.OrdinalIgnoreCase);
        }

        // ============================================
        // 🎯 PROVIDED UTILITY METHODS
        // ============================================

        /// <summary>
        /// Display the file system tree structure visually
        /// Helps students visualize their BST structure
        /// </summary>
        public void DisplayTree()
        {
            Console.WriteLine("🌳 File System Tree Structure:");
            Console.WriteLine("================================");

            if (root == null)
            {
                Console.WriteLine("   (Empty file system)");
                return;
            }
            DisplayTreeEnhanced(root, "", true, true);
            Console.WriteLine("================================\n");
            Console.WriteLine("🌲 Horizontal Level-by-Level View:");
            DisplayTreeByLevels();
        }

        /// <summary>
        /// Enhanced tree display with better visual formatting and clear parent-child relationships
        /// </summary>
        private void DisplayTreeEnhanced(TreeNode? node, string prefix, bool isLast, bool isRoot)
        {
            if (node == null) return;

            // Display current node with enhanced formatting
            string connector = isRoot ? "🌟 " : (isLast ? "└── " : "├── ");
            string nodeInfo = $"{node.FileData.Name}{(node.FileData.Type == FileType.Directory ? "/" : $" ({FormatSize(node.FileData.Size)})")}";

            Console.WriteLine(prefix + connector + nodeInfo);

            // Update prefix for children
            string childPrefix = prefix + (isRoot ? "" : (isLast ? "    " : "│   "));

            // Display children with clear Left/Right indicators
            bool hasLeft = node.Left != null;
            bool hasRight = node.Right != null;

            if (hasRight)
            {
                Console.WriteLine(childPrefix + "│");
                Console.WriteLine(childPrefix + "├─(R)─┐");
                DisplayTreeEnhanced(node.Right, childPrefix + "│     ", !hasLeft, false);
            }

            if (hasLeft)
            {
                Console.WriteLine(childPrefix + "│");
                Console.WriteLine(childPrefix + "└─(L)─┐");
                DisplayTreeEnhanced(node.Left, childPrefix + "      ", true, false);
            }
        }

        /// <summary>
        /// Display tree in a horizontal level-by-level format
        /// </summary>
        private void DisplayTreeByLevels()
        {
            if (root == null) return;

            var queue = new Queue<(TreeNode?, int)>();
            queue.Enqueue((root, 0));
            int currentLevel = -1;

            while (queue.Count > 0)
            {
                var (node, level) = queue.Dequeue();

                if (level > currentLevel)
                {
                    if (currentLevel >= 0) Console.WriteLine();
                    Console.Write($"Level {level}: ");
                    currentLevel = level;
                }

                if (node != null)
                {
                    Console.Write($"[{node.FileData.Name}{(node.FileData.Type == FileType.Directory ? "/" : "")}] ");
                    queue.Enqueue((node.Left, level + 1));
                    queue.Enqueue((node.Right, level + 1));
                }
                else
                {
                    Console.Write("[null] ");
                }
            }
            Console.WriteLine();
        }


        private string FormatSize(long bytes)
        {
            if (bytes < 1024) return $"{bytes}B";
            if (bytes < 1024 * 1024) return $"{bytes / 1024}KB";
            return $"{bytes / (1024 * 1024)}MB";
        }

        /// <summary>
        /// Get comprehensive statistics about the file system
        /// </summary>
        public FileSystemStats GetStatistics()
        {
            var stats = new FileSystemStats
            {
                TotalOperations = operationCount,
                SessionDuration = DateTime.Now - sessionStart
            };

            if (root != null)
            {
                CalculateStats(root, stats);
            }

            return stats;
        }

        private void CalculateStats(TreeNode? node, FileSystemStats stats)
        {
            if (node == null) return;

            var file = node.FileData;
            if (file.Type == FileType.File)
            {
                stats.TotalFiles++;
                stats.TotalSize += file.Size;

                if (file.Size > stats.LargestFileSize)
                {
                    stats.LargestFileSize = file.Size;
                    stats.LargestFile = file.Name;
                }
            }
            else
            {
                stats.TotalDirectories++;
            }

            CalculateStats(node.Left, stats);
            CalculateStats(node.Right, stats);
        }

        /// <summary>
        /// Check if the file system is empty
        /// </summary>
        public bool IsEmpty() => root == null;

        /// <summary>
        /// Load sample data for testing and demonstration
        /// </summary>
        public void LoadSampleData()
        {
            Console.WriteLine("📁 Loading sample file system data...");

            // Sample directories
            var sampleDirs = new[]
            {
                "Documents", "Pictures", "Videos", "Music", "Downloads",
                "Projects", "Code", "Images", "Archive"
            };

            // Sample files with extensions and sizes
            var sampleFiles = new[]
            {
                ("readme.txt", 2048L), ("config.json", 1024L), ("app.cs", 5120L),
                ("photo.jpg", 2048000L), ("song.mp3", 4096000L), ("video.mp4", 52428800L),
                ("document.pdf", 1048576L), ("presentation.pptx", 3145728L),
                ("spreadsheet.xlsx", 512000L), ("archive.zip", 10485760L)
            };

            try
            {
                // Create directories
                foreach (var dir in sampleDirs.Take(6))
                {
                    CreateDirectory(dir);
                }

                // Create files
                foreach (var (fileName, size) in sampleFiles.Take(8))
                {
                    CreateFile(fileName, size);
                }

                Console.WriteLine("✅ Sample data loaded successfully!");
            }
            catch (NotImplementedException)
            {
                Console.WriteLine("⚠️  Cannot load sample data - TODO methods not implemented yet");
            }
        }
    }
}