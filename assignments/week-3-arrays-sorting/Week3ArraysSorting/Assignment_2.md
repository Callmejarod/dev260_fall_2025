# Assignment 2 — Week 3 Arrays & Sorting

## Part A — Board Game Implementation (Tic Tac Toe)

### How to Play
- Players take turns marking a cell on the board.  
- The first player to align three marks horizontally, vertically, or diagonally wins.  
- If all cells are filled without a winning alignment, the game ends in a draw.  

### How to Reset
- The board is represented as a 2D array.
- To reset, all cells are set to an empty string or default value.
- The game can then start a new round without restarting the program.

### How the 2D Array is Used
- The board is stored in a 2D array, where each element represents a cell.  
- Rows and columns allow for easy checking of win conditions (horizontal, vertical, diagonal).  
- Array indices correspond directly to board positions for both input validation and game logic.


## Part B — Book Catalog

**Recursive Sort Implemented:** Quicksort

### Normalization Rules
- Titles are converted to **uppercase** using `ToUpperInvariant()`.
- Leading and trailing spaces are trimmed.

**How It Works**
1. Titles are normalized and sorted using Quicksort.  
2. The 2D index is built in a single pass over the sorted list.  
3. At lookup, the first two letters of the query determine the `[start,end)` range in O(1) time.  
4. A binary search is performed within that range to find an exact match.  
5. If no match is found, nearby suggestions can be displayed.

**Quicksort (your implementation)**  
- **Average case:** O(n log n) time, O(log n) space (recursive call stack)  
- **Worst case:** O(n²) time (if pivot selection is unlucky)  

**2D Index + Lookup**  
- **Index construction:** O(n) time, O(1) extra space (besides the arrays)  