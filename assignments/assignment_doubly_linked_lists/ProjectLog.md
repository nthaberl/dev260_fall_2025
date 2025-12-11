# Step-by-Step Log with Challenges and Commentary

## Part A

### Step 1
Already implemented

### Step 2
Already implemented

### Step 3 - Add Methods
Where the fun begins! 
It's been a very long time since I've worked with lists.
Several years ago I created a similar program using JS to implement a Singly Linked List.
This time it was still important to keep track of a next pointer as well as a *previous* pointer. Doubly Linked Lists also use tails, not just heads
The links provided to GeeksforGeeks were helpful in visualizing the data and providing guidance.
The step-by-step instructions for each method were extremely helpful too! I ran the program often to see if I had any compiler issues. My previous JS Singly Linked List program reminded me that if the head == null, then the list is empty.

While building out AddFirst() and AddList() I had some compiler errors that were easy fixes (issues with syntax).

I had a little hiccup with Insert(), as I didn't know how I wanted to validate the data. I learned about exceptions in a previous class, reviewed my notes, and tried IndexOutOfRange as it can be used to validate indices in array *and* collections. Perfect.
While working through the rest of the method I saw that I had to implement the GetNodeAt() method. I followed the step-by-step instructions there. Ran into a scope issue where I initialized the "current" variable inside of each else/if instead of at the beginning of at the top of the method. Tried declaring "var current;", compiler didn't like that either. Declared "var current = head;" Compiler happy.

Went back to Insert(). Updating the pointers was challenging and I had a hard time wrapping my mind around it. I reread the GeeksforGeeks material and drew out a diagram to visualize what was happening. I tested a few times with data. Also ran into some issues with validating the index range, as the steps said "count inclusive" so I wrote index >= count. This was an easy fix.

### Step 4 - Traversal and Display
Had an issue where traversing showed the same number over and over in my console. Went back to my code, forgot an iterator in my while loops, but wasn't quite sure how to iterate? Referred to GeeksforGeeks for examples in traversals. 

ToArray() was interesting. Tried to initialize an int type array. Compiler threw an error and then I realized it needed to be a "T" type. Did some Googling to understand how to write out the method, StackOverflow was helpful. 

### Step 5 - Search Methods
Traversed with a while loop in Contains() like I did in the display methods, but ran into issues trying to compare the data, as I was running into compiler issues again ("==" does not work!). StackOverflow was helpful again. Asked ChatGPT to clarify the reasoning and what made the compiler upset. Since T is a generic type, and the compiler doesn't know what T will be, it doesn't allow a simple comparison operator. I was reminded of .Equals() which would have worked fine here, except it would have thrown a NullReferenceException in the event that one of the items was null. Likely not an issue in the program but an important disctinction to note anyway. Ran into an issue with IndexOf(), as I forgot to iterate the list AND index. Otherwise, step-by-step instructions made it clear how to flesh out the methods.

### Step 6 - Remove Methods
For RemoveFirst(), my previous notes didn't cover what type of exception to throw in a situation like this, did some Googling, Gemini was helpful in suggesting ArgumentNullException. Had issues testing this out as the program doesn't let you remove items from an empty list. Rest of the remove methods were easier to build out with step by step instructions.

### Step 7 - Advanced Operations
Clear() was very easy to write out, but struggled with the reversing operations. Drew diagrams, walked through GeeksforGeeks illustrations, Googled, my mind couldn't grasp it. I had been bending my mind for awhile at this point. Used AI to help break down the process and work towards solution.

## Part B

### Step 8 - Song Class
Already implemented

### Step 9 - PlayList Core Structure
Already implemented

### Step 10 - Playlist Management
Had to refer to DoublyLinkedList assignment to remember method names
Mimicked Insert() for AddSongAt().
For RemoveSong(), didn't understand what "If the song being removed is the current song, handle current song update" was asking, asked AI for a plain english explanation. Felt silly afterwards because it made sense what was being asked. Tried to call .Find() but the compiler threw an error saying that 'Find' wasn't in the current context, which left me confused. Looked through the using statements, they looked fine.. looked through the constructor and realized "playlist" was calling the DoublyLinkedList, meaning I would need to use playlist.Find(), not just Find(). Then compiler got upset about 'cannot convert from 'Week4DoublyLinkedLists.Core.Node<Week4DoublyLinkedLists.Applications.Song>' to 'Week4DoublyLinkedLists.Applications.Song'.' I could tell there was a type mismatch but needed help untangling where the mismatch was happening and asked AI to interpret. The explanation made it clear, Find() is returning the node itself but Remove() is looking for the data *in* the node. I tried to test the method but the console let me know I still had more work to do! I decided to skip to Step 11 so I could have a visual confirmation that the methods in step 10 were working.

