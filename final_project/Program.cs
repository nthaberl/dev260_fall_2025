using static System.Console;

namespace final_project
{
    /// <summary>
    /// Entry point for the BOTW Campfire
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            //Using a try-catch block here for "top-level" error handling.
            // Acts as a safety net that catches any unhandled exceptions that might
            // bubble up from deeper in the application.
            try
            {
                var menu = new MenuNavigation();
                
                // Start the main application loop
                menu.Start();
            }
            catch (Exception ex)
            {
                WriteLine($"❌ Fatal error: {ex.Message}");
                WriteLine("Press any key to exit...");
                ReadKey();
            }
        }
    }
}