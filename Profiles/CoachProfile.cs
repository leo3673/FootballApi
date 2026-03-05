using AutoMapper;
using FootballApi.DTOs;
using FootballApi.Models;

namespace FootballApi.Profiles
{
    public class CoachProfile : Profile
    {
        public CoachProfile()
        {
            CreateMap<Coach, CoachDto>()
                .ForMember(dest => dest.ClubName, opt => opt.MapFrom(src => src.Club.ClubName));
        }
    }
}
