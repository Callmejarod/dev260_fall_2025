using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TeamManagerApp.Services;
using TeamManagerApp.Models;

namespace TeamManagerApp.UI
{
    public class AppNavigator    
    {
        // private readonly PlayerService playerService;
        private readonly ManagerService managerService;
        private readonly WaiverWire waiverWire;
        private Manager userManager;

        private bool isRunning;

        public AppNavigator(ManagerService managerService, WaiverWire waiverWire)
        {
            this.managerService = managerService ?? throw new ArgumentNullException(nameof(managerService));
            this.waiverWire = waiverWire ?? throw new ArgumentNullException(nameof(waiverWire));

            userManager = null;
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

                Console.WriteLine($"You selected: {choice}\n");
                
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

                case "3":
                    HandleCreateManager();
                    break;
                    
                case "4":
                    HandleDisplayWaiverList();
                    break;
                    
                case "5":
                    HandleAddPlayer();
                    break;
                    
                case "6":
                    HandleRemovePlayer();
                    break;
                    
                case "7":
                    HandleChangeTeamName();
                    break;
                    
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

        private void HandleCreateManager()
        {
            Console.WriteLine("=== Manager Creation ===");

            // Error handling
            try
            {
                bool success = false;
                string managerName = "";
                string teamName = "";

                while (!success)
                {
                    // Collect names
                    Console.WriteLine("\nEnter your name: ");
                    managerName = Console.ReadLine();

                    Console.WriteLine("\nEnter your team name: ");
                    teamName = Console.ReadLine();

                    // Validate empty inputs
                    if (string.IsNullOrEmpty(managerName) || string.IsNullOrEmpty(teamName))
                    {
                        Console.WriteLine("\n✗ ERROR: Manager name and team name cannot be empty.");
                        continue;
                    }

                    // Attempt to add manager
                    if (managerService.AddManager(managerName, teamName))
                    {
                        success = true;
                        userManager = managerService.FindManager(managerName); // Assign the users team
                    }
                    else
                    {
                        Console.WriteLine("\n✗ ERROR: That manager name already exists. Please try a different name.");
                    }
                }

                Console.WriteLine($"\n✓ SUCCESS: Manager '{managerName}' created! You're ready to add players.");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"✗ ERROR: {ex.Message}");
            }

            Console.WriteLine(); 
        }

        private void HandleDisplayWaiverList()
        {
            Console.WriteLine("=== Waiver Wire ===");

            try
            {
                var players = waiverWire.GetAvailablePlayers();

                if (!players.Any())
                {
                    Console.WriteLine("No players available on the waiver wire.");
                    return;
                }

                foreach (var player in players)
                {
                    Console.WriteLine(player.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ ERROR: {ex.Message}");
            }

            Console.WriteLine();
        }

        private void HandleAddPlayer()
        {
            Console.WriteLine("=== Player Add ===");

            try
            {
                int playerId;
                BasketballPlayer playerToAdd;

                Console.WriteLine("Enter the Player ID:  ");
                string userInput = Console.ReadLine();
                
                // Check if the user entered a number
                if (int.TryParse(userInput, out playerId))
                {
                    // Check if the player is in the Dictionary
                    if (waiverWire.FindPlayer(playerId) != null)
                    {
                        // Add the player to the users team
                        playerToAdd = waiverWire.FindPlayer(playerId);
                        userManager.AddPlayer(playerToAdd);

                        // Remove him from waivers
                        waiverWire.RemovefromWaivers(playerId);

                        Console.WriteLine($"\n✓ SUCCESS: Player claimed off waivers!\n");
                        managerService.ListPlayersByManager(userManager.ManagerName);
                    }
                    else
                    {
                        Console.WriteLine($"✗ ERROR: '{playerId}' is not available to claim in waivers.");
                        return;
                    }

                }
                else
                {
                    Console.WriteLine($"✗ ERROR: '{userInput}' is not a valid Id number.");
                    return;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ ERROR: {ex.Message}");
            }

            Console.WriteLine();
        }

        private void HandleRemovePlayer()
        {
            Console.WriteLine("=== Player Removal ===");

            Console.Write("Enter the Player ID: ");
            string userInput = Console.ReadLine();

            // Validate ID input
            if (!int.TryParse(userInput, out int playerId))
            {
                Console.WriteLine($"✗ ERROR: '{userInput}' is not a valid ID number.");
                Console.WriteLine();
                return;
            }

            // Check if the player is actually on the user's team
            BasketballPlayer playerToRemove = userManager.FindPlayerByID(playerId);

            if (playerToRemove == null)
            {
                Console.WriteLine($"✗ ERROR: Player ID {playerId} is not on your team.");
                Console.WriteLine();
                return;
            }

            // Confirm action
            Console.WriteLine(
                $"\nAre you sure you want to drop {playerToRemove.FullName}?\n" +
                "They will be placed on waivers for anyone to claim. (Y/N)"
            );

            string confirmation = Console.ReadLine()?.Trim().ToLower();

            if (confirmation != "y")
            {
                Console.WriteLine("\nOperation cancelled.\n");
                return;
            }

            // Remove from team + add back to waivers
            userManager.RemovePlayer(playerId);
            waiverWire.AddToWaivers(playerToRemove);

            Console.WriteLine($"\n✓ SUCCESS: {playerToRemove.FullName} is now on waivers!");
            Console.WriteLine();
        }

        private void HandleChangeTeamName()
        {
            Console.WriteLine("=== Update Team Name ===");

            // Check if the user has created a manager yet
            if (userManager == null)
            {
                Console.WriteLine("✗ ERROR: You have not created a manager.");
                Console.WriteLine();
                return;
            }

            string currentName = userManager.TeamName;
            string newName;
            bool success;

            Console.WriteLine($"Current Team Name: {currentName}");
            Console.Write("New Team Name: ");
            newName = Console.ReadLine();

            // Check if user entered empty string
            if (string.IsNullOrWhiteSpace(newName))
            {
                Console.WriteLine("\n✗ ERROR: Team name cannot be empty.");
                Console.WriteLine();
                return;
            }

            // Call update name function
            success = managerService.UpdateTeamName(userManager.ManagerName, newName);

            if (success)
            {
                Console.WriteLine("\n✓ SUCCESS:");
                Console.WriteLine($"Previous Team Name: {currentName}");
                Console.WriteLine($"New Team Name: {newName}");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("✗ ERROR: Failed to update team name.");
            }


        }




    }
}
