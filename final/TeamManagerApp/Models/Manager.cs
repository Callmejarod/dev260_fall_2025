using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TeamManagerApp.Models
{
    public class Manager
    {

        public string ManagerName {get; private set;}
        public string TeamName {get; private set;}
        public HashSet<BasketballPlayer> PlayersList;



        public Manager(string managerName, string teamName)
        {
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
            BasketballPlayer playerToRemove = FindPlayerByID(playerId);
            
            if(playerToRemove == null)
            {
                return false;
            }
            
            return PlayersList.Remove(playerToRemove);

        }

        // Helper Methods
        public BasketballPlayer FindPlayerByID(int playerId)
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