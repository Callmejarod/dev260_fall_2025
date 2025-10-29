# Browser Navigation System - Study Notes

## BrowserSession Building Step-by-Step Progress

- **Step 1: VisitURL Method**
    - Started by implementing the logic for visiting a new page.
    - Checked if currentPage exists — if it does, push it to the backStack.
    - Cleared the forwardStack to simulate a browser resetting forward history after a new navigation.
    - Created a new WebPage object and set it as the current page.

- **Step 2: GoBack Method**
    - Checked if backStack has any pages (guard clause).
    - If so, pushed the currentPage to the forwardStack.
    - Popped the top page from the backStack and made it the new currentPage.
    - Returned true if navigation succeeded, false otherwise.

- **Step 3: GoForward Method**
    - Checked if forwardStack is empty.
    - Pushed the currentPage to the backStack.
    - Popped from the forwardStack to move forward through history.
    - Returned true or false depending on success.

- **Step 4: DisplayBackHistory Method**
    - Used a foreach loop to display titles and URLs.
    - Counted down positions from the stack size to show the order clearly.
    - Displayed (No back history) if the stack was empty.

- **Step 5: DisplayForwardHistory Method**
    - imilar logic to the back history display.
    - Counted pages and printed them in order.
    - Ensured proper message when no forward history existed.

- **Step 6: ClearHistory Method**
    - Calculated total cleared pages using both stack counts.
    - Cleared both backStack and forwardStack.
    - Displayed confirmation message showing how many pages were removed.

---

## Challenges Faced
- Remembering to clear the forward stack after visiting a new URL.
- Mixing up which stack to push/pop when going back or forward.

## Overall Reflection

This assignment helped me clearly see how abstract data structures like stacks translate into real-world functionality. Implementing browser navigation gave me a better grasp on how software maintains history states and allows movement between them.