Structure	Operation	Big-O (Avg)	One-sentence rationale
Array	Access by index		O(1) since you can accessing by index is one operation.
Array	Search (unsorted)		o(n) since you may have to search through the length of the array.
List<T>	Add at end		o(1) since you are only accessing and adding to the end of the list.
List<T>	Insert at index		o(n) because the list would have to shift every other item down based on where it's inserted. 
Stack<T>	Push / Pop / Peek		o(1) because it's inserting and grabbing from the top of the stack. 
Queue<T>	Enqueue / Dequeue / Peek		o(1) since you're adding the end of the line and removing from the front. 
Dictionary<K,V>	Add / Lookup / Remove		o(1) because there are no indexes to shift, you just add your k,v pair. 
HashSet<T>	Add / Contains / Remove		o(1) since there is only uniques and it could be a fast lookup. 
