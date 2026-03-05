using System.ComponentModel.DataAnnotations;

namespace FootballApi.Models
{
    public class ClubMatchRequest
    {
        public int ClubID { get; set; }
        public string? Season { get; set; }
    }

    public class Match
    {
        [Key]
        public int MatchID { get; set; }
        public int HomeClubID { get; set; }
        public int AwayClubID { get; set; }
        public int HomeGoals { get; set; }
        public int AwayGoals { get; set; }
        public DateTime? MatchDate { get; set; }
        public int LeagueId { get; set; }
        public string? Season { get; set; }
        public Club? HomeClub { get; set; }
        public Club? AwayClub { get; set; }

    }
}
