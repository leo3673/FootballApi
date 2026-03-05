using AutoMapper;
using FootballApi.DTOs;
using FootballApi.Models;

namespace FootballApi.Profiles
{
    public class ClubProfile : Profile
    {
        public ClubProfile()
        {
            CreateMap<Club, ClubDto>();
        }
    }
}
