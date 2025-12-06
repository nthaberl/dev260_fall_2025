using System;
using static System.Console;
using System.Collections.Generic;

/*
=== QUICK REFERENCE GUIDE ===

Stack<T> Essential Operations:
- new Stack<string>()           // Create empty stack
- stack.Push(item)              // Add item to top (LIFO)
- stack.Pop()                   // Remove and return top item
- stack.Peek()                  // Look at top item (don't remove)
- stack.Clear()                 // Remove all items
- stack.Count                   // Get number of items

Safety Rules:
- ALWAYS check stack.Count > 0 before Pop() or Peek()
- Empty stack Pop() throws InvalidOperationException
- Empty stack Peek() throws InvalidOperationException

Common Patterns:
- Guard clause: if (stack.Count > 0) { ... }
- LIFO order: Last item pushed is first item popped
- Enumeration: foreach gives top-to-bottom order

Helpful icons!:
- ✅ Success
- ❌ Error
- 👀 Look
- 📋 Display out
- ℹ️ Information
- 📊 Stats
- 📝 Write
*/

namespace StackLab
{
    /// <summary>
    /// Student skeleton version - follow along with instructor to build this out!
    /// Uncomment the class name and Main method when ready to use this version.
    /// </summary>
    // class Program  // Uncomment this line when ready to use
    class StudentSkeleton
    {

        // TODO: Step 1 - Declare two stacks for action history and undo functionality
        private static Stack<string> actionHistory = new Stack<string>();
        private static Stack<string> undoHistory = new Stack<string>();

        // TODO: Step 2 - Add a counter for total operations
        private static int totalOperations = 0;
        static void Main(string[] args)
        {
            Console.WriteLine("=== Interactive Stack Demo ===");
            Console.WriteLine("Building an action history system with undo/redo\n");

            bool running = true;
            while (running)
            {
                DisplayMenu();
                string choice = Console.ReadLine()?.ToLower() ?? "";

                switch (choice)
                {
                    case "1":
                    case "push":
                        HandlePush();
                        break;
                    case "2":
                    case "pop":
                        HandlePop();
                        break;
                    case "3":
                    case "peek":
                    case "top":
                        HandlePeek();
                        break;
                    case "4":
                    case "display":
                        HandleDisplay();
                        break;
                    case "5":
                    case "clear":
                        HandleClear();
                        break;
                    case "6":
                    case "undo":
                        HandleUndo();
                        break;
                    case "7":
                    case "redo":
                        HandleRedo();
                        break;
                    case "8":
                    case "stats":
                        ShowStatistics();
                        break;
                    case "9":
                    case "exit":
                        running = false;
                        ShowSessionSummary();
                        break;
                    default:
                        Console.WriteLine("❌ Invalid choice. Please try again.\n");
                        break;
                }
            }
        }

        static void DisplayMenu()
        {
            Console.WriteLine("┌─ Stack Operations Menu ─────────────────────────┐");
            Console.WriteLine("│ 1. Push      │ 2. Pop       │ 3. Peek/Top    │");
            Console.WriteLine("│ 4. Display   │ 5. Clear     │ 6. Undo        │");
            Console.WriteLine("│ 7. Redo      │ 8. Stats     │ 9. Exit        │");
            Console.WriteLine("└─────────────────────────────────────────────────┘");
            // TODO: Step 3 - add stack size and total operations to our display
            WriteLine($"Current stack size: {actionHistory.Count} | Total Operations: {totalOperations} ");
            Console.Write("\nChoose operation (number or name): ");
        }

        // TODO: Step 4 - Implement HandlePush method
        static void HandlePush()
        {
            // TODO: 
            // 1. Prompt user for input
            Write("Enter action to add to history: ");
            string? action = ReadLine();
            // 2. Validate input is not empty
            if (!string.IsNullOrWhiteSpace(action))
            {
                // 3. Push to actionHistory stack
                actionHistory.Push(action.Trim());
                // 4. Clear undoHistory stack (new action invalidates redo)
                undoHistory.Clear(); //clear redo stack 
                                     // 5. Increment totalOperations
                totalOperations++;
                // 6. Show confirmation message
                WriteLine($"✅ Pushed '{action}' pushed to history\n");
            }
            else
            {
                WriteLine("❌ Action cannot be empty.\n");
            }
        }

        // TODO: Step 5 - Implement HandlePop method
        static void HandlePop()
        {
            // TODO:
            // 1. Check if actionHistory stack has items (guard clause!)
            if (actionHistory.Count > 0)
            {
                string poppedAction = actionHistory.Pop();
                undoHistory.Push(poppedAction); //for redo
                totalOperations++;
                WriteLine($"✅ Popped '{poppedAction}' from history.\n");
                if (actionHistory.Count == 0)
                {
                    WriteLine("ℹ️ History is now empty.\n");
                }
                else
                {
                    WriteLine($"👀 New top action: '{actionHistory.Peek()}'\n");
                }
            }
            // 2. If empty, show error message
            else
            {
                WriteLine("❌ Cannot pop from empty history. \n");
            }
            // 3. If not empty:
            //    - Pop from actionHistory
            //    - Push popped item to undoHistory (for redo)
            //    - Increment totalOperations
            //    - Show what was popped
            //    - Show new top item (if any)
        }

        // TODO: Step 6 - Implement HandlePeek method
        static void HandlePeek()
        {
            // TODO:
            // 1. Check if actionHistory stack has items
            // 3. If not empty, peek at top item and display
            if (actionHistory.Count > 0)
            {
                string topAction = actionHistory.Peek();
                WriteLine($"👀 Top action: '{topAction}'\n");

            }
            // 2. If empty, show appropriate message
            else
            {
                WriteLine("❌ History is empty. Nothing to peek at.\n");
            }
            // 4. Remember: Peek doesn't modify the stack!
        }

        // TODO: Step 7 - Implement HandleDisplay method
        static void HandleDisplay()
        {
            // TODO:
            // 1. Show a header for the display
            WriteLine("📋 Current History Stack (Top To Bottom): ");
            // 2. Check if stack is empty
            if (actionHistory.Count == 0)
            {
                WriteLine("\nℹ️ History is empty.\n");
            }
            // 3. If not empty, enumerate through stack (foreach)
            else
            {
                int position = actionHistory.Count;
                foreach (string action in actionHistory)
                {
                    // 5. Mark the top item clearly
                    string marker = position == actionHistory.Count ? "Top" : "   ";
                    // 4. Show items in LIFO order with position numbers
                    WriteLine($" {position:D2}. {action}  {marker}");
                    position--;
                }
            }
            WriteLine();
        }

        // TODO: Step 8 - Implement HandleClear method
        static void HandleClear()
        {
            // TODO:
            // 1. Check if there are items to clear
            // 3. If not empty:
            //    - Remember count before clearing
            //    - Clear both actionHistory and undoHistory
            //    - Increment totalOperations
            //    - Show confirmation with count cleared
            if (actionHistory.Count > 0)
            {
                int clearedCount = actionHistory.Count;
                actionHistory.Clear();
                undoHistory.Clear();
                totalOperations++;
                WriteLine($"✅ Cleared {clearedCount} actions from history.\n");
            }
            // 2. If empty, show info message
            else
            {
                WriteLine("❌ History is already empty. Nothing to clear. \n");
            }

        }

        // TODO: Step 9 - Implement HandleUndo method (Advanced)
        static void HandleRedo()
        {
            // TODO:
            // 1. Check if undoHistory has items to restore
            // 3. If not empty:
            //    - Pop from undoHistory
            //    - Push back to actionHistory
            //    - Increment totalOperations
            //    - Show what was restored
            if (undoHistory.Count > 0)
            {
                string actionToRestore = undoHistory.Pop();
                actionHistory.Push(actionToRestore);
                totalOperations++;
                WriteLine($"✅ Redid action: '{actionToRestore}'\n");
            }
            // 2. If empty, show "nothing to undo" message
            else
            {
                WriteLine("ℹ️ Nothing to redo.\n");
            }
        }

        // TODO: Step 10 - Implement HandleRedo method (Advanced)
        static void HandleUndo()
        {
            // TODO:
            // 1. Check if actionHistory has items to redo
            // 3. If not empty:
            //    - Pop from actionHistory
            //    - Push to undoHistory
            //    - Increment totalOperations
            //    - Show what was redone
            if (actionHistory.Count > 0)
            {
                string actionToRemove = actionHistory.Pop();
                undoHistory.Push(actionToRemove);
                totalOperations++;
                WriteLine($"✅Undid action: '{actionToRemove}'\n");
            }
            // 2. If empty, show "nothing to redo" message
            else
            {
                WriteLine("❌ No action to undo. \n");
            }
        }

        // TODO: Step 11 - Implement ShowStatistics method
        static void ShowStatistics()
        {
            // TODO:
            // Display current session statistics:
            WriteLine("📊 Current Session Statistics: ");
            
            // - Current stack size
            WriteLine($"- Current Stack Size: {actionHistory.Count}");

            // - Undo stack size
            WriteLine($"- Undo Stack Size: {undoHistory.Count}");

            // - Total operations performed
            WriteLine($"- Total operations performed: {totalOperations}");

            // - Whether stack is empty
            WriteLine($"- Stack isEmpty? {(actionHistory.Count == 0 ? "Yes" : "No")}");

            // - Current top action (if any)
            if (actionHistory.Count > 0)
            {
                WriteLine($"Current top action: '{actionHistory.Peek()}");
            }
            else
            {
                WriteLine("No actions to peek at");
            }
            WriteLine();
        }

        // TODO: Step 12 - Implement ShowSessionSummary method
        static void ShowSessionSummary()
        {
            // TODO:
            // Show final summary when exiting:
            WriteLine("📋 Final Session Summary: ");

            // - Total operations performed
            WriteLine($"- Total operations performed: {totalOperations}");

            // - Final stack size
            WriteLine($"- Final stack size: {actionHistory.Count}");

            // - List remaining actions (if any)
            if (actionHistory.Count > 0)
            {
                WriteLine("- Remaining actions in stack: ");
                int position = actionHistory.Count;
                foreach (string action in actionHistory)
                {
                    WriteLine($"   {position:D2}. {action}");
                    position--;
                }
            }
            else
            {
                WriteLine("✨ Stack was cleared before exit ✨");
            }
            // - Encouraging message
            WriteLine("\nThank you for using the Stack Action History Manager!");
            WriteLine("Press any key to exit ...");
            // - Wait for keypress before exit
            ReadKey();
        }
    }
}
