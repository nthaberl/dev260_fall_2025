# Assignment 8: Spell Checker & Vocabulary Explorer - Implementation Notes

**Name:** [Natascha]

## HashSet Pattern Understanding

**How HashSet<T> operations work for spell checking:**  
*[Explain your understanding of how O(1) lookups, automatic uniqueness, and set-based categorization work together for efficient text analysis]*  
The automatic uniqueness and membership checks allow for extremely fast and efficient lookups instead of iterating through entire sets. The set-based categorization helps the SpellChecker to quickly organize and analyze the text based on the method.

## Challenges and Solutions

**Biggest challenge faced:**  
*[Describe the most difficult part of the assignment - was it text normalization, HashSet operations, or file I/O handling?]*  
Using the File class for I/O handling, that was something I've never had to deal with before.  
I also had to learn how to implement .Take(), that was a method I've never used before. 


**How you solved it:**  
*[Explain your solution approach and what helped you figure it out]*  
I referred to the Microsoft documentation and StackOverflow for examples

**Most confusing concept:**  
*[What was hardest to understand about HashSet operations, text processing, or case-insensitive comparisons?]*  
The most confusion I had was trying to understand why so many correctly spelled words were categorized as misspelled.
Then I realized the "dictionary" the words were being compared to was pretty limited! 

## Code Quality

**What you're most proud of in your implementation:**  
*[Highlight the best aspect of your code - maybe your normalization strategy, error handling, or efficient text analysis]*  
GetMisspelledWords and GetUniqueWordsSample came out more cleanly than I would have written in the past

**What you would improve if you had more time:**  
*[Identify areas for potential improvement - perhaps better tokenization, more robust error handling, or additional features]*  
I would have used the NormalizeWord helper method instead of manually doing it in each method.  
I was conflicted because AnalyzeTextFile asked for it to be done manually, and subsequent methods asked for words to be normalized in the same manner

## Testing Approach

**How you tested your implementation:**  
*[Describe your overall testing strategy - how did you verify spell checking worked correctly?]*  
As I implemented each method I tested as much as I could. As more methods were being implemented, I printed the Sets and List to ensure they were populating correctly. I tried using methods that shouldn't work (like Categorize before a Dictionary has been loaded) to ensure everything was working as it should. 

**Test scenarios you used:**  
*[List specific scenarios you tested, like mixed case words, punctuation handling, edge cases, etc.]*  
I tested mixed case words, punctuation, extra spaces at the beginning or end, and tested cases like trying to categorize words before analyzing text.  

**Issues you discovered during testing:**  
*[Any bugs or problems you found and fixed during development]*  
I ran into a bug when trying to load a file to be analyzed. I would get a confirmation message that text was loaded to be analyzed but would run into issues when I tried to categorize the word. I looked through the navigator and spellchecker to see what was happening after adding a text file to be analyzed. I realized that HasAnalyzedText was never returning true, thus causing my issues.
In AnalyzeTextFile I set currentFileName = fileName and it resolved my issues.  
Another bug I found was that even if words haven't been categorized, #5 will return a statement saying that there were no misspelled words after being analyzed which isn't really true.


## HashSet vs List Understanding

**When to use HashSet:**  
*[Explain when you would choose HashSet over List based on your experience]*  
When looking to see if a value exists at all in the set (checking membership) and order does not matter.


**When to use List:**  
*[Explain when List is more appropriate than HashSet]*  
When a value exists but can also exist multiple times. AllWordsInText was used to count the occurence of words.


**Performance benefits observed:**  
*[Describe how O(1) lookups and automatic uniqueness helped your implementation]*  
Being able to use .Contains() instead of using loops was quicker to write and works a lot more quickly.
Iterating through an entire collection to find one instance is time consuming.


## Real-World Applications

**How this relates to actual spell checkers:**  
*[Describe how your implementation connects to tools like Microsoft Word, Google Docs, etc.]*  
This implementation is a lot more simple than word processors that also handle punctuation and case.
But it helped me understand how it can be done efficiently, instead of iterating through an entire dictionary on the backend,  
it can just lookup the word with O(1) efficiency.


**What you learned about text processing:**  
*[What insights did you gain about handling real-world text data and normalization?]*  
It's been awhile since I used Regex but I was reminded how flexible it can be to help find (or filter) characters.  
Extremely helpful in text normalization.


## Stretch Features

[If you implemented any extra credit features like vocabulary suggestions or advanced analytics, describe them here. If not, write "None implemented"]
Regex was mentioned as being a stretch goal in AnalyzeTextFile but unsure if it counts. 

## Time Spent

**Total time:** [4.5 hours]

**Breakdown:**
- Understanding HashSet concepts and assignment requirements: [.5 hours]
- Implementing the 6 core methods: [2 hours]
- Testing different text files and scenarios: [.5 hours]
- Debugging and fixing issues: [.5 hours]
- Writing these notes: [1 hours]

**Most time-consuming part:**  
*[Which aspect took the longest and why - text normalization, HashSet operations, file I/O, etc.]*  
Getting GetMisspelledWords() to work. I was having compiler issues with .Take() and couldn't figure out how to resolve it.
After reading documentation and going through StackOverflow, learned that it returns an IEnumerable, when I needed to return a list. Luckily it was easy to fix!

## Key Learning Outcomes

**HashSet concepts learned:**  
*[What did you learn about O(1) performance, automatic uniqueness, and set-based operations?]*  
The automatic uniqueness was really handy in this scenario, no extra steps were needed to deal with duplicates and this is inherent to HashSets.  
Also very quick and easy to do membership checks in comparison to other data structures were iterating is necessary.

**Text processing insights:**  
*[What did you learn about normalization, tokenization, and handling real-world text data?]*  
I learned what tokenization meant. I've split and normalized text many times before but never learned a term for it.

**Software engineering practices:**  
*[What did you learn about error handling, user interfaces, and defensive programming?]*  
I'm reminded how defensive programming is a good habit to build; you can't assume a program will behave the way you intend it to,  