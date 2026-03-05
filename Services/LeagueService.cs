using FootballApi.DTOs;
using Microsoft.EntityFrameworkCore;

namespace FootballApi.Service
{
    public class LeagueService : ILeagueService
    {
        private readonly AppDbContext _context;

        public LeagueService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LeagueStandingDto>> GetLeagueStandingsAsync(int leagueId)
        {
            var matches = await _context.matches
                .Include(m => m.HomeClub)
                .Include(m => m.AwayClub)
                .Where(m => m.LeagueId == leagueId)
                .ToListAsync();

            var standings = new Dictionary<int, LeagueStandingDto>();

            foreach (var match in matches)
            {
                if (match.AwayClubID == null || match.HomeClubID == null)
                    continue;

                // initialize clubs if not exist
                if (!standings.ContainsKey(match.AwayClubID))
                    standings[match.AwayClubID] = new LeagueStandingDto { clubName = match.AwayClub.ClubName };
                if (!standings.ContainsKey(match.HomeClubID))
                    standings[match.HomeClubID] = new LeagueStandingDto { clubName = match.HomeClub.ClubName };

                var away = standings[match.AwayClubID];
                var home = standings[match.HomeClubID];

                home.season = match.Season;
                away.season = match.Season;

                // update stats
                away.goalsAgainst += match.HomeGoals;
                away.goalsFor += match.AwayGoals;
                away.played++; home.played++;
                home.goalsAgainst += match.AwayGoals;
                home.goalsFor += match.HomeGoals;

                if (match.HomeGoals > match.AwayGoals) { home.wins++; away.losses++; }
                else if (match.HomeGoals < match.AwayGoals) { away.wins++; home.losses++; }
                else { home.draws++; away.draws++; }
            }

            return standings.Values
                .OrderByDescending(s => s.points)
                .ThenByDescending(s => s.goalDifference)
                .ThenByDescending(s => s.goalsFor)
                .ToList();
        }
    }
}
