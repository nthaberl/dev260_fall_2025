using static System.Console;

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
            // TODO: Implement this method
            switch (mode)
            {
                case GameMode.Casual:
                    if (casualQueue.Contains(player))
                    {
                        throw new InvalidOperationException($"{player.Username} is already in the casual queue!");
                    }
                    else
                    {
                        casualQueue.Enqueue(player);
                        player.JoinQueue();
                        break;
                    }

                case GameMode.Ranked:
                    if (rankedQueue.Contains(player))
                    {
                        throw new InvalidOperationException($"{player.Username} is already in the ranked queue!");
                    }
                    else
                    {
                        rankedQueue.Enqueue(player);
                        player.JoinQueue();
                        break;
                    }

                case GameMode.QuickPlay:
                    if (quickPlayQueue.Contains(player))
                    {
                        throw new InvalidOperationException($"{player.Username} is already in the quickplay queue!");
                    }
                    else
                    {
                        quickPlayQueue.Enqueue(player);
                        player.JoinQueue();
                        break;
                    }
            }
        }

        /// <summary>
        /// TODO: Try to create a match from the specified queue
        /// 
        /// Requirements:
        /// - Return null if not enough players (need at least 2)
        /// - For Casual: Any two players can match (simple FIFO)
        /// - For Ranked: Only players within ±2 skill levels can match
        /// - For QuickPlay: Prefer skill matching, but allow any match if queue > 4 players
        /// - Remove matched players from queue and call LeaveQueue() on them
        /// - Return new Match object if successful
        /// </summary>
        public Match? TryCreateMatch(GameMode mode)
        {
            // TODO: Implement this method
            // Hint: Different logic needed for each mode
            // Remember to check queue count first!

            //using helper function to get queuemode
            var currentQueue = GetQueueByMode(mode);

            //returns null if any queue has less than 2 players
            if (currentQueue.Count < 2)
            {
                return null;
            }

            switch (mode)
            {
                //in casual mode, simply match the first two players in the queue
                case GameMode.Casual:
                    {
                        Player player1 = currentQueue.Dequeue();
                        Player player2 = currentQueue.Dequeue();
                        player1.LeaveQueue();
                        player2.LeaveQueue();
                        return new Match(player1, player2, mode);
                    }

                //cannot pull elements from middle of queue
                //easier to copy elements to array first and search for a match
                case GameMode.Ranked:
                    {
                        Player[] rankedArray = rankedQueue.ToArray();

                        //initialize search indexes
                        int searchIndex1 = -1;
                        int searchIndex2 = -1;
                        bool foundMatch = false;

                        for (int i = 0; i < rankedArray.Length && !foundMatch; i++)
                        {
                            for (int j = i + 1; j < rankedArray.Length && !foundMatch; j++)
                            {
                                if (CanMatchInRanked(rankedArray[i], rankedArray[j]))
                                {
                                    searchIndex1 = i;
                                    searchIndex2 = j;
                                    foundMatch = true;
                                }
                            }
                        }

                        //if no match found, return null
                        if (!foundMatch)
                        {
                            return null;
                        }

                        //store the two matched players
                        Player p1 = rankedArray[searchIndex1];
                        Player p2 = rankedArray[searchIndex2];

                        //store elements from the array into a temp queue *without* the matched players
                        var updatedRankedQueue = new Queue<Player>();
                        for (int i = 0; i < rankedArray.Length; i++)
                        {
                            if (i != searchIndex1 && i != searchIndex2)
                            {
                                updatedRankedQueue.Enqueue(rankedArray[i]);
                            }
                        }

                        //copy the queue back to the original rankedQueue
                        rankedQueue = updatedRankedQueue;

                        p1.LeaveQueue();
                        p2.LeaveQueue();

                        return new Match(p1, p2, mode);
                    }

                //follow same rules as casual if queue has 4+ people
                //otherwise implement same logic as ranked
                case GameMode.QuickPlay:
                    {
                        if (currentQueue.Count > 3)
                        {
                            var player1 = currentQueue.Dequeue();
                            var player2 = currentQueue.Dequeue();
                            player1.LeaveQueue();
                            player2.LeaveQueue();
                            return new Match(player1, player2, mode);
                        }
                        else
                        {
                            //storing elements of quickPlayQueue into an array for easier comparison between players
                            Player[] quickPlayArray = quickPlayQueue.ToArray();

                            //initialize search indexes
                            int searchIndex1 = -1;
                            int searchIndex2 = -1;
                            bool foundMatch = false;

                            for (int i = 0; i < quickPlayArray.Length && !foundMatch; i++)
                            {
                                for (int j = i + 1; j < quickPlayArray.Length && !foundMatch; j++)
                                {
                                    if (CanMatchInRanked(quickPlayArray[i], quickPlayArray[j]))
                                    {
                                        searchIndex1 = i;
                                        searchIndex2 = j;
                                        foundMatch = true;
                                    }
                                }
                            }

                            //if no match found, return null
                            if (!foundMatch)
                            {
                                return null;
                            }

                            //store the two matched players
                            Player player1 = quickPlayArray[searchIndex1];
                            Player player2 = quickPlayArray[searchIndex2];

                            //store elements from the array into a temp queue *without* the matched players
                            var updatedQuickPlayQueue = new Queue<Player>();
                            for (int i = 0; i < quickPlayArray.Length; i++)
                            {
                                if (i != searchIndex1 && i != searchIndex2)
                                {
                                    updatedQuickPlayQueue.Enqueue(quickPlayArray[i]);
                                }
                            }

                            //copy the queue back to the original rankedQueue
                            quickPlayQueue = updatedQuickPlayQueue;

                            player1.LeaveQueue();
                            player2.LeaveQueue();

                            return new Match(player1, player2, mode);
                        }
                    }

                default:
                    return null;
            }
        }

        /// Requirements:
        /// - Call match.SimulateOutcome() to determine winner
        /// - Add match to matchHistory
        /// - Increment totalMatches counter
        /// - Display match results to console
        /// </summary>
        public void ProcessMatch(Match match)
        {
            // TODO: Implement this method
            // Hint: Very straightforward - simulate, record, display
            match.SimulateOutcome();
            matchHistory.Add(match);
            totalMatches++;
            WriteLine($"{match.ToDetailedString()}");
        }

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
            WriteLine("Current queue status: ");

            if (casualQueue.Count == 0 && rankedQueue.Count == 0 && quickPlayQueue.Count == 0)
            {
                WriteLine("All queues are currently empty!\n");
                return;
            }

            //display casual queue
            WriteLine("😎 Casual queue status: ");
            if (casualQueue.Count < 1)
            {
                WriteLine("No players waiting in the casual queue..\n");
            }
            else
            {
                WriteLine($"Current players in casual queue: {casualQueue.Count}");
                int position = 1;
                foreach (var player in casualQueue)
                {
                    WriteLine($"{position}. {player}");
                    position++;
                }
                WriteLine();
            }

            //display ranked queue
            WriteLine("🔥 Ranked queue status: ");
            if (rankedQueue.Count < 1)
            {
                WriteLine("No players waiting in the ranked queue..\n");
            }
            else
            {
                WriteLine($"Current players in ranked queue: {rankedQueue.Count}");
                int position = 1;
                foreach (var player in rankedQueue)
                {
                    WriteLine($"{position}. {player}");
                    position++;
                }
                WriteLine();
            }

            //display quickplayqueue
            WriteLine("⚡ Quickplay queue status: ");
            if (quickPlayQueue.Count < 1)
            {
                WriteLine("No players waiting in the quickplay queue..\n");
                return;
            }
            else
            {
                WriteLine($"Current players in casual queue: {casualQueue.Count}");
                int position = 1;
                foreach (var player in casualQueue)
                {
                    WriteLine($"{position}. {player}");
                    position++;
                }
                WriteLine();
            }
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
            WriteLine($"{player.ToDetailedString()}");


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
            // TODO: Implement this method
            // Hint: Check queue counts and apply mode-specific logic
            if (casualQueue.Count >= 2 || quickPlayQueue.Count >= 2)
            {
                return "No wait!";
            }
            if (casualQueue.Count == 1 || quickPlayQueue.Count == 1)
            {
                return "Short wait";
            }
            else
            {
                return "Long wait";
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
    }
}