using System.Security;

namespace Assignment6
{
    /// <summary>
    /// Main matchmaking system managing queues and matches
    /// Students implement the core methods in this class
    /// </summary>
    public class MatchmakingSystem
    {
        // Data structures for managing the matchmaking system
        private Queue<Player> casualQueue = new Queue<Player>();
        private Queue<Player> rankedQueue = new Queue<Player>();
        private Queue<Player> quickPlayQueue = new Queue<Player>();
        private List<Player> allPlayers = new List<Player>();
        private List<Match> matchHistory = new List<Match>();

        // Statistics tracking
        private int totalMatches = 0;
        private DateTime systemStartTime = DateTime.Now;

        /// <summary>
        /// Create a new player and add to the system
        /// </summary>
        public Player CreatePlayer(string username, int skillRating, GameMode preferredMode = GameMode.Casual)
        {
            // Check for duplicate usernames
            if (allPlayers.Any(p => p.Username.Equals(username, StringComparison.OrdinalIgnoreCase)))
            {
                throw new ArgumentException($"Player with username '{username}' already exists");
            }

            var player = new Player(username, skillRating, preferredMode);
            allPlayers.Add(player);
            return player;
        }

        /// <summary>
        /// Get all players in the system
        /// </summary>
        public List<Player> GetAllPlayers() => allPlayers.ToList();

        /// <summary>
        /// Get match history
        /// </summary>
        public List<Match> GetMatchHistory() => matchHistory.ToList();

        /// <summary>
        /// Get system statistics
        /// </summary>
        public string GetSystemStats()
        {
            var uptime = DateTime.Now - systemStartTime;
            var avgMatchQuality = matchHistory.Count > 0 
                ? matchHistory.Average(m => m.SkillDifference) 
                : 0;

            return $"""
                🎮 Matchmaking System Statistics
                ================================
                Total Players: {allPlayers.Count}
                Total Matches: {totalMatches}
                System Uptime: {uptime.ToString("hh\\:mm\\:ss")}
                
                Queue Status:
                - Casual: {casualQueue.Count} players
                - Ranked: {rankedQueue.Count} players  
                - QuickPlay: {quickPlayQueue.Count} players
                
                Match Quality:
                - Average Skill Difference: {avgMatchQuality:F1}
                - Recent Matches: {Math.Min(5, matchHistory.Count)}
                """;
        }

        // ============================================
        // STUDENT IMPLEMENTATION METHODS (TO DO)
        // ============================================

        /// <summary>
        /// TODO: Add a player to the appropriate queue based on game mode
        /// 
        /// Requirements:
        /// - Add player to correct queue (casualQueue, rankedQueue, or quickPlayQueue)
        /// - Call player.JoinQueue() to track queue time
        /// - Handle any validation needed
        /// </summary>
        public void AddToQueue(Player player, GameMode mode)
        {
            switch (mode)
            {
                // casual
                case GameMode.Casual:
                    casualQueue.Enqueue(player);
                    player.JoinQueue();
                    break;
                // ranked
                case GameMode.Ranked:
                    rankedQueue.Enqueue(player);
                    player.JoinQueue();

                    break;
                // Quickplay
                case GameMode.QuickPlay:
                    quickPlayQueue.Enqueue(player);
                    player.JoinQueue();

                    break;
                // invalid input
                default:
                    Console.WriteLine("❌ Error adding to queue. Invalid choice.");
                    break;
            }
        }

        /// <summary>
        /// TODO: Try to create a match from the specified queue
        /// 
        /// Requirements:
        /// - Return null if not enough players (need at least 2)
        /// - For Casual: Any two players can match (simple FIFO)
        /// -  
        /// - For QuickPlay: Prefer skill matching, but allow any match if queue > 4 players
        /// - Remove matched players from queue and call LeaveQueue() on them
        /// - Return new Match object if successful
        /// </summary>
        public Match? TryCreateMatch(GameMode mode)
        {
            switch(mode)
            {
                // Casual matches
                case GameMode.Casual:
                    if (casualQueue.Count < 2)
                    {
                        return null;
                    }

                    // Remove matched players from queue
                    Player casualPlayer1 = casualQueue.Dequeue();
                    Player casualPlayer2 = casualQueue.Dequeue();

                    casualPlayer1.LeaveQueue();
                    casualPlayer2.LeaveQueue();

                    return new Match(casualPlayer1, casualPlayer2, GameMode.Casual);

                // Ranked matches
                case GameMode.Ranked:
                    if (rankedQueue.Count < 2)
                    {
                        return null;
                    }

                    // Convert to list for indexing
                    List<Player> rankedPlayers = rankedQueue.ToList();

                    // Players within ±2 skill levels can match
                    for (int i = 0; i < rankedPlayers.Count; i++)
                    {
                        for (int j = i + 1; j < rankedPlayers.Count; j++)
                        {
                            Player rankedPlayer1 = rankedPlayers[i];
                            Player rankedPlayer2 = rankedPlayers[j];

                            // Take the absolute value from subtracting the two skill ratings
                            if (Math.Abs(rankedPlayer1.SkillRating - rankedPlayer2.SkillRating) <= 2)
                            {
                                RemovePlayersFromQueue(rankedQueue, rankedPlayer1, rankedPlayer2);

                                rankedPlayer1.LeaveQueue();
                                rankedPlayer2.LeaveQueue();

                                return new Match(rankedPlayer1, rankedPlayer2, GameMode.Ranked);
                            }
                        }
                    }
                    // Return null if no players are within the acceptable skill range
                    return null;

                // Quickplay matches
                case GameMode.QuickPlay:
                    if (quickPlayQueue.Count < 2)
                    {
                        return null;
                    }

                    Player quickPlayer1;
                    Player quickPlayer2;

                    // Allow any match if the queue is greater than 4 players
                    if (quickPlayQueue.Count > 4)
                    {
                        quickPlayer1 = quickPlayQueue.Dequeue();
                        quickPlayer2 = quickPlayQueue.Dequeue();

                        quickPlayer1.LeaveQueue();
                        quickPlayer2.LeaveQueue();

                        return new Match(quickPlayer1, quickPlayer2, GameMode.QuickPlay);
                    }

                    // prefered skill matching
                    List<Player> quickPlayers = quickPlayQueue.ToList();

                    for (int i = 0; i < quickPlayers.Count; i++)
                    {
                        for (int j = i + 1; j < quickPlayers.Count; j++)
                        {
                            quickPlayer1 = quickPlayers[i];
                            quickPlayer2 = quickPlayers[j];

                            if (Math.Abs(quickPlayer1.SkillRating - quickPlayer2.SkillRating) <= 2)
                            {
                                RemovePlayersFromQueue(quickPlayQueue, quickPlayer1, quickPlayer2);

                                quickPlayer1.LeaveQueue();
                                quickPlayer2.LeaveQueue();

                                return new Match(quickPlayer1, quickPlayer2, GameMode.QuickPlay);
                            }
                        }
                    }

                    return null;
                
                default:
                    return null;
            }
        }

        /// <summary>
        /// TODO: Process a match by simulating outcome and updating statistics
        /// 
        /// Requirements:
        /// - Call match.SimulateOutcome() to determine winner
        /// - Add match to matchHistory
        /// - Increment totalMatches counter
        /// - Display match results to console
        /// </summary>
        public void ProcessMatch(Match match)
        {
            match.SimulateOutcome();

            matchHistory.Add(match);

            totalMatches++;

            // display match results 
            Console.WriteLine("\nMatch Results:\n");
            Console.WriteLine($"Winner: {match.Winner}");
            Console.WriteLine($"Loser: {match.Loser}");
            Console.WriteLine($"Match Time: {match.MatchTime}\n");
        }

        /// <summary>
        /// TODO: Display current status of all queues with formatting
        /// 
        /// Requirements:
        /// - Show header "Current Queue Status"
        /// - For each queue (Casual, Ranked, QuickPlay):
        ///   - Show queue name and player count
        ///   - List players with position numbers and queue times
        ///   - Handle empty queues gracefully
        /// - Use proper formatting and emojis for readability
        /// </summary>
        public void DisplayQueueStatus()
        {
            // TODO: Implement this method
            // Hint: Loop through each queue and display formatted information
            throw new NotImplementedException("DisplayQueueStatus method not yet implemented");
        }

        /// <summary>
        /// TODO: Display detailed statistics for a specific player
        /// 
        /// Requirements:
        /// - Use player.ToDetailedString() for basic info
        /// - Add queue status (in queue, estimated wait time)
        /// - Show recent match history for this player (last 3 matches)
        /// - Handle case where player has no matches
        /// </summary>
        public void DisplayPlayerStats(Player player)
        {
            // TODO: Implement this method
            // Hint: Combine player info with match history filtering
            throw new NotImplementedException("DisplayPlayerStats method not yet implemented");
        }

        /// <summary>
        /// TODO: Calculate estimated wait time for a queue
        /// 
        /// Requirements:
        /// - Return "No wait" if queue has 2+ players
        /// - Return "Short wait" if queue has 1 player
        /// - Return "Long wait" if queue is empty
        /// - For Ranked: Consider skill distribution (harder to match = longer wait)
        /// </summary>
        public string GetQueueEstimate(GameMode mode)
        {
            switch (mode)
            {
                // casual 
                case GameMode.Casual:
                    if (casualQueue.Count >= 2)
                    {
                        return "No wait";
                    }
                    else if (casualQueue.Count == 1)
                    {
                        return "Short wait";
                    }
                    else
                    {
                        return "Long wait";
                    }
                    
                // ranked
                case GameMode.Ranked:
                    if (rankedQueue.Count >= 2)
                    {
                        return "Short wait";
                    }
                    else if (rankedQueue.Count == 1)
                    {
                        return "Long wait";
                    }
                    else
                    {
                        return "Very long wait (No players in the queue.)";
                    }

                // quickplay
                case GameMode.QuickPlay:
                    if (quickPlayQueue.Count >= 2)
                    {
                        return "No wait";
                    }
                    else if (quickPlayQueue.Count == 1)
                    {
                        return "Short wait";
                    }
                    else
                    {
                        return "Long wait";
                    }

                default:
                    return "Unknown";
            }
        }

        // ============================================
        // HELPER METHODS (PROVIDED)
        // ============================================

        /// <summary>
        /// Helper: Check if two players can match in Ranked mode (±2 skill levels)
        /// </summary>
        private bool CanMatchInRanked(Player player1, Player player2)
        {
            return Math.Abs(player1.SkillRating - player2.SkillRating) <= 2;
        }

        /// <summary>
        /// Helper: Remove player from all queues (useful for cleanup)
        /// </summary>
        private void RemoveFromAllQueues(Player player)
        {
            // Create temporary lists to avoid modifying collections during iteration
            var casualPlayers = casualQueue.ToList();
            var rankedPlayers = rankedQueue.ToList();
            var quickPlayPlayers = quickPlayQueue.ToList();

            // Clear and rebuild queues without the specified player
            casualQueue.Clear();
            foreach (var p in casualPlayers.Where(p => p != player))
                casualQueue.Enqueue(p);

            rankedQueue.Clear();
            foreach (var p in rankedPlayers.Where(p => p != player))
                rankedQueue.Enqueue(p);

            quickPlayQueue.Clear();
            foreach (var p in quickPlayPlayers.Where(p => p != player))
                quickPlayQueue.Enqueue(p);

            player.LeaveQueue();
        }

        /// <summary>
        /// Helper: Get queue by mode (useful for generic operations)
        /// </summary>
        private Queue<Player> GetQueueByMode(GameMode mode)
        {
            return mode switch
            {
                GameMode.Casual => casualQueue,
                GameMode.Ranked => rankedQueue,
                GameMode.QuickPlay => quickPlayQueue,
                _ => throw new ArgumentException($"Unknown game mode: {mode}")
            };
        }
   
        /// <summary>
        /// Helper: Extract two players from the queue to find a ranked match
        /// </summary>
        private void RemovePlayersFromQueue(Queue<Player> queue, Player player1, Player player2)
        {
            Queue<Player> newQueue = new Queue<Player>();

            foreach (var player in queue)
            {
                if (player != player1 && player != player2)
                {
                    newQueue.Enqueue(player);
                }
            }

            queue.Clear();

            foreach(var player in newQueue)
            {
                queue.Enqueue(player);
            }
        } 
    }
}