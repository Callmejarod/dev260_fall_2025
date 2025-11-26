# Doubly Linked List & Music Playlist - Study Notes

## Doubly Linked List building step by step progress

- **Step 3: Add Methods**
    - I first started with implementing methods to add nodes.
    - Checked if the list was empty. If so, set the new node as both head and tail.
    - If not empty, properly updated `Next` and `Previous` pointers to maintain the doubly linked structure.

- **Step 4: Remove Methods**
    - Implemented removal by node and by position.
    - Faced challenges updating `head` or `tail` when removing first or last node.
    - Needed to ensure the `Previous` and `Next` references were correctly updated to avoid null reference errors.

- **Step 5: Traversal & Count**
    - Implemented forward and backward traversal using `Next` and `Previous`.
    - Added `Count` property to track the number of nodes in the list.

- **Step 6: Search & Indexing**
    - Create methods to find a node by value.
    - Need a helper method `GetNodeAt(position)` to access nodes by index since arrays aren’t used.


## Music Playlist Implementation Progress

- **Step 10a: Adding Songs**
    - Implemented `AddSong` to add to the end of the playlist.
    - Implemented `AddSongAt` to insert at a specific position.
    - Set `currentSong` to the first node when the playlist is empty.

- **Step 10b: Removing Songs**
    - Implemented `RemoveSong` to remove a specific song.
    - Implemented `RemoveSongAt` to remove by position.
    - Updated `currentSong` reference if the currently playing song was removed.

- **Step 10c: Navigation**
    - Implemented `Next()`, `Previous()`, and `JumpToSong(position)`.
    - Learned the importance of null checks for head, tail, and current nodes.
    - Ensured bidirectional traversal worked correctly for all positions.

- **Step 11: Display**
    - Implemented `DisplayPlaylist` to show all songs with position numbers.
    - Marked the current song with a "►" indicator.
    - Implemented `DisplayCurrentSong` for detailed metadata (Title, Artist, Album, Year, Duration, Genre).

---

## Challenges Faced

- Handling `currentSong` updates when adding/removing nodes.
- Accessing nodes by position since linked lists do not support direct indexing.
- Ensuring correct pointers after insertions/removals!!!!
- Mostly Wrapping my head around the logic of repointing to a new node when inserting or removing was my biggest struggle. 

## Performance Reflection

- Doubly linked lists are ideal for efficient insertion/removal anywhere in the list (O(1) if you have the node reference).
- They allow bidirectional traversal, making them perfect for playlists, undo/redo functionality, or browser history.
- Arrays/lists would be better if frequent random access by index is needed.