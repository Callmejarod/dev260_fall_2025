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

        public BasketballPlayer? FindPlayer(int playerId)
        {
            if (AvailablePlayers.ContainsKey(playerId))
            {
                return AvailablePlayers[playerId];
            }
            else
            {
                return null;
            }
        }

        public void PopulateWaiverWire()
        {
            AddToWaivers(new BasketballPlayer("Stephen", "Curry", "Warriors", "Point Guard"));
            AddToWaivers(new BasketballPlayer("Kyrie", "Irving", "Mavericks", "Point Guard"));
            AddToWaivers(new BasketballPlayer("Anthony", "Edwards", "Timberwolves", "Shooting Guard"));
            AddToWaivers(new BasketballPlayer("Tyrese", "Haliburton", "Pacers", "Point Guard"));
            AddToWaivers(new BasketballPlayer("Damian", "Lillard", "Bucks", "Point Guard"));
            AddToWaivers(new BasketballPlayer("LaMelo", "Ball", "Hornets", "Point Guard"));
            AddToWaivers(new BasketballPlayer("Jalen", "Brunson", "Knicks", "Point Guard"));
            AddToWaivers(new BasketballPlayer("Trae", "Young", "Hawks", "Point Guard"));
            AddToWaivers(new BasketballPlayer("CJ", "McCollum", "Pelicans", "Shooting Guard"));
            AddToWaivers(new BasketballPlayer("Bradley", "Beal", "Suns", "Shooting Guard"));

            AddToWaivers(new BasketballPlayer("Jayson", "Tatum", "Celtics", "Small Forward"));
            AddToWaivers(new BasketballPlayer("Kevin", "Durant", "Suns", "Small Forward"));
            AddToWaivers(new BasketballPlayer("Kawhi", "Leonard", "Clippers", "Small Forward"));
            AddToWaivers(new BasketballPlayer("Jimmy", "Butler", "Heat", "Small Forward"));
            AddToWaivers(new BasketballPlayer("Giannis", "Antetokounmpo", "Bucks", "Power Forward"));
            AddToWaivers(new BasketballPlayer("Zion", "Williamson", "Pelicans", "Power Forward"));
            AddToWaivers(new BasketballPlayer("Paolo", "Banchero", "Magic", "Power Forward"));
            AddToWaivers(new BasketballPlayer("Scottie", "Barnes", "Raptors", "Power Forward"));
            AddToWaivers(new BasketballPlayer("Michael", "Porter Jr.", "Nuggets", "Small Forward"));
            AddToWaivers(new BasketballPlayer("Brandon", "Ingram", "Pelicans", "Small Forward"));

            AddToWaivers(new BasketballPlayer("Karl-Anthony", "Towns", "Timberwolves", "Center"));
            AddToWaivers(new BasketballPlayer("Victor", "Wembanyama", "Spurs", "Center"));
            AddToWaivers(new BasketballPlayer("Bam", "Adebayo", "Heat", "Center"));
            AddToWaivers(new BasketballPlayer("Rudy", "Gobert", "Timberwolves", "Center"));
            AddToWaivers(new BasketballPlayer("Brook", "Lopez", "Bucks", "Center"));
            AddToWaivers(new BasketballPlayer("Chet", "Holmgren", "Thunder", "Center"));

            AddToWaivers(new BasketballPlayer("Franz", "Wagner", "Magic", "Small Forward"));
            AddToWaivers(new BasketballPlayer("Desmond", "Bane", "Grizzlies", "Shooting Guard"));
            AddToWaivers(new BasketballPlayer("Jaylen", "Brown", "Celtics", "Shooting Guard"));
            AddToWaivers(new BasketballPlayer("Klay", "Thompson", "Mavericks", "Shooting Guard"));
        }

        public IEnumerable<BasketballPlayer> GetAvailablePlayers()
        {
            return AvailablePlayers.Values;
        }


    }
















}