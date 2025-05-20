using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP21I8
{
    public sealed class ConsoleUi
    {
        private readonly StatisticsService _stats;
        public ConsoleUi(StatisticsService stats) => _stats = stats;

        public void Run()
        {
            Console.WriteLine("Röplabda bajnokság – statisztikák\n");

            foreach (var season in _stats.Seasons)
            {
                Console.WriteLine($"Szezon: {season}");
                Console.WriteLine("Legtöbb győzelem:");
                foreach (var (Team, Wins) in _stats.GetTeamWins(season))
                {
                    Console.WriteLine($"  {Team}: {Wins} győzelem");
                }

                var avgSets = _stats.GetAverageSetsPerMatch(season);
                Console.WriteLine($"Átlagos lejátszott szettek meccsenként: {avgSets:F2}\n");
            }
        }
    }

}
