# Assignment 8: Spell Checker & Vocabulary Explorer - Implementation Notes

**Name:** Jarod Atienzo

## HashSet Pattern Understanding

**How HashSet<T> operations work for spell checking:**
HashSet<T> allows O(1) membership testing, which means checking whether a word exists in the dictionary is extremely fast regardless of the dictionary size. Additionally, HashSet automatically enforces uniqueness, so I didn’t need to manually filter out duplicate words from the text. By using HashSet for `uniqueWordsInText`, `correctlySpelledWords`, and `misspelledWords`, I could efficiently categorize words into correct and incorrect groups without worrying about duplicates.

## Challenges and Solutions

**Biggest challenge faced:**
The most challenging part of the assignment was ensuring consistent text normalization. Words in the text could have different cases, punctuation, or whitespace, and I needed to make sure both the dictionary and the analyzed text were normalized the same way for accurate spell checking.

**How you solved it:**
I used the `NormalizeWord` helper method that trims whitespace, removes punctuation using Regex, and converts words to lowercase using `ToLowerInvariant()`. I applied this both when loading the dictionary and when analyzing the text. This ensured all comparisons were consistent and avoided false misspellings.

**Most confusing concept:**
Understanding the relationship between normalized text and dictionary lookups was initially confusing. I also had to carefully remember that `HashSet.Contains()` is case-sensitive unless you either normalize words or use `StringComparer.OrdinalIgnoreCase`.
## Code Quality

**What you're most proud of in your implementation:**
I’m proud of the consistent normalization strategy and the clean categorization logic using HashSets. The code gracefully handles missing files, duplicates, and maintains accurate counts of correctly spelled versus misspelled words.

**What you would improve if you had more time:**
I would expand the dictionary with a more comprehensive word list to reduce false positives and possibly implement more advanced tokenization that handles hyphenated words, contractions, and numbers more accurately.

## Testing Approach

**How you tested your implementation:**
I manually tested each feature by analyzing multiple text files with known correctly spelled and intentionally misspelled words.

**Test scenarios you used:**
- Words with different capitalization (e.g., `Dog` vs `dog`)  
- Words with punctuation attached (e.g., `fox.` or `jumps!`)  
- Words repeated multiple times to test counting logic  

**Issues you discovered during testing:**
The biggest issue was that some words were incorrectly categorized as misspelled because the dictionary file was small. I also had to make sure `NormalizeWord` was applied consistently to avoid false negatives.

## HashSet vs List Understanding

**When to use HashSet:**
HashSet is ideal when you need fast membership testing and automatic uniqueness, like checking if a word exists in a dictionary or storing unique words from a text.

**When to use List:**
List is more appropriate when the order of elements matters, duplicates are allowed, or you need to preserve all occurrences of words, such as `allWordsInText` which tracks every word including duplicates.

**Performance benefits observed:**
Using HashSet allowed O(1) lookups and automatic removal of duplicates, making the categorization of words extremely efficient even with larger texts.

## Real-World Applications

**How this relates to actual spell checkers:**
This assignment mirrors the behavior of real-world spell checkers like Microsoft Word and Google Docs, which must quickly check each word against a dictionary and provide feedback on spelling errors.

**What you learned about text processing:**
I learned the importance of normalization, tokenization, and handling edge cases such as punctuation and varying capitalization. Consistency is key to accurate text analysis.

## Stretch Features

None implemented.

## Time Spent

**Total time:** 6 hours

**Breakdown:**
- Understanding HashSet concepts and assignment requirements: 1 hour
- Implementing the 6 core methods: 2 hours
- Testing different text files and scenarios: 1.5 hours
- Debugging and fixing issues: 1 hour
- Writing these notes: 0.5 hours

**Most time-consuming part:** Text normalization and ensuring consistent comparisons between dictionary words and analyzed text was the most time-consuming aspect.

## Key Learning Outcomes

**HashSet concepts learned:**
I learned how O(1) performance, automatic uniqueness, and set-based operations can dramatically simplify text analysis tasks like spell checking and word categorization.

**Text processing insights:**
I gained practical experience with normalization, tokenization, and using Regex to clean text. I also learned how subtle differences in capitalization or punctuation can affect text processing.

**Software engineering practices:**
I reinforced the importance of defensive programming, error handling, and consistent helper methods. I also improved my approach to structuring code for clarity and maintainability.