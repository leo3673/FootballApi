using AutoMapper;
using FootballApi.DTOs;
using FootballApi.Models;

namespace FootballApi.Profiles
{
    public class MatchProfile : Profile
    {
        public MatchProfile()
        {
            CreateMap<Match, MatchDetailDto>()
                .ForMember(dest => dest.homeClub, opt => opt.MapFrom(src => src.HomeClub.ClubName))
                .ForMember(dest => dest.awayClub, opt => opt.MapFrom(src => src.AwayClub.ClubName));
        }
    }
}
