using AutoMapper;
using FootballApi.DTOs;
using FootballApi.Models;

namespace FootballApi.Profiles
{
    public class PlayerProfile : Profile
    {
        public PlayerProfile()
        {
            CreateMap<Player, PlayerDetailDto>()
                .ForMember(dest => dest.Assists, opt => opt.MapFrom(src => src.PlayerSeasonStats.Assists))
                .ForMember(dest => dest.ClubName, opt => opt.MapFrom(src => src.Club.ClubName))
                .ForMember(dest => dest.Goals, opt => opt.MapFrom(src => src.PlayerSeasonStats.Goals));

            CreateMap<Player, PlayerListByClubDto>();

            CreateMap<Player, PlayerListByLeagueAndPositionDto>()
                .ForMember(dest => dest.LeagueId, opt => opt.MapFrom(src => src.Club.LeagueId));

            CreateMap<PlayerSeasonStats, PlayerAssistsBySeasonDto>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Player.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Player.LastName));

            CreateMap<PlayerSeasonStats, PlayerGoalsBySeasonDto>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Player.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Player.LastName));
        }
    }
}
