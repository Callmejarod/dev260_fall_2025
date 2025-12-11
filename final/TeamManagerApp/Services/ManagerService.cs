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

            Console.WriteLine($"Players for {managerToList.ManagerName}'s team:");
            foreach (BasketballPlayer player in managerToList.PlayersList)
            {
                Console.WriteLine($"- {player.FirstName} {player.LastName}");
            }
        }
    }
}