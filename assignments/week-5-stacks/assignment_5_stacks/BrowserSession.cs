using System;
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

        public void VisitUrl(string url, string title)
        {
            if (currentPage != null)
            {
                backStack.Push(currentPage);

            }
            forwardStack.Clear();
            currentPage = new WebPage(url, title);
        }

        public bool GoBack()
        {
            if (backStack.Count == 0)
            {
                return false;
            }
            else
            {
                forwardStack.Push(currentPage);
                currentPage = backStack.Pop();
                return true;
            }
        }

        public bool GoForward()
        {
            if (forwardStack.Count == 0)
            {
                return false;
            }
            else
            {
                backStack.Push(currentPage);
                currentPage = forwardStack.Pop();
                return true;
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

        public void DisplayBackHistory()
        {
            Console.WriteLine("📚 Back History (most recent first):");
            if (backStack.Count == 0)
            {
                Console.WriteLine("   (No back history)");
                return;
            }
            else
            {
                int position = backStack.Count;
                foreach (WebPage backPage in backStack)
                {
                    Console.WriteLine($"   {position}. {backPage.Title} ({backPage.Url})");
                    position--;
                }
            }
        }

        public void DisplayForwardHistory()
        {
            Console.WriteLine("📖 Forward History (next page first):");
            if (forwardStack.Count == 0)
            {
                Console.WriteLine("   (No forward history)");
                return;
            }
            else
            {
                int position = forwardStack.Count;
                foreach (WebPage forwardPage in forwardStack)
                {
                    Console.WriteLine($"   {position}. {forwardPage.Title} ({forwardPage.Url})");
                    position--;
                }
            }
        }

        /// <summary>
        /// Clear all navigation history
        /// </summary>
        public void ClearHistory()
        {
            int totalCleared = backStack.Count + forwardStack.Count;
            backStack.Clear();
            forwardStack.Clear();
            Console.WriteLine($"Cleared {totalCleared} pages from navigation history.");
        }
    }
}