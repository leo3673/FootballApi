namespace FootballApi.DTOs
{
    public class ClubDto
    {
        public int ClubID { get; set; }
        public string ClubName { get; set; } = string.Empty;
        public int? EstablishedYear { get; set; }
        public string Stadium { get; set; } = string.Empty;
        public int? Capacity { get; set; }
        public int LeagueId { get; set; }
    }
}
