# Assignment 9: BST File System Navigator - Implementation Notes

**Name:** Jarod Atienzo

## Binary Search Tree Pattern Understanding

**How BST operations work for file system navigation:**
[Explain your understanding of how O(log n) searches, automatic sorting through in-order traversal, and hierarchical file organization work together for efficient file management]

Answer: BSTs allow very fast and predictable searches because each comparison eliminates half the remaining possibilities, giving average-case O(log n) performance.

## Challenges and Solutions

**Biggest challenge faced:**
[Describe the most difficult part of the assignment - was it recursive tree algorithms, custom file/directory comparison logic, or complex BST deletion?]

Answer: BST deletion was definitely the hardest part because deleting a node requires reorganizing the tree without losing any children. The two-child case was the most confusing since I needed to replace the node with its inorder successor in a clean and consistent way.

**How you solved it:**
[Explain your solution approach and what helped you figure it out - research, debugging, testing strategies, etc.]

Answer: I solved it by breaking the problem down into smaller steps and printing debug outputs to track the path of the deletion.

**Most confusing concept:**
[What was hardest to understand about BST operations, recursive thinking, or file system hierarchies?]

Answer: Understanding how recursive calls return updated nodes was initially tricky.

## Code Quality

**What you're most proud of in your implementation:**
[Highlight the best aspect of your code - maybe your recursive algorithms, custom comparison logic, or efficient tree traversal]

Answer: I’m proud of how clean and reusable my BST helper methods turned out, especially InsertNode, DeleteNode, and CompareFileNodes. The comparison logic made the file system behave exactly like a real one, and my recursive code ended up being very readable.

**What you would improve if you had more time:**
[Identify areas for potential improvement - perhaps better error handling, more efficient algorithms, or additional features]

Answer: Optimizing repeated traversal and improving error messages.

## Real-World Applications

**How this relates to actual file systems:**
[Describe how your implementation connects to tools like Windows File Explorer, macOS Finder, database indexing, etc.]

Answer: Most operating systems use some form of tree structure to organize directory hierarchies.

**What you learned about tree algorithms:**
[What insights did you gain about recursive thinking, tree traversal, and hierarchical data organization?]

Answer: I learned how powerful recursion is for hierarchical data. Traversals, node replacement, and recursive returns finally made sense. I also learned how ordering rules can completely change the behavior of a tree structure.

## Stretch Features

[If you implemented any extra credit features like file pattern matching or directory size analysis, describe them here. If not, write "None implemented"]

Answer: None

## Time Spent

**Total time:** 7 hours

**Breakdown:**

- Understanding BST concepts and assignment requirements: 30 min
- Implementing the 8 core TODO methods: 4 hours
- Testing with different file scenarios: 1 hour
- Debugging recursive algorithms and BST operations: 1 hour
- Writing these notes: 30 min

**Most time-consuming part:** BST deletion was the most time-consuming because of the multiple cases, ensuring pointers updated correctly, and debugging where recursion went wrong.
