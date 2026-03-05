namespace FootballApi.DTOs
{
    public class CoachDto
    {
        public int CoachID { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int ExperienceYears { get; set; }
        public int LeagueId { get; set; }
        public string ClubName { get; set; } = string.Empty;
    }
}
