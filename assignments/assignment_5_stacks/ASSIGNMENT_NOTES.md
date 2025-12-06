# Assignment 5: Browser Navigation System - Implementation Notes

**Name:** Natascha

## Dual-Stack Pattern Understanding

**How the dual-stack pattern works for browser navigation:**
[Explain your understanding of how the back and forward stacks work together to create browser-like navigation]
The two stacks are tracking two different directions in navigation. One stack, backStack will be filled as a user navigates different pages (webpages are pushed into the stack).
The second stack comes into play when the user backtracks. Back tracking is equivalent to popping a website from backStack, but as it is popped, it is added to the forwardStack.
forwardStack maintains all the websites that were popped, so when the user wishes to revisit by going "forward", sites are popped from forwardStack and added back to backStack.

## Challenges and Solutions

**Biggest challenge faced:**
[Describe the most difficult part of the assignment]
The largest challenge I faced was writing out the method for VisitUrl(). I was trying to set the individual properties of currentPage with the arguments passed in.

**How you solved it:**
[Explain your solution approach and what helped you figure it out]
I realized I needed a better understanding of how the program was put together. 
I familiarized myself with the WebPage class and realized I actually needed to instantiate a WebPage when calling VisitUrl(). 

**Most confusing concept:**
[What was hardest to understand and how you worked through it]
The names of the stacks threw me off. I was imagining a user going "forward" as they navigated to different websites, but the sites needed to be stored in backTrack.
This led to confusion in the beginning. I had to remember that the stack is being used for the *ability* to go back, hence the name backTrack.

## Code Quality

**What you're most proud of in your implementation:**
[Highlight the best aspect of your code]
With the provided instructions the logic wasn't too complex, however I'm proud of the clear, straightforward code I've written.

**What you would improve if you had more time:**
[Identify areas for potential improvement]
If I had more time I would push for the stretch goals, however the workload in my other classes prevents me from attempting them. 

## Testing Approach

**How you tested your implementation:**
[Describe your overall testing strategy]
I tested often. Every time I completed a method I would test.

**Issues you discovered during testing:**
[Any bugs or problems you found and fixed]
The biggest bugs were found during the biggest challenge I described earlier.
I was using the StackStarter from Lab 5 for reference, so when writing out the foreach loop for display histories, I had an issue with my numbering.
The numbers were decrementing for each url, when they should have been incrementing. This was an easy fix.

## Stretch Features

[If you implemented any extra credit features, describe them here. If not, write "None implemented"]
**none implemented**


## Time Spent

**Total time:** [2 hours]

**Breakdown:**

- Understanding the assignment: [.5 hours]
- Implementing the 6 methods: [1.5 hours]
- Testing and debugging: [0 hours] *(done concurrently while implementing 6 methods)*
- Writing these notes: [.5 hours]

**Most time-consuming part:** [Which aspect took the longest and why]
