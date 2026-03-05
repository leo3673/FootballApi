using FootballApi.DTOs;

namespace FootballApi.Service
{
    public interface ILeagueService
    {
        Task<IEnumerable<LeagueStandingDto>> GetLeagueStandingsAsync(int leagueId);
    }
}
