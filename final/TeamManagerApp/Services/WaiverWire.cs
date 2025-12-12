using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using TeamManagerApp.Models;

namespace TeamManagerApp.Services
{
    public class WaiverWire
    {
        // HashSet of Players that are available on the waiver wire
        private Dictionary<int, BasketballPlayer> AvailablePlayers;

        public WaiverWire()
        {
            AvailablePlayers = new Dictionary<int, BasketballPlayer>();
        }


        public bool AddToWaivers(BasketballPlayer player)
        {
            // Guard clause check if there's duplicate players
            if (AvailablePlayers.ContainsKey(player.Id))
            {
                return false;
            }

            AvailablePlayers.Add(player.Id, player);
            return true;
        }

        public bool RemovefromWaivers(int playerId)
        {
            return AvailablePlayers.Remove(playerId);
        }
    }
















}