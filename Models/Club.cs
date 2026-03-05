using System.ComponentModel.DataAnnotations;

namespace FootballApi.Models
{
    public class ClubRequest
    {
        public int LeagueId { get; set; }
    }
    public class Club
    {
        [Key]
        public int ClubID { get; set; }
        public string? ClubName { get; set; }
        public int EstablishedYear{ get; set; }
        public string? Stadium { get; set; }
        public int Capacity{ get; set; }
        public int LeagueId { get; set; }

    }
}
