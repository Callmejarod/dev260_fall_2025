using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TeamManagerApp.Models
{
    public class Manager
    {

        private static int _nextId = 1;
        public int Id {get; }
        public string ManagerName {get; set;}
        public string TeamName {get; set;}
        public HashSet<BasketballPlayer> PlayersList;



        public Manager(string managerName, string teamName)
        {
            Id = _nextId++;
            ManagerName = managerName;
            TeamName = teamName;
            PlayersList =  new HashSet<BasketballPlayer>();
        }

        public bool AddPlayer(BasketballPlayer player)
        {
            return PlayersList.Add(player);
        }

        public bool RemovePlayer(int playerId)
        {
            BasketballPlayer? playerToRemove = FindPlayerByID(playerId);
            
            if(playerToRemove == null)
            {
                return false;
            }
            
            return PlayersList.Remove(playerToRemove);

        }

        // Helper Methods
        public BasketballPlayer? FindPlayerByID(int playerId)
        {
            foreach (BasketballPlayer player in PlayersList)
            {
                if(player.Id == playerId)
                {
                    return player;
                }
            }
            return null;
        }
    }
}