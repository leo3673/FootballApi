namespace FootballApi.DTOs
{
    public class PlayerAssistsBySeasonDto
    {
        public int? Assists { get; set; }
        public string? Season { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }

    public class PlayerDetailDto
    {
        public int? Assists { get; set; }
        public string? ClubName { get; set; }
        public string? Country { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? FirstName { get; set; }
        public int? Goals { get; set; }
        public string? LastName { get; set; }
        public string? Position { get; set; }
    }

    public class PlayerListByClubDto
    {
        public DateTime DateOfBirth { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Position { get; set; }
    }

    public class PlayerListByLeagueAndPositionDto
    {
        public DateTime DateOfBirth { get; set; }
        public string? Country { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int PlayerID { get; set; }
        public string? Position { get; set; }
        public int? LeagueId { get; set; }
    }

    public class PlayerGoalsBySeasonDto
    {
        public int? Goals { get; set; }
        public string? Season { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }

    public class PlayerPositionsListDto
    {
        public string? Position { get; set; }
    }
}
