# Assignment 6: Game Matchmaking System - Implementation Notes

**Name:** Natascha

## Multi-Queue Pattern Understanding

**How the multi-queue pattern works for game matchmaking:**  
*[Explain your understanding of how the three different queues (Casual, Ranked, QuickPlay) work together and why each has different matching strategies]*  
The queues are separated so they can implement their own matching logic.  
Trying to accomplish the same matchmaking rules with one queue would be messy and cluttered and really challenging to maintain.  
It would also slow down the system trying to find players in the queue with the same matchmaking preference.  

## Challenges and Solutions

**Biggest challenge faced:**  
*[Describe the most difficult part of the assignment - was it the skill-based matching, queue management, or match processing?]*  
Implementing the ranked matchmaking was the most difficult and I spent the most time fleshing out the TryCreateMatch method.

At another point the entire MatchMakingSystem.cs had red squiggles for EVERY instance of Player and Match. I'm not sure what happened,  
I thought I had to backtrack my entire project. I restarted my IDE, ran it again and it just sorted itself out.  

**How you solved it:**  
*[Explain your solution approach and what helped you figure it out]*  
Initially I was trying to figure out *how* to match the players based on their levels, but realized that there was already a helper method provided to handle the calculation.  
With that out of the way, I decided to store the queue into an array because it's easier to work with the indices of an array.  
There was a lot of investigating on how to search through queues and work with values within queues.  
This is how I implemented an array in my solution.

**Most confusing concept:**  
*[What was hardest to understand about queues, matchmaking algorithms, or game mode differences?]*  
I was most confused how everything in the program worked together from just the starter code.  
Once I started implementing the methods it started to come together and make more sense.  


## Code Quality

**What you're most proud of in your implementation:**  
*[Highlight the best aspect of your code - maybe your skill matching logic, queue status display, or error handling]*  
The ranked matchmaking! There may be a simpler or more efficient way to implement it but I was happy that it worked out.


**What you would improve if you had more time:**  
*[Identify areas for potential improvement - perhaps better algorithms, more features, or cleaner code structure]*  
Possibly a cleaner code structure and more DRY code. Some snippets are repeated within methods and it would be nice to avoid that  
for easier maintenance in the future. If I had a lot more time I might rebuild it so that the queue was an interface,  
and create the different modes of matchmaking queues with instances of the original interface.  
This would make it easier to implement different styles of matchmaking in the future.


## Testing Approach

**How you tested your implementation:**  
*[Describe your overall testing strategy - how did you verify skill-based matching worked correctly?]*  
First step is to ensure there are no compiler errors! After that I tested the program often, even when methods weren't completely finished.  
With skill-based I added all the different players into the matchmaking queue to see how it would play it. I did the same with quickplay queue.

**Test scenarios you used:**  
*[List specific scenarios you tested, like players with different skill levels, empty queues, etc.]*  
I used many scenarios. Empty queues, queues with different players of different ranks, queues with players of the same rank, odd number of players,  
even number of players, players that shouldn't match, using custom players, etc. I tried to come up with as many scenarios as possible.  
I only used small sets of players though.  
I didn't try to fill the queue with hundreds or thousands of players.

**Issues you discovered during testing:**  
*[Any bugs or problems you found and fixed during development]*  
One bug I noticed was adding a player to the same queue twice. I wanted to avoid this, so I added a guard clause and an error message.  
During testing, the player wouldn't be added if they were already in the queue BUT GameNavigator.cs was providing a confirmation message that they were added.  
I double checked the queue to verify they weren't added twice, so I implemented an exception to mitigate this issue.


## Game Mode Understanding

**Casual Mode matching strategy:**  
*[Explain how you implemented FIFO matching for Casual mode]*  
FIFO is the baseline functionality of a queue. Dequeue pulls the first (oldest) items in the queue.  
After players are enqueued, dequeue pulls them out in the order they were entered.

**Ranked Mode matching strategy:**  
*[Explain how you implemented skill-based matching (±2 levels) for Ranked mode]*  
Luckily there was already a helper method included to match players that were ±2 levels.  
I also copied the queue into an array to make iterating and comparing easier.

**QuickPlay Mode matching strategy:**  
*[Explain your approach to balancing speed vs. skill matching in QuickPlay mode]*  
From my understanding, Quickplay matches FIFO if there are 4 or more players in the queue, otherwise it does skill based matching.  
I implemented the logic from both casual mode and ranked based on the number of people in the quickplay queue.


## Real-World Applications

**How this relates to actual game matchmaking:**  
*[Describe how your implementation connects to real games like League of Legends, Overwatch, etc.]*  
I don't play many multiplay online games but many of them utilize this style of matchmaking. Casual is nice if you just want to hop in and play.  
Ranked is nice if you want to play with people within your skill level and rank up!

**What you learned about game industry patterns:**  
*[What insights did you gain about how online games handle player matching?]*  
Since I've mostly coded web apps I haven't put much thought into how games are built and what data structures they might utilize,  
but this assignment provided insight on how complex something like matchmaking can get.


## Stretch Features  
*[If you implemented any extra credit features like team formation or advanced analytics, describe them here. If not, write "None implemented"]*  
**none implemented**


## Time Spent

**Total time:**  
*[~6-7 hours]*  
Hard to pinpoint how long it actually took because I took many breaks and worked on assignments for other classes while completing this one.  
But I would say that 6-7 hours is a safe estimate. 


**Breakdown:**
It's challenging to say how long each item took, as many of them were done concurrently.
- Understanding the assignment and queue concepts: [.5 hours]
- Implementing the 6 core methods: [2 hours]
- Testing different game modes and scenarios: [2 hours]
- Debugging and fixing issues: [1 hours]
- Writing these notes: [1 hour]

**Most time-consuming part:**  
*[Which aspect took the longest and why - algorithm design, debugging, testing, etc.]*  
Testing took the longest as it was time consuming trying to build different scenarios to ensure program was working as it should  
Everytime a portion of a method was implemented I test it.  
I implemented the casual logic of several methods first and tested them before completing the ranked and quickplay logic.


## Key Learning Outcomes

**Queue concepts learned:**  
*[What did you learn about managing multiple queues and different processing strategies?]*  
Maintaining seperate queues allows you to implement different rules and logic for each each queue allowing for a variety of matchmaking styles.  
Even though we only implemented basic queues, with logic it was easy to utilize them in ways other than FIFO. 

**Algorithm design insights:**  
*[What did you learn about designing matching algorithms and handling different requirements?]*  
I learned a lot about matching algorithms but not sure if I implemented it in the most *efficient* way.  
I'm sure there are more efficient ways but writing a robust method helps me understand and see how the logic is working. 

**Software engineering practices:**  
*[What did you learn about error handling, user interfaces, and code organization?]*  
Many of the errors were already handled in the starter code, but I tried hard to make sure the output was legible.
I also tried hard to keep the code organized to ensure that I wouldn't get lost in it.  