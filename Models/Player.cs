using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootballApi.Models
{
    [Table("players")]
    public class Player
    {        
        public int ClubID { get; set; }
        public string? Country { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [Key]
        public int PlayerID { get; set; }
        public string? Position { get; set; }
        public string? Season { get; set; }
        public Club? Club { get; set; }
        public PlayerSeasonStats? PlayerSeasonStats { get; set; }
    }

    [Table("player_season_stats")]
    public class PlayerSeasonStats
    {
        public int? Assists { get; set; }
        public int ClubID { get; set; }
        public int? Goals { get; set; }
        public string? Season { get; set; }
        [Key]
        public int StatID { get; set; }
        [ForeignKey("Players")]
        public int PlayerID { get; set; }
        public Player? Player { get; set; }

    }
}
