using System.ComponentModel.DataAnnotations;

namespace FootballApi.Models
{
    public class CoachRequest
    {
        public int LeagueId { get; set; }
    }

    public class Coach
    {
        [Key]
        public int CoachID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int ExperienceYears { get; set; }
        public int ClubID { get; set; }
        public int LeagueId { get; set; }    
        public Club? Club { get; set; }
    }
}
