using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TeamManagerApp.Models;

namespace TeamManagerApp.Services
{
    public class ManagerService
    {
        private Dictionary<string, Manager> ManagerDict;
        private List<Manager> ManagerList;

        public ManagerService()
        {
            ManagerDict = new Dictionary<string, Manager>();
            ManagerList = new List<Manager>();
        }

        // Adds a manager to the dictionary
        public bool AddManager(string managerName, string managerTeamName)
        {
            // no duplicate manager names
            if (ManagerDict.ContainsKey(managerName))
            {
                return false;
            }

            Manager managerToAdd = new Manager(managerName, managerTeamName);
            ManagerDict.Add(managerName, managerToAdd);
            ManagerList.Add(managerToAdd);

            return true;
        }

        // Removes a manager from the dictionary
        public bool RemoveManager(string managerName)
        {
            if (!ManagerDict.ContainsKey(managerName))
            {
                return false;
            }

            // Get Manager Object
            Manager managerToRemove = ManagerDict[managerName];

            // Remove from dictionary
            ManagerDict.Remove(managerName);

            // Remove from list
            ManagerList.Remove(managerToRemove);

            return true;
        }

        // Show all managers
        public void ListAllManagers()
        {
            foreach(Manager manger in ManagerList)
            {
                Console.WriteLine($"- {manger.ManagerName}: {manager.TeamName}");
            }
        }
    }
}