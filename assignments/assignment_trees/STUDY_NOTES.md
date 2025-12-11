# Assignment 9: BST File System Navigator - Implementation Notes

**Name:** [Natascha]

## Binary Search Tree Pattern Understanding

**How BST operations work for file system navigation:**
*[Explain your understanding of how O(log n) searches, automatic sorting through in-order traversal, and hierarchical file organization work together for efficient file management]*  

Answer: BSTs help make search operations more efficient because instead of scanning for every item in the structure, the search is cut in half. If the tree is balanced, this means searching only takes O(log n).  
BSTs also keep everything sorted because nodes are placed based on comparison, and in-order traversal lists all items in the correct order without any additional sorting.  


## Challenges and Solutions

**Biggest challenge faced:**
*[Describe the most difficult part of the assignment - was it recursive tree algorithms, custom file/directory comparison logic, or complex BST deletion?]*  

Answer: I struggled the most with the BST deletion and ensuring the tree maintained its structure.

**How you solved it:**
*[Explain your solution approach and what helped you figure it out - research, debugging, testing strategies, etc.]*

Answer: I did a lot of Googling and reading and learning about different methods. I initially tried to implement a recursive approach, but it got very complex very quickly.  
Found an iterative approach that made it much easier to implement.  

**Most confusing concept:**
*[What was hardest to understand about BST operations, recursive thinking, or file system hierarchies?]*  

Answer: I always struggled with recursive thinking in the past but using it in this way made it a lot easier to understand.  
I'm still struggling to understand BST operations deeply but this assignment helped me understand them better.  

## Code Quality

**What you're most proud of in your implementation:**
*[Highlight the best aspect of your code - maybe your recursive algorithms, custom comparison logic, or efficient tree traversal]*  

Answer: I'm most proud of the tree traversal, I've been dreading this data structure all quarter and traversing the tree actually isn't too bad.  

**What you would improve if you had more time:**
*[Identify areas for potential improvement - perhaps better error handling, more efficient algorithms, or additional features]*  

Answer: If I had more time I would like to learn how to make my implementations more efficient and shoot for the stretch goals.  

## Real-World Applications

**How this relates to actual file systems:**
*[Describe how your implementation connects to tools like Windows File Explorer, macOS Finder, database indexing, etc.]*  

Answer: Real oeprating systems use tree-liek structures to organize and manage files efficiently, keeping everything sorted and allowing for fast lookups.  
The implementation for this assignment works in a somewhat similar way but on a much smaller scale. The BST keeps all the files in a predictable order which makes searching, inserting, and deleting faster than iterating through every item in the structure individually.

**What you learned about tree algorithms:**
*[What insights did you gain about recursive thinking, tree traversal, and hierarchical data organization?]*  

Answer: This assignment helped me to apply recursive thinking in a helpful way! I think the hierarchical data organization still left me a little confused in the grand scheme of BSTs, it added an additional layer of complexity. 

## Stretch Features

*[If you implemented any extra credit features like file pattern matching or directory size analysis, describe them here. If not, write "None implemented"]*  

Answer: None implemented

## Time Spent

**Total time:** [7 hours]

**Breakdown:**

- Understanding BST concepts and assignment requirements: [1.5 hours]
- Implementing the 8 core TODO methods: [3 hours]
- Testing with different file scenarios: [1 hours]
- Debugging recursive algorithms and BST operations: [2 hours]
- Writing these notes: [.5 hours]

**Most time-consuming part:** [Which aspect took the longest and why - recursive thinking, BST deletion, custom comparison logic, etc.]  
BST deletion took the most as a standalone task. Understanding the concepts and requirements took a lot of time while working through the assignment.  
It was also a challenge to get going with the assignment, and difficult to debug from the beginning. Methods were intertwined with other methods, so a lot of implementation had to happen before I could really get going and it was difficult to know if it was correctly implemented right away. 

