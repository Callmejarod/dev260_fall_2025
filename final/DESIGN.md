# Project Design & Rationale

**Instructions:** Replace prompts with your content. Be specific and concise. If something doesn't apply, write "N/A" and explain briefly.

---

## Data Model & Entities

**Core entities:**  
_List your main entities with key fields, identifiers, and relationships (1–2 lines each)._



**Entity A:**

- Name: `Manager`
- Key fields: ManagerName, TeamName, PlayerList
- Identifiers: ManagerName
- Relationships: One Manager owns many `BasketballPlayer` Objects

**Entity B:**

- Name: `BasketballPlayer`
- Key fields:Id, FirstName, LastName, Team, Position
- Identifiers: Id (auto-incremented)
- Relationships: A BasketballPlayer belongs to either `Manager` or is on the `WaiverWire`

**Entity C:**

- Name: `WaiverWire`
- Key fields: AvailablePlayers
- Identifiers: `BasketballPlayer.Id`
- Relationships: Holds many `BasketballPlayer` objects that are not currently assigned to any `Manager`

**Identifiers (keys) and why they're chosen:**  

- **ManagerName** is used as the identifier for `Manager` objects because it is user-facing, unique within the league, and intuitive for lookups. 
- **BasketballPlayer.Id** is an auto-incremented integer to guarantee uniqueness, prevent name collisions, and support fast lookups when claiming or removing players.
- Numeric IDs were preferred over names for players to avoid ambiguity and simplify user input validation

---

## Data Structures — Choices & Justification

_List only the meaningful data structures you chose. For each, state the purpose, the role it plays in your app, why it fits, and alternatives considered._

### Structure #1

**Chosen Data Structure:**  
`Dictionary<int, Manager>`

**Purpose / Role in App:**  
Stores and manages all league managers. Powers manager creation, lookup, updates, and listing.

**Why it fits:**  
- Fast O(1) average-time lookup by manager ID
- Ideal for ensuring manager names are unique
- Simple and readable for core operations

**Alternatives considered:**  
- `SortedDictionary` - unnecessary ordering overhead
- `PlayersDictionary` (In Manager Object) - unnecessary since a team usually has less than 12 players on a roster. So no need for fast lookups just uniqueness.

---

### Structure #2

**Chosen Data Structure:**  
`HashSet<BasketballPlayer>`

**Purpose / Role in App:**  
Used by each Manager to store their team's players and enforce uniqueness.

**Why it fits:**  
- Prevents duplicates players on a team
- Fast O(1) add and remove
- Ideal for unordered collection where uniqueness matters


**Alternatives considered:**  
- `Dictionary<int, BasketballPlayer>` (In Manager Object) - unnecessary since a team usually has less than 12 players on a roster. So no need for fast lookups just uniqueness.
- `List<BasketballPlayer>` — rejected due to duplicate risk and slower searches

---

### Structure #3

**Chosen Data Structure:**  
`Dictionary<int, BasketballPlayer>`

**Purpose / Role in App:**  
Represents the waiver wire, enabling players to be claimed or dropped efficiently by ID.

**Why it fits:**  
- O(1) lookup by player ID
- Mirrors real fantasy sports waiver systems
- Makes add/remove operations explicit and safe

**Alternatives considered:**  
`List<BasketballPlayer>` — rejected due to O(n) search time

`Queue<BasketballPlayer>` — not appropriate since order does not matter

---


## Comparers & String Handling

**Comparer choices:**  
- `StringComparer.OrdinalIgnoreCase` is used for manager name keys to prevent duplicate managers with different casing.

**For keys:**
- Manager names are treated as case-insensitive.

**For display sorting (if different):**
- N/A (display order reflects insertion order)

**Normalization rules:**  
- User input is trimmed and checked for empty or whitespace-only values.
- Manager names are validated for uniqueness before creation.
- Player IDs are validated using int.TryParse to prevent runtime errors.

**Bad key examples avoided:**  
- Player full names (not unique)
- Team names (mutable and not guaranteed unique)
- Case-sensitive strings ("Jarod" vs "jarod")
---

## Performance Considerations

**Expected data scale:**  
- Managers: 1 - 20
- Players: 40 - 300
- Waiver Wire Size: 50 - 200 players

**Performance bottlenecks identified:**  
- Repeated null lookups were addressed using guard clauses and nullable reference handling.
- Dictionary-based lookups prevent unnecessary iteration.


**Big-O analysis of core operations:**  
_Provide time complexity for your main operations (Add, Search, List, Update, Delete)._

- Add: O(1)
- Search: O(1)
- List:  O(n)
- Update: O(1)
- Delete: O(1)

---

## Design Tradeoffs & Decisions

**Key design decisions:**  
- Chose in-memory data structures for simplicity and clarity.
- Centralized business logic in service classes (ManagerService, WaiverWire).
- Used guard clauses heavily to prevent invalid state and crashes.

**Tradeoffs made:**  
- Avoided premature optimization given the small dataset size.
- Prioritized readability and maintainability over advanced persistence.

**What you would do differently with more time:**  
- Persist data using a database or file storage.
- Implement sorting and filtering options for waiver wire listings.
- Implement PriorityQueue for Waiver Wire pick ups to simulate IRL fantasy apps. 

