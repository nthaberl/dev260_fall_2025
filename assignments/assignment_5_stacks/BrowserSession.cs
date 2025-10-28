using System;
using static System.Console;
using System.Collections.Generic;

namespace Assignment5
{
    /// <summary>
    /// Manages browser navigation state with back and forward stacks
    /// </summary>
    public class BrowserSession
    {
        private Stack<WebPage> backStack;
        private Stack<WebPage> forwardStack;
        private WebPage? currentPage;

        public WebPage? CurrentPage => currentPage;
        public int BackHistoryCount => backStack.Count;
        public int ForwardHistoryCount => forwardStack.Count;
        public bool CanGoBack => backStack.Count > 0;
        public bool CanGoForward => forwardStack.Count > 0;

        public BrowserSession()
        {
            backStack = new Stack<WebPage>();
            forwardStack = new Stack<WebPage>();
            currentPage = null;
        }

        /// <summary>
        /// Navigate to a new URL
        /// TODO: Implement this method
        /// - If there's a current page, push it to back stack
        /// - Clear the forward stack (new navigation invalidates forward history)
        /// - Set the new page as current
        /// </summary>
        public void VisitUrl(string url, string title)
        {
            // TODO: Implement navigation logic
            WebPage newPage = new WebPage(url, title);
            if (currentPage != null)
            {
                backStack.Push(currentPage);
                forwardStack.Clear();
                currentPage = newPage;
            }
            else
            {
                currentPage = newPage;
            }
        }

        /// <summary>
        /// Navigate back to previous page
        /// TODO: Implement this method
        /// - Check if back navigation is possible
        /// - Move current page to forward stack
        /// - Pop page from back stack and make it current
        /// </summary>
        public bool GoBack()
        {
            if (backStack.Count > 0)
            {
                forwardStack.Push(currentPage);
                currentPage = backStack.Pop();
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Navigate forward to next page
        /// TODO: Implement this method
        /// - Check if forward navigation is possible
        /// - Move current page to back stack
        /// - Pop page from forward stack and make it current
        /// </summary>
        public bool GoForward()
        {
            if (forwardStack.Count > 0)
            {
                backStack.Push(currentPage);
                currentPage = forwardStack.Pop();
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Get navigation status information
        /// </summary>
        public string GetNavigationStatus()
        {
            var status = $"📊 Navigation Status:\n";
            status += $"   Back History: {BackHistoryCount} pages\n";
            status += $"   Forward History: {ForwardHistoryCount} pages\n";
            status += $"   Can Go Back: {(CanGoBack ? "✅ Yes" : "❌ No")}\n";
            status += $"   Can Go Forward: {(CanGoForward ? "✅ Yes" : "❌ No")}";
            return status;
        }

        /// <summary>
        /// Display back history (most recent first)
        /// TODO: Implement this method
        /// Expected output format:
        /// 📚 Back History (most recent first):
        ///    1. Google Search (https://www.google.com)
        ///    2. GitHub Homepage (https://github.com)
        ///    3. Stack Overflow (https://stackoverflow.com)
        /// 
        /// If empty, show: "   (No back history)"
        /// Use foreach to iterate through backStack (it gives LIFO order automatically)
        /// </summary>
        public void DisplayBackHistory()
        {
            // TODO: Implement back history display
            // 1. Print header: "📚 Back History (most recent first):"
            WriteLine("📚 Back History (most recent first):");
            
            // 2. Check if backStack.Count == 0, if so print "   (No back history)" and return
            if (backStack.Count == 0)
            {
                WriteLine("   (No back history)");
                return;
            }
            else
            {
                int position = 1;
                // 3. Use foreach loop with backStack to display pages
                foreach (WebPage page in backStack)
                {
                    // 4. Show position number, page title, and URL for each page
                    // 5. Format: "   {position}. {page.Title} ({page.Url})"
                    WriteLine($"   {position}. {page.Title} ({page.Url})");
                    position++;
                }
            }
        }

        /// <summary>
        /// Display forward history (next page first)
        /// TODO: Implement this method
        /// Expected output format:
        /// 📖 Forward History (next page first):
        ///    1. Documentation Page (https://docs.microsoft.com)
        ///    2. YouTube (https://www.youtube.com)
        /// 
        /// If empty, show: "   (No forward history)"
        /// Use foreach to iterate through forwardStack (it gives LIFO order automatically)
        /// </summary>
        public void DisplayForwardHistory()
        {
            // TODO: Implement forward history display
            // 1. Print header: "📖 Forward History (next page first):"
            WriteLine("📖 Forward History (next page first): ");
            // 2. Check if forwardStack.Count == 0, if so print "   (No forward history)" and return
            if (forwardStack.Count == 0)
            {
                WriteLine("   (No back history)");
                return;
            }
            // 3. Use foreach loop with forwardStack to display pages
            else
            {
                int position = 1;
                foreach (WebPage page in forwardStack)
                {
                    // 4. Show position number, page title, and URL for each page
                    // 5. Format: "   {position}. {page.Title} ({page.Url})"
                    WriteLine($"   {position}. {page.Title} ({page.Url})");
                    position++;
                }
            }
        }

        /// <summary>
        /// Clear all navigation history
        /// TODO: Implement this method
        /// Expected behavior:
        /// - Count total pages to be cleared (backStack.Count + forwardStack.Count)
        /// - Clear both backStack and forwardStack
        /// - Print confirmation: "✅ Cleared {totalCleared} pages from navigation history."
        /// Note: This does NOT clear the current page, only the navigation history
        /// </summary>
        public void ClearHistory()
        {
            // TODO: Implement clear history functionality
            // 1. Calculate total pages: int totalCleared = backStack.Count + forwardStack.Count;
            int totalCleared = backStack.Count + forwardStack.Count;

            // 2. Clear both stacks: backStack.Clear() and forwardStack.Clear()
            backStack.Clear();
            forwardStack.Clear();

            // 3. Print confirmation message with count of cleared pages
            WriteLine($"Cleared {totalCleared} pages from navigation history! Navigation history is squeaky clean ✨");
        }
    }
}