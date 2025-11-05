# Assignment 6: Game Matchmaking System - Implementation Notes

**Name:** Jarod Atienzo

## Multi-Queue Pattern Understanding

**How the multi-queue pattern works for game matchmaking:**

- Casual queue is used when any two players want to match up (FIFO).
- Ranked Queue can only be played when two players within the +2 skill level of each other are able to match.
- Quick play queue preferes skill matching, but will allow anyone to match up if there is 4 or more players in the queue. 

## Challenges and Solutions

**Biggest challenge faced:**
The biggest challenge I faced in this assignment was implementing the logic inside the `TryCreateMatch()` method. It was difficult to figure out how to handle ranked queues when the top two players were not within the same skill range.

**How you solved it:**
To solve this, I created a separate list that contained all players in the queue. I then searched for two players who could match based on their skill levels, removed them from the queue, and rebuilt the queue using the updated list. This allowed me to cleanly separate matching logic from queue management.

**Most confusing concept:**
The most confusing part of this assignment was understanding how to use the existing classes and objects effectively when building new methods. It took some time to see how everything connected, but once I understood the relationships, I was able to create helper methods that improved reusability and made the code flow much smoother.
## Code Quality

**What you're most proud of in your implementation:**
[Highlight the best aspect of your code - maybe your skill matching logic, queue status display, or error handling]
For me, error handling and creating helper methods are what stood out to me. There were times where I realized that code was getting repetative and needed a helper method for readability. 

**What you would improve if you had more time:**
I think there are better ways to for me to approach match making in the `TryCreateMatch()` helper method. There is probably a better way to match players in ranked without having to turn the queue into a list. 

## Testing Approach

**How you tested your implementation:**
After finishing each method, I tested all possible edge cases to make sure the matchmaking logic worked correctly.

**Test scenarios you used:**
- I checked how the system behaved when there were fewer than two players in the queue, when players had large skill differences, and when multiple players had similar skill levels

**Issues you discovered during testing:**
- Syntax (mostly)
- Logic for ranked queue

## Game Mode Understanding

**Casual Mode matching strategy:**
The first two players who enter the queue are the first ones to be matched together, regardless of their skill level. I called `Dequeue`() twice to remove the first two players. After that, I called their `LeaveQueue`() methods and created a new Match object with those two players.

**Ranked Mode matching strategy:**
For Ranked mode, I implemented a skill-based matching system that pairs players whose skill ratings are within +2 levels of each other. I first converted the `rankedQueue` into a list so I could use nested for loops to compare every possible pair of players. If I found two players whose skill ratings met the condition `Math.Abs(player1.SkillRating - player2.SkillRating) <= 2`, I removed them from the queue using a helper method called `RemovePlayersFromQueue()`. Then, I called their `LeaveQueue()` methods and created a new Match object. This ensures that players are matched fairly based on skill, keeping the competition balanced.

## Real-World Applications

**How this relates to actual game matchmaking:**
This helped me understand how real-world games like Overwatch or Apex Legends adjust their matchmaking systems depending on the game mode and player base to keep players engaged and satisfied. Just like Overwatch, this has a quick play queue and a competative (ranked) queue.


**What you learned about game industry patterns:**
learned that online games use different matchmaking patterns to balance fairness, skill level, and wait times. For example, ranked modes often use strict skill-based algorithms to create competitive and balanced matches, while casual and quick play modes prioritize speed and accessibility. 

## Stretch Features
None

## Time Spent

**Total time:** [8 hours]

**Breakdown:**

- Understanding the assignment and queue concepts: [1 hours]
- Implementing the 6 core methods: [4 hours]
- Testing different game modes and scenarios: [1 hours]
- Debugging and fixing issues: [1 hours]
- Writing these notes: [1 hours]

**Most time-consuming part:** 
`TryCreateMatch()` and `DisplayPlayerState()`

## Key Learning Outcomes

**Queue concepts learned:**
I learned how to manage multiple queues by giving each one its own matching strategy based on the game mode. For example, the Casual queue used a simple FIFO approach, while Ranked required skill-based matching within a specific range, and QuickPlay balanced both speed and fairness.


**Algorithm design insights:**
I learned the advanced and basics concepts of queues (FIFO) and how to this is used in real life matchmaking systems. 

**Software engineering practices:**
I learned the importance of writing clean, organized code that’s easy to read and maintain. Using helper methods like RemovePlayersFromQueue() improved reusability and reduced repetition across different game modes. I also learned to handle potential errors gracefully, such as checking if queues had enough players before creating matches. This assignment helped me understand how proper structure, clear method responsibilities, and consistent naming make debugging and future updates much easier.
