using AutoMapper;
using FootballApi.DTOs;
using FootballApi.Models;

namespace FootballApi.Profiles
{
    public class FacilityProfile : Profile
    {
        public FacilityProfile()
        {
            CreateMap<Facility, FacilityDto>();
        }
    }
}
