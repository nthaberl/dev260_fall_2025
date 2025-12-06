# Assignment 10: Flight Route Network Navigator - Implementation Notes

**Name:** [Natascha]

## Graph Data Structure Understanding

**How adjacency list representation works for flight networks:**  
*[Explain your understanding of how Dictionary<string, List<Flight>> provides O(1) airport lookups, efficient sparse graph storage, and why this is better than adjacency matrix for flight networks with 16 airports and 52 flights]*

Answer: Dictionary<string, List<Flight>> serves as the adjacency list, where the keys are the aiport codes and the values are lists of flights from those aiports. Since the list is stored in a dictionary, airport lookups are naturally O(1). This is more efficienct in this scenario, because not every airport is connected to every other airport. An adjacency matrix would require enough space to store flights between every pair of airports, resulting in 256 flights instead of the 52 used in the data set. 

**Difference between BFS and Dijkstra's algorithms:**  
*[Explain when to use BFS (shortest path by hops) vs Dijkstra's (shortest path by cost), and how each algorithm guarantees finding optimal paths]*

Answer: BFS is used on unweighted graphs or when all the edges are of equal weight. Traverses a graph layer by layer, using a queue to keep track of which vertices to visit next while also keeping track of which vertices have already been visited. When the last node is reached, it is guaranteed to be the shortest, otherwise it would have been visited already in an earlier layer. Dijkstra's is used when edges have weight and maintains a priority queue of vertices to explore paths with the lowest cost first, so that when it visits a node, the lowest cost path has been found.

## ONLY IMPLEMENTED METHODS GONE OVER IN CLASS FOR LAB ##

## Challenges and Solutions

~~**Biggest challenge faced:**
*[Describe the most difficult part of the assignment - was it implementing BFS traversal, Dijkstra's priority queue logic, path reconstruction from parent maps, or understanding graph algorithms?]*~~

Answer:

~~**How you solved it:**  
*[Explain your solution approach and what helped you figure it out - research, drawing graphs on paper, debugging with breakpoints, testing with simple examples, etc.]*~~

Answer:

~~**Most confusing concept:**
*[What was hardest to understand about graph traversal, queue/priority queue usage, parent map path reconstruction, or algorithm termination conditions?]*~~

Answer:

## Algorithm Implementation Details

~~**BFS Implementation (FindRoute and FindShortestRoute):**
[Describe how you implemented the queue-based traversal, visited tracking with HashSet, parent map for path reconstruction, and why BFS guarantees shortest path in unweighted graphs]~~

Answer:

~~**Dijkstra's Implementation (FindCheapestRoute):**
[Explain how you used PriorityQueue<string, decimal>, implemented the relaxation step, tracked distances, and reconstructed the cheapest path]~~

Answer:

~~**Path Reconstruction Logic:**
[Describe your approach to building the final route from the parent map, handling the reverse traversal, and ensuring the path goes from origin to destination]~~

Answer:

## Code Quality

~~**What you're most proud of in your implementation:**
[Highlight the best aspect of your code - maybe your clean BFS implementation, efficient Dijkstra's algorithm, well-structured network analysis methods, or thorough error handling]~~

Answer:

**What you would improve if you had more time:**
[Identify areas for potential improvement - perhaps optimizing priority queue usage, adding more comprehensive error handling, implementing bidirectional search, or adding visualization features]

Answer: I would love to flesh out the methods, will tackle it during winter break!

## Real-World Applications

**How this relates to actual routing systems:**
[Describe how your implementation connects to real-world systems like Google Flights, Google Maps navigation, social network friend suggestions, or internet packet routing]

Answer: This implementation is a simplified version of other real world tools, Google flights being the most similar (helping people find shortest or cheapest routes between cities/airports). Google Maps navigation is similar in that it helps find the best way to get from point a (a vertex) to point b (another vertex). Social networks provide suggestions by looking at existing connections between people, and the internet routes packets between routers/devices and tries to find the best path to do so (fastest/most reliable/safest).

**What you learned about graph algorithms:**
[What insights did you gain about graph traversal techniques, the power of BFS and Dijkstra's for different optimization goals, and how adjacency lists make sparse graphs efficient?]

Answer: I learned **a lot** about graphs between lectures, outside resources, and this assignment as this was my first exposure to them ever. I learned that they are more of an abstract structure but helpful in many different scenarios. The different algorithms and different structures for adjacency shows how flexible this structure can be. 

## Testing and Verification

~~**Test cases you created:**
[List the specific test scenarios you used - which airport pairs did you test? Did you verify shortest vs cheapest routes differ? How did you test edge cases like disconnected airports or origin=destination?]~~

Answer:

~~**Interesting findings from testing:**
[Describe any surprising results - routes that took unexpected paths, cost vs stops tradeoffs you discovered, or hub airports you identified]~~

Answer:

## Optional Challenge

[If you implemented the optional FindRoutesByCriteria method with DFS and constraints, describe your approach here. If not, write "Not implemented - focused on core requirements"]

Answer: Not implemented - only completed the lab portion

## Time Spent

**Total time:** [X hours]

**Breakdown:**

- Understanding graph concepts and assignment requirements: [X hours]
- Implementing basic search operations (TODO #1-3): [X hours]
- Implementing BFS pathfinding (TODO #4-5): [X hours]
- Implementing Dijkstra's algorithm (TODO #6): [X hours]
- Implementing network analysis (TODO #8-10): [X hours]
- Testing with flights.csv and edge cases: [X hours]
- Debugging graph traversal algorithms: [X hours]
- Writing these notes: [X hours]

~~**Most time-consuming part:** [Which aspect took the longest and why - understanding Dijkstra's algorithm, debugging path reconstruction, implementing priority queue logic, etc.]~~

## Key Takeaways

**Most important lesson learned:**
[What's the single most valuable insight you gained from this assignment about graph algorithms, pathfinding, or algorithm design?]

Answer:

**How this changed your understanding of data structures:**
[How did working with graphs expand your perspective on data organization compared to arrays, linked lists, trees, etc.?]

Answer:
