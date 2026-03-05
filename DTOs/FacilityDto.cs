namespace FootballApi.DTOs
{
    public class FacilityDto
    {
        public string FacilityName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string FacilityType { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public int ClubId { get; set; }
    }
}