### Step 11 - Display and Basic Management

DisplayPlaylist() - Had to look up foreach syntax as a refresher on W3. "foreach (type variableName in arrayName)" means foreach (Song song in playlist). I've never actually used an index with a foreach, but my instincts said to intialize an index outside of the loop (like a while loop). I also looked up the song properties and found ToString. The method worked but output wasn't quite right (each song was "1 System.Func`1[System.String]"). Tried song.ToString(), IDE prompted a simplification by just using "song". To find current song, I tried to match song to currentSong. Through a compiler error, realized currentSong points to a node, so I had to match song to currentSong.Data. 
DisplayCurrentSong() wasn't too bad to implement, a compiler error said 'Node<Song>' does not contain definition for 'ToDetailedString'' - realized I pointed at the node *again* and not the data. currentSong.Data.ToDetailedString fixed it. Saw that I needed to use the GetCurrentPosition method to fully display all the requested data. GetCurrentSong() was easy to implement. Back to Step 10b.

### Step 10 (Again)
RemoveSongAt() - I tried to use GetNodeAt() from DoublyLinkedList but got a compiler error that it was private. I could make it public but opted to keep it private since it was part of the starter code. So I decided to use GetNodeAt() as a blueprint to write RemoveSongAt(). The instructions for checking if it was the current song were almost the same as in RemoveSong, so I used that as a blueprint to finish out the method. I did some testing with the data to ensure it worked. 

Navigation: This was fairly straightforward. First ensuring that there was a currentSong, and if so, checking if it has a .Next property, if yes, then assign the currentSong to currentSong.Next and return true, otherwise return false. I originally wrote it out with 2 if checks, but noticed this was redundant. The if check can check if both are !=null with && operator, and if not, the method will just return false.
Same implementation used for Previous(), just working with previous pointers instead. Tested out functionality to make sure they both worked as intended.

For JumpToSong(), decided to use the RemoveSongAt() method for inspiration, but instead of removing the song I'd be setting currentSong to the current variable in the method. For position validations I've been throwing exceptions, however the method asked to return true or false if the method was completed, so an invalid position returns false to satisfy method requirements. 

Went to test the functionality of the music player and realized... RemoveSong() was not actually removing any songs. The song would still show up even though I had a confirmation message that the song was removed meaning the method was working but something else was happening. Looked through the method and noticed I was returning true before the method had finished executing. Ensured the method was returning true after the removal operation, the program compiled smoothly, but now I got a new error: "🚧 RemoveSong not implemented: TODO: Helper - Implement RemoveNode helper method". I didn't realize I was missing this helper method alllll the way back on the DoublyLinkedListFile! Following the instructions was fairly straightforward, but referenced GeekforGeeks (and drew my own diagram) for help with reassigning the pointers in removing a middle node.

After completing Parts A and B, realized Part A has an option for "Comprehensive Testing". This would have helped prevent the issues I ran into further down the line when I tried to implement RemvoeSong().

# Challenges Faced
One challenge I faced was iterating through the Doubly Linked List; it's not like an array where you can use the iterator (index++), you have to use your node's pointer to point to the next node. This made me think of the implementations of these structures under the hood. Since arrays are stored contiguously, it's easy to just call the iterator, because it works continguously along the memory address for the array. Doubly Linked Lists are *not* stored the same way, nodes are scattered all over memory, so traversal happens by following the Next pointer. 

Another challenge I faced was untangling where pointers needed to point after adding or removing a node in the **middle** of the list. Accomplishing this at the head or tail wasn't as much of an issue, but it was easy to get turned around on where the pointers needed to point after the operation.

A final challenge I faced was keeping track of what type of data I was working with when completing methods. A node contains two pointers and data, sometimes I only needed to work with the data itself, not the pointers. 

# Performance Reflection
DoublyLinkedLists are great in that they allow for fast insertions and removals, as long as the position is known. Only pointers have to be updated, unlike other structures where the data is stored contiguously and large portions of data have to be moved around. Data also does not have to be pre-allocated, nodes can be added and removed, whereas something like an array is initialized with a set size. If the size is too small it won't hold all the data, but too large and extra memory is wasted for indexes not holding any data. 

Iterating through doubly linked lists are not as efficient for the reason mentioned above. Each node also takes up more memory as it holds the data as well as pointers. Cache performance is also not as great in comparison to a contiguously stored data structure since the CPU has to follow pointers. With contiguous structures, the CPU can take advantage of cache locality since the memory address of the next item is stored right next to it. The CPU may fetch the next several elements when fetching one. 

DoublyLinkedLists are ideal for situations where the size of the collection frequently changes, and insertions/removals at known positions are also frequently needed. These operate at O(1) when the position is known. Array-like structures require O(n) for insertions or removals (unless at the end with only takes O(1)).



