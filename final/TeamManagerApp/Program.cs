using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TeamManagerApp.UI;
using TeamManagerApp.Services;
using TeamManagerApp.Models;

namespace TeamManagerApp
{
    /// <summary>
    /// Main entry point for the Team Manager application.
    /// This application uses Dictionaries, Lists, and HashSets through interactive team managing.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Team Manager App ===");
            Console.WriteLine();
            try
            {
                // Initialize the system
                var managerService = new ManagerService();
                var waiverWire = new WaiverWire();
                
                // Start the interactive navigator immediately
                var navigator = new AppNavigator(managerService, waiverWire);
                navigator.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }
    }
}