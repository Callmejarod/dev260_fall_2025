using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace TeamManagerApp.UI
{
    public class AppNavigator    
    {
        private readonly PlayerService playerService;
        private readonly ManagerService managerService;

        private bool isRunning;

        public AppNavigator(PlayerService playerService, ManagerService managerService)
        {
                        // Always validate constructor parameters to prevent null reference exceptions
            this.playerService = playerService ?? throw new ArgumentNullException(nameof(playerService));
            this.isRunning = true;
        }

        public void Run()
        {
            Console.WriteLine("Welcome to the Team Manager!");
        }
    }






}