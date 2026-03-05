using System.ComponentModel.DataAnnotations;

namespace FootballApi.Models
{
    public class Facility
    {
        [Key]
        public int FacilityID { get; set; }
        public string? FacilityName { get; set; }
        public string? Description { get; set; }
        public string? FacilityType { get; set; }
        public string? Location { get; set; }
        public int ClubId { get; set; }
    }
}
