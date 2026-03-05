using System.ComponentModel.DataAnnotations;

namespace FootballApi.Models
{
    public class LeagueRequest
    {
        public int LeagueId { get; set; }
    }

    public class League
    {
        [Key]
        public int LeagueId { get; set; }
        public string LeagueName { get; set; }
        public string Country { get; set; }
        public int Tier { get; set; }
        public int FoundedYear { get; set; }

    }
}
