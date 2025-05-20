using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace HP21I8
{
    public static class Program
    {
        public static void Main()
        {
            try
            {
                IMatchRepository repository = new JsonFileMatchRepository("matches.json");
                var statsService = new StatisticsService(repository);
                var ui = new ConsoleUi(statsService);

                ui.Run();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[VÉGZETES] {ex.Message}");
                Environment.Exit(1);
            }
        }
    }
}
