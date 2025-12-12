using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TeamManagerApp.Services;

namespace TeamManagerApp.UI
{
    public class AppNavigator    
    {
        // private readonly PlayerService playerService;
        private readonly ManagerService managerService;
        private readonly WaiverWire waiverwire;

        private bool isRunning;

        private readonly WaiverWire waiverWire;

        public AppNavigator(ManagerService managerService, WaiverWire waiverWire)
        {
            this.managerService = managerService ?? throw new ArgumentNullException(nameof(managerService));
            this.waiverWire = waiverWire ?? throw new ArgumentNullException(nameof(waiverWire));
            this.isRunning = true;
        }

        public void Run()
        {
            // NOTE: Clear welcome message
            Console.WriteLine("=== Team Manager App ===");
            Console.WriteLine("======================================");
            Console.WriteLine("Welcome to the interactive team managing application!");
            Console.WriteLine();

            // NOTE: The main application loop - keeps running until isRunning becomes false
            while (isRunning)
            {
                DisplayMainMenu();
                string choice = Console.ReadLine()?.ToLower() ?? "";

                Console.WriteLine($"You selected: {choice}");
                
                // NOTE: Wrap all user operations in try-catch to prevent crashes
                // from invalid input or business logic errors
                try
                {
                    ProcessCommand(choice.Trim());
                }
                catch (Exception ex)
                {
                    // NOTE: Global exception handler prevents crashes and provides
                    // helpful feedback. This is crucial for student learning environments.
                    Console.WriteLine($"❌ Error: {ex.Message}");
                }

                // NOTE: Pause and clear screen for better user experience
                if (isRunning)
                {
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        /// Process user commands and route to appropriate handlers.
        private void ProcessCommand(string input)
        {
            // NOTE: Split the input into command and arguments
            // RemoveEmptyEntries prevents issues with multiple spaces
            string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 0) return;
            
            // NOTE: Commands are case-insensitive for better user experience
            string command = parts[0].ToLower();
            // INSTRUCTOR NOTE: Skip(1) gets everything after the first element (the arguments)
            string[] args = parts.Skip(1).ToArray();
            
            // NOTE: Switch statement provides clean command routing.
            // Each numbered case (1-6) corresponds to a specific TODO method for easy testing.
            switch (command)
            {
                case "1":
                    HandlePopulateManagers();
                    break;









                    
                case "2":
                    HandlePopulateWaivers();
                    break;
                    
                // case "3":
                // case "categorize":
                //     HandleCategorizeCommand();
                //     break;
                    
                // case "4":
                // case "check":
                //     HandleCheckCommand(args);
                //     break;
                    
                // case "5":
                // case "misspelled":
                //     HandleListMisspelledCommand();
                //     break;
                    
                // case "6":
                // case "unique":
                //     HandleListUniqueCommand();
                //     break;
                    
                // case "7":
                // case "stats":
                //     HandleStatsCommand();
                //     break;
                    
                case "8":
                case "exit":
                case "quit":
                    isRunning = false;
                    ShowGoodbye();
                    break;
                    
                default:
                    // INSTRUCTOR NOTE: Always handle invalid input gracefully with helpful feedback
                    Console.WriteLine($"❌ Unknown command: '{command}'. Please try again.\n");
                    break;
            }
        }

        // Display menu
        private void DisplayMainMenu()
        {            
            Console.WriteLine("┌─────────────────────   Team Manager & Player Explorer   ─────────────────────┐");
            Console.WriteLine("│ 1. Populate Managers       │ 2. Populate Waivers   │ 3. Create Your Manager  │");
            Console.WriteLine("│ 4. Waiver Wire List        │ 5. Add Players        │ 6. Remove Players       │");
            Console.WriteLine("│ 7. Change Team Name        │ 8. List Managers      │ 9. List Players         │");
            Console.WriteLine("│ 10. Exit                                                                     │");
            Console.WriteLine("└──────────────────────────────────────────────────────────────────────────────┘");
            Console.Write("\nChoose an option by number: ");
        }

        // Goodbye method
        private void ShowGoodbye()
        {
            Console.WriteLine("\n📚 Thank you for using the Team Manager App!");
            Console.WriteLine("=============================================================");
        }

        // Handle Populate Managers
        private void HandlePopulateManagers()
        {
            Console.WriteLine("Attempting to populate managers...");

            // Error handling
            try
            {
                managerService.PopulateManagers();
                Console.WriteLine($"✓ SUCCESS: Priscila, Jarod, and Justin are now in your fantasy league!");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"✗ ERROR: {ex.Message}");
            }

            Console.WriteLine();
        }

        // Handle Populate Waivers
        private void HandlePopulateWaivers()
        {
            Console.WriteLine("Attempting to populate waiver wire...");

            // Error handling
            try
            {
                waiverWire.PopulateWaiverWire();
                Console.WriteLine($"✓ SUCCESS: Players are now available to pick up in the waiver wires!");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"✗ ERROR: {ex.Message}");
            }

            Console.WriteLine();            
        }








    }
}
