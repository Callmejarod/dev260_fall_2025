# Team Manager App

> A console-based fantasy basketball team manager that allows users to manage a roster, claim players from waivers, and drop players back into the waiver wire.

---

## What I Built (Overview)

**Problem this solves:**  
This application simulates a simplified fantasy basketball team management system. It allows a user to manage their team roster by claiming players from a waiver wire and dropping players back into it. The app models real-world fantasy sports workflows while demonstrating effective use of core data structures and clean application design.

**Core features:**  
- Create and manage a user-controlled team

- View available players on the waiver wire

- Claim players from waivers

- Remove players from a team and return them to waivers

- Prevent duplicate players and invalid operations through validation

## How to Run

**Requirements:**  
- .NET SDK 7.0 or later
- Windows, macOS, or Linux
- Console/terminal access

```bash
git clone https://github.com/Callmejarod/dev260_fall_2025
cd final/TeamManagerApp
dotnet build
```

**Run:**  

```bash
dotnet run
```

**Sample data (if applicable):**  
Sample basketball players are loaded into the waiver wire at application startup via in-memory initialization. No external files or database setup is required as of yet. 

---

## Using the App (Quick Start)

**Typical workflow:**  
_Describe the typical user workflow in 2–4 steps._

**Your Answer:**

1. Start the application and create a manager/team.
2. View the waiver wire to see available players.
3. Enter a player ID to claim a player onto your team.
4. Remove players from your team to place them back on waivers.

**Input tips:**  
- Manager names are case-insensitive.

- Player selection is done using numeric Player IDs.

- Invalid inputs (non-numeric IDs, unavailable players, duplicates) are handled with clear error messages.

- Confirmation prompts are used for destructive actions like player removal.

---

## Data Structures (Brief Summary)

> Full rationale goes in **DESIGN.md**. Here, list only what you used and the feature it powers.

**Data structures used:**  
- `Dictionary<string, Manager>` → Stores league managers and enables fast lookup by manager name

- `HashSet<BasketballPlayer>` → Stores a manager’s roster while enforcing player uniqueness

- `Dictionary<int, BasketballPlayer>` → Represents the waiver wire for efficient player lookup and claims by ID
---

## Manual Testing Summary

> No unit tests required. Show how you verified correctness with 3–5 test scenarios.

**Test scenarios:**  
_Describe each test scenario with steps and expected results._

**Scenario 1: Claim player from waivers**

- Steps: View waiver wire → Enter valid player ID → Claim player
- Expected result: Player added to team and removed from waivers
- Actual result: Player successfully claimed and listed on team

**Scenario 2: Claim non-existent player**

- Steps: Enter an invalid or unavailable player ID
- Expected result: Error message shown, no state change
- Actual result: Error displayed correctly

**Scenario 3: Prevent duplicate players**

- Steps: Attempt to claim the same player twice
- Expected result: Player cannot be added twice
- Actual result: Duplicate prevented by HashSet

**Scenario 4: Remove player from team**

- Steps: Remove player by ID → Confirm action
- Expected result: Player removed from team and added back to waivers
- Actual result: Player successfully moved to waivers

---

## Known Limitations

**Limitations and edge cases:**  
- Data is stored in memory only and resets when the app exits

- No persistence or multi-user support

- Waiver priority order is not implemented

## Comparers & String Handling

**Keys comparer:**  
`StringComparer.OrdinalIgnoreCase` is used for manager names to prevent duplicate entries caused by casing differences.

**Normalization:**  
- User input is trimmed of whitespace

- Manager names are validated for uniqueness

- Player IDs are validated using int.TryParse

---

## Credits & AI Disclosure

**Resources:**  
- Course lecture notes and assignments
- Google
- StackOverflow


**AI usage (if any):**  

AI tools were used for conceptual guidance, design discussion, and reviewing code structure. All logic was implemented and verified manually, and all code was tested and debugged by the author.

  ***

## Challenges and Solutions

**Biggest challenge faced:**  
Designing the interaction between the manager’s roster and the waiver wire while preventing invalid states such as duplicate players or null references.

**How you solved it:**  
By carefully selecting data structures (HashSet and Dictionary), using guard clauses, validating user input, and refactoring logic into service classes for clarity and separation of concerns.

**Most confusing concept:**  
Handling nullable references and understanding compiler warnings related to possible null values in C#.

## Code Quality

**What you're most proud of in your implementation:**  
The clear separation of responsibilities between UI handling, business logic, and data models, as well as the thoughtful selection of data structures based on real usage patterns.

**What you would improve if you had more time:**  
- Implement waiver priority using a PriorityQueue
- Add sorting and filtering options for player listings
- Add a fantasy point system and make users face off against each other

## Real-World Applications

**How this relates to real-world systems:**  
This project mirrors real-world inventory and roster management systems such as fantasy sports platforms, 
where users manage resources, enforce uniqueness, and perform frequent add/remove operations.


**Your Answer:**

**What you learned about data structures and algorithms:**  
I learned how choosing the correct data structure directly impacts performance, correctness, and simplicity. I also gained practical experience with Big-O tradeoffs, key design, and enforcing uniqueness through structure choice instead of manual checks.

## Submission Checklist

- [x] Public GitHub repository link submitted
- [x] README.md completed (this file)
- [x] DESIGN.md completed
- [x] Source code included and builds successfully
- [ ] (Optional) Slide deck or 5–10 minute demo video link (unlisted)

**Demo Video Link (optional):**
