using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP21I8
{
    public sealed class StatisticsService
    {
        private readonly List<Match> _matches;
        public StatisticsService(IMatchRepository repository)
        {
            _matches = repository?.Load() ?? throw new ArgumentNullException(nameof(repository));
        }

        public IEnumerable<(string Team, int Wins)> GetTeamWins(string season) =>
            _matches
                .Where(m => m.Season == season)                     
                .Select(m => new                                   // győztes
                {
                    Winner = m.SetsHome > m.SetsAway ? m.TeamHome : m.TeamAway
                })
                .GroupBy(x => x.Winner)                            
                .Select(g => (Team: g.Key, Wins: g.Count()))
                .OrderByDescending(t => t.Wins)                    
                .ToList();

        public double GetAverageSetsPerMatch(string season) =>
            _matches
                .Where(m => m.Season == season)
                .Select(m => m.SetsHome + m.SetsAway)
                .Average();                                        

        public IEnumerable<string> Seasons => _matches.Select(m => m.Season)
                                                       .Distinct()
                                                       .OrderByDescending(s => s);
    }
}
