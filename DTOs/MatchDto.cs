namespace FootballApi.DTOs
{
    public class MatchDetailDto
    {
        public int matchID { get; set; }
        public DateTime? matchDate { get; set; }
        public int homeGoals { get; set; }
        public int awayGoals { get; set; }
        public string? homeClub { get; set; }
        public string? awayClub { get; set; }
    }

    public class WinLoseMatchStatDto
    {
        public int wins { get; set; }
        public int draws { get; set; }
        public int losses { get; set; }
    }

    public class GoalsPerMatchStatDto
    {
        public DateTime? matchDate { get; set; }
        public int goals { get; set; }
        public string season { get; set; }
        public string opponent { get; set; }
    }

    public class LeagueStandingDto
    {
        public string clubName { get; set; }
        public int draws { get; set; }
        public int goalsAgainst { get; set; }
        public int goalDifference => goalsFor - goalsAgainst;
        public int goalsFor { get; set; }
        public int losses { get; set; }
        public int played { get; set; }
        public int points => wins * 3 + draws;
        public string season { get; set; }
        public int wins { get; set; }
    }
}
