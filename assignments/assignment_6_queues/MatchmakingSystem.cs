using static System.Console;
using System.Linq;

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

        //using switch to add player to correct queue
        //validating that they are not already in the queue
        //if so, throw an exception
        //can not simply display an error message
        //GameNavigator will display that player was successfully added even if they weren't
        //so an exception is needed
        //otherwise player is added to queue
        public void AddToQueue(Player player, GameMode mode)
        {
            //using helper function to get queuemode
            var currentQueue = GetQueueByMode(mode);

            switch (mode)
            {
                case GameMode.Casual:
                    if (currentQueue.Contains(player))
                    {
                        throw new InvalidOperationException($"{player.Username} is already in the casual queue!");
                    }
                    else
                    {
                        currentQueue.Enqueue(player);
                        player.JoinQueue();
                        break;
                    }

                case GameMode.Ranked:
                    if (currentQueue.Contains(player))
                    {
                        throw new InvalidOperationException($"{player.Username} is already in the ranked queue!");
                    }
                    else
                    {
                        currentQueue.Enqueue(player);
                        player.JoinQueue();
                        break;
                    }

                case GameMode.QuickPlay:
                    if (currentQueue.Contains(player))
                    {
                        throw new InvalidOperationException($"{player.Username} is already in the quickplay queue!");
                    }
                    else
                    {
                        currentQueue.Enqueue(player);
                        player.JoinQueue();
                        break;
                    }
            }
        }

        public Match? TryCreateMatch(GameMode mode)
        {
            //using helper function to get current queue
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
                        Player[] rankedArray = currentQueue.ToArray();

                        //initialize search indexes
                        int searchIndex1 = -1;
                        int searchIndex2 = -1;
                        bool foundMatch = false;

                        for (int i = 0; i < rankedArray.Length && !foundMatch; i++)
                        {
                            for (int j = i + 1; j < rankedArray.Length && !foundMatch; j++)
                            {
                                //using helper method to see if players can be matched
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

                        //grab the two matched players
                        Player player1 = rankedArray[searchIndex1];
                        Player player2 = rankedArray[searchIndex2];

                        //store elements from the array into a temp queue *without* the matched players
                        var updatedRankedQueue = new Queue<Player>();
                        for (int i = 0; i < rankedArray.Length; i++)
                        {
                            if (i != searchIndex1 && i != searchIndex2)
                            {
                                updatedRankedQueue.Enqueue(rankedArray[i]);
                            }
                        }

                        //copy the queue back to the *original* rankedQueue
                        //currentQueue is just a copy!
                        rankedQueue = updatedRankedQueue;

                        player1.LeaveQueue();
                        player2.LeaveQueue();

                        return new Match(player1, player2, mode);
                    }

                //follow same rules as casual if queue has 4+ people
                //otherwise implement same logic as ranked match
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
                            Player[] quickPlayArray = currentQueue.ToArray();

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

                            if (!foundMatch)
                            {
                                return null;
                            }

                            Player player1 = quickPlayArray[searchIndex1];
                            Player player2 = quickPlayArray[searchIndex2];

                            var updatedQuickPlayQueue = new Queue<Player>();
                            for (int i = 0; i < quickPlayArray.Length; i++)
                            {
                                if (i != searchIndex1 && i != searchIndex2)
                                {
                                    updatedQuickPlayQueue.Enqueue(quickPlayArray[i]);
                                }
                            }

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

        public void ProcessMatch(Match match)
        {
            match.SimulateOutcome();
            matchHistory.Add(match);
            totalMatches++;
            WriteLine($"{match.ToDetailedString()}");
        }

        public void DisplayQueueStatus()
        {
            WriteLine("\nCurrent Queue Status: \n");

            if (casualQueue.Count == 0 && rankedQueue.Count == 0 && quickPlayQueue.Count == 0)
            {
                WriteLine("All the queues are empty!\n");
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
                WriteLine($"Players waiting in casual queue: {casualQueue.Count}");
                int position = 1;
                foreach (var player in casualQueue)
                {
                    WriteLine($"{position}. {player} - joined at: {player.JoinedQueue}");
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
                WriteLine($"Players waiting in ranked queue: {rankedQueue.Count}");
                int position = 1;
                foreach (var player in rankedQueue)
                {
                    WriteLine($"{position}. {player} - joined at: {player.JoinedQueue}");
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
                WriteLine($"Players waiting in quickplay queue: {quickPlayQueue.Count}");
                int position = 1;
                foreach (var player in quickPlayQueue)
                {
                    WriteLine($"{position}. {player}  - joined at: {player.JoinedQueue}");
                    position++;
                }
                WriteLine();
            }
        }

        public void DisplayPlayerStats(Player player)
        {

            WriteLine($"{player.ToDetailedString()}");
            if (casualQueue.Contains(player))
            {
                int position = 1;
                foreach (var person in casualQueue)
                {
                    if (person == player)
                    {
                        break;
                    }
                    position++;
                }
                WriteLine($"Currently in position {position} of {casualQueue.Count} in the casual queue\n");
            }
            else if (rankedQueue.Contains(player))
            {
                int position = 1;
                foreach (var person in rankedQueue)
                {
                    if (person == player)
                    {
                        break;
                    }
                    position++;
                }
                WriteLine($"Currently in position {position} of {rankedQueue.Count} in the ranked queue\n");
            }
            else if (quickPlayQueue.Contains(player))
            {
                int position = 1;
                foreach (var person in quickPlayQueue)
                {
                    if (person == player)
                    {
                        break;
                    }
                    position++;
                }
                WriteLine($"Currently in position {position} of {quickPlayQueue.Count} in the quickplay queue\n");
            }
            else
            {
                WriteLine($"\nCurrently {player.Username} is not in any queues.\n");
            }

            //using LINQ to filter through matchHistory list to grab matches for player
            //player can be player 1 or player 2 in any match
            //grab last 3 matches in the list
            //reverse order to show most recent match details at top

            var playerMatchHistory = matchHistory
            .Where(match => match.Player1 == player || match.Player2 == player)
            .TakeLast(3)
            .Reverse();

            if (playerMatchHistory.Count() == 0)
            {
                WriteLine($"{player.Username} has not played any matches\n");
            }
            else
            {
                foreach (var match in playerMatchHistory)
                {
                    WriteLine($"{match}");
                }
                WriteLine();
            }
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
            //using helper function to get queuemode
            var currentQueue = GetQueueByMode(mode);

            if (currentQueue == null || currentQueue.Count == 0)
            {
                return $" 😴 There has a long wait..\n";
            }

            switch (mode)
            {
                //following the requirements as written for casual queue
                case GameMode.Casual:
                    {
                        if (currentQueue.Count > 1)
                        {
                            return $" 🏃 There's no wait!\n";
                        }
                        else
                        {
                            return $" 🤏 There is a short wait.\n";
                        }
                    }
                //because quickplay follows FIFO with 4 people, no wait if there are 4 people
                //otherwise follow ranked scenario
                case GameMode.QuickPlay:
                    {
                        if (currentQueue.Count > 3)
                        {
                            return $" 🏃 There's no wait!\n";
                        }
                        else
                        {
                            var queuedPlayers = currentQueue.ToArray();
                            bool foundMatch = false;

                            for (int i = 0; i < queuedPlayers.Length && !foundMatch; i++)
                            {
                                for (int j = i + 1; j < queuedPlayers.Length && !foundMatch; j++)
                                {
                                    if (CanMatchInRanked(queuedPlayers[i], queuedPlayers[j]))
                                    {
                                        foundMatch = true;
                                    }
                                }
                            }

                            if (foundMatch)
                            {
                                return $" 🏃 There's no wait!\n";
                            }
                            else
                            {
                                return $" 🤏 There is a short wait.\n";
                            }
                        }
                    }

                //implemented similar logic as from TryCreateMatch
                case GameMode.Ranked:
                    {
                        if (currentQueue.Count > 1)
                        {
                            var queuedPlayers = currentQueue.ToArray();
                            bool foundMatch = false;

                            for (int i = 0; i < queuedPlayers.Length && !foundMatch; i++)
                            {
                                for (int j = i + 1; j < queuedPlayers.Length && !foundMatch; j++)
                                {
                                    if (CanMatchInRanked(queuedPlayers[i], queuedPlayers[j]))
                                    {
                                        foundMatch = true;
                                    }
                                }
                            }

                            if (foundMatch)
                            {
                                return $" 🏃 There's no wait!\n";
                            }
                            else
                            {
                                return $" 🤏 There is a short wait.\n";
                            }
                        }
                        else
                        {
                            return $" 🤏 There is a short wait.\n";
                        }
                    }

                default:
                    return null;
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