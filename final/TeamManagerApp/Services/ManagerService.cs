using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TeamManagerApp.Models;

namespace TeamManagerApp.Services
{
    public class ManagerService
    {
        private Dictionary<int, Manager> ManagerDict;
        private List<Manager> ManagerList;

        public ManagerService()
        {
            ManagerDict = new Dictionary<int, Manager>();
            ManagerList = new List<Manager>();
        }

        // Adds a manager to the dictionary
        public bool AddManager(string managerName, string managerTeamName)
        {
            // Find out if the managers name is already taken
            if(FindManager(managerName) != null)
            {
                return false;
            }
    
            Manager managerToAdd = new Manager(managerName, managerTeamName);
            
            // Add using ID as key
            ManagerDict.Add(managerToAdd.Id, managerToAdd);
            ManagerList.Add(managerToAdd);

            return true;
        }

        // Removes a manager from the dictionary
        public bool RemoveManager(string managerName)
        {            
            // Find the manager object
            Manager managerToRemove = FindManager(managerName);

            // Guard Clause
            if (managerToRemove == null)
            {
                return false;
            }

            // Remove from dictionary
            ManagerDict.Remove(managerToRemove.Id);

            // Remove from list
            ManagerList.Remove(managerToRemove);

            return true;
        }

        //updates team name
        public bool UpdateTeamName(string managerName, string newTeamName)
        {
            // Find the manager object
            Manager managerToUpdate = FindManager(managerName);

            if (managerToUpdate == null)
            {
                return false;
            }

            // Update team name
            managerToUpdate.TeamName = newTeamName;

            return true;

        }

        // Finds if a manager exists
        public Manager FindManager(string managerName)
        {
            for (int i = 0; i < ManagerList.Count; i++)
            {
                if (ManagerList[i].ManagerName.Equals(managerName, StringComparison.OrdinalIgnoreCase))
                {
                    return ManagerList[i];
                }
            }

            return null;
        }

        // Show all managers
        public void ListAllManagers()
        {
            foreach(Manager manager in ManagerList)
            {
                Console.WriteLine($"- {manager.ManagerName}: {manager.TeamName}");
            }
        }

        // List the players of a given managers team
        public void ListPlayersByManager(string managerName)
        {
            Manager managerToList = FindManager(managerName);

            // Guard against manager not found
            if (managerToList == null)
            {
                Console.WriteLine("Manager not found.");
                return;
            }

            // Guard against a team with no players
            if(managerToList.PlayersList.Count == 0)
            {
                Console.WriteLine($"{managerToList.ManagerName} has no players on their team.");
                return;
            }

            Console.WriteLine($"Players on {managerToList.TeamName}:");
            foreach (BasketballPlayer player in managerToList.PlayersList)
            {
                Console.WriteLine($"- [{player.Id}] {player.FirstName} {player.LastName} {player.Position} - {player.Team}");
            }
        }

        // Populates the app with other managers and players on their team
        public void PopulateManagers()
        {
            // Random Managers chosen
            AddManager("Priscila", "Nikola 🥤");
            AddManager("Jarod", "Nonchalant Generator");
            AddManager("Justin", "Team youngins");

            // Get manager references
            Manager priscila = FindManager("Priscila");
            Manager jarod = FindManager("Jarod");
            Manager justin = FindManager("Justin");

            // Guard Clause
            if (priscila == null || jarod == null || justin == null)
            return;

            // --- Players for Priscila ---
            priscila.AddPlayer(new BasketballPlayer("Nikola", "Jokic", "Nuggets", "Center"));
            priscila.AddPlayer(new BasketballPlayer("Mikal", "Bridges", "Knicks", "Shooting Guard"));
            priscila.AddPlayer(new BasketballPlayer("Evan", "Mobly", "Cavaliers", "Center"));
            priscila.AddPlayer(new BasketballPlayer("Jamal", "Murray", "Nuggets", "Point Guard"));
            priscila.AddPlayer(new BasketballPlayer("Donavan", "Mitchel", "Cavaliers", "Point Guard"));

            // --- Players for Jarod ---
            jarod.AddPlayer(new BasketballPlayer("Shai", "Gilgeous-Alexander", "Thunder", "Point Guard"));
            jarod.AddPlayer(new BasketballPlayer("Devin", "Booker", "Suns", "Shooting Guard"));
            jarod.AddPlayer(new BasketballPlayer("Jalen", "Johnson", "Hawks", "Power Forward"));
            jarod.AddPlayer(new BasketballPlayer("Vj", "Edgecombe", "76ers", "Point Guard"));
            jarod.AddPlayer(new BasketballPlayer("Alperen", "Sengun", "Rockets", "Center"));

            // --- Players for Justin ---
            justin.AddPlayer(new BasketballPlayer("Cooper", "Flag", "Mavericks", "Point Guard"));
            justin.AddPlayer(new BasketballPlayer("Derrick", "White", "Celtics", "Shooting Guard"));
            justin.AddPlayer(new BasketballPlayer("Lebron", "James", "Lakers", "Small Forward"));
            justin.AddPlayer(new BasketballPlayer("Derik", "Queen", "Pelicans", "Power Forward"));
            justin.AddPlayer(new BasketballPlayer("Joel", "Embid", "76ers", "Center"));

        }

    }
}