using AutoMapper;
using FootballApi.DTOs;
using FootballApi.Models;
using FootballApi.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FootballApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FootballController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILeagueService _leagueService;
        private readonly IMapper _mapper;

        public FootballController(AppDbContext context, ILeagueService leagueService, IMapper mapper)
        {
            _context = context;
            _leagueService = leagueService;
            _mapper = mapper;
        }
        [HttpGet("GetClubsByLeagueId")]
        public async Task<ActionResult<IEnumerable<ClubDto>>> GetClubsByLeagueId(int leagueId)
        {
            var clubs = await _context.clubs
                .Where(c => c.LeagueId == leagueId)
                .ToListAsync();

            var clubDtos = _mapper.Map<IEnumerable<ClubDto>>(clubs);

            return Ok(clubDtos);
        }
        [HttpGet("GetClubFacilitiesByClubId")]
        public async Task<ActionResult<IEnumerable<FacilityDto>>> GetClubFacilitiesByClubId(int clubId)
        {
            var facilities = await _context.facilities
                .Where(c => c.ClubId == clubId)
                .ToListAsync();

            var facilityDtos = _mapper.Map<IEnumerable<FacilityDto>>(facilities);

            return Ok(facilityDtos);
        }
        [HttpGet("GetCoaches")]
        public async Task<ActionResult<IEnumerable<Coach>>> GetCoaches()
        {
            return await _context.coaches.ToListAsync();
        }
        [HttpGet("GetCoachesByLeagueId")]
        public async Task<ActionResult<IEnumerable<CoachDto>>> GetCoachesByLeagueId(int leagueId)
        {
            var coaches = await _context.coaches
                .Include(c => c.Club)
                .Where(c => c.LeagueId == leagueId)
                .ToListAsync();

            var coachDtos = _mapper.Map<IEnumerable<CoachDto>>(coaches);

            return Ok(coachDtos);
        }       
        [HttpGet("GetLeagues")]
        public async Task<ActionResult<IEnumerable<League>>> GetLeagues()
        {
            return await _context.leagues.ToListAsync();
        }
        [HttpGet("GetLeagueNameById")]
        public async Task<string> GetLeagueNameById(int leagueId)
        {
            List<League> League = await _context.leagues
                .Where(p => p.LeagueId == leagueId)
                .ToListAsync();
            return League[0].LeagueName;
        }
        [HttpGet("GetLeagueStandingsByLeagueId")]
        public async Task<IActionResult> GetLeagueStandingsByLeagueId(int leagueId)
        {
            var standings = await _leagueService.GetLeagueStandingsAsync(leagueId);
            return Ok(standings);
        }
        [HttpGet("GetMatchDetailListByClubId")]
        public async Task<ActionResult<IEnumerable<MatchDetailDto>>> GetMatchDetailListByClubId(int clubId, string season)
        {
            var matchResultList = await _context.matches
                .Include(c => c.HomeClub)
                .Include(c => c.AwayClub)
                .Where(m => m.HomeClubID == clubId || m.AwayClubID == clubId)
                .ToListAsync();

            var matchDetailDtos = _mapper.Map<IEnumerable<MatchDetailDto>>(matchResultList);

            return Ok(matchDetailDtos);
        }
        [HttpGet("GetMatchGoalsByClubId")]
        public async Task<ActionResult<IEnumerable<GoalsPerMatchStatDto>>> GetMatchGoalsByClubId(int clubId, string season)
        {
            var matchtList = await _context.matches
                .Include(m => m.HomeClub)
                .Include(m => m.AwayClub)
                .Where(m => (m.HomeClubID == clubId || m.AwayClubID == clubId) && m.Season == season)
                .Select(m => new
                {
                    HomeClubName = m.HomeClub.ClubName,
                    AwayClubName = m.AwayClub.ClubName,
                    m.MatchDate,
                    m.HomeClubID,
                    m.AwayClubID,
                    m.HomeGoals,
                    m.AwayGoals,
                    m.Season
                })
                .OrderBy(m => m.MatchDate)
                .ToListAsync();

            var GoalsPerMatchStatDtos = matchtList.Select(m => new GoalsPerMatchStatDto
            {
                matchDate = m.MatchDate,
                goals = m.HomeClubID == clubId ? m.HomeGoals : m.AwayGoals,
                season = m.Season,
                opponent = m.HomeClubID == clubId ? m.AwayClubName : m.HomeClubName
            }).ToList();

            return Ok(GoalsPerMatchStatDtos);
        }
        [HttpGet("GetMatchWinLoseByClubId")]
        public async Task<ActionResult<IEnumerable<WinLoseMatchStatDto>>> GetMatchWinLoseByClubId(int clubId, string season)
        {
            var matchtList = await _context.matches
                .Include(m => m.HomeClub)
                .Include(m => m.AwayClub)
                .Where(m => (m.HomeClubID == clubId || m.AwayClubID == clubId) && m.Season == season)
                .Select(m => new
                {
                    HomeClubName = m.HomeClub.ClubName,
                    AwayClubName = m.AwayClub.ClubName,
                    m.HomeClubID,
                    m.AwayClubID,
                    m.HomeGoals,
                    m.AwayGoals
                })
                .ToListAsync();

            WinLoseMatchStatDto WinLoseResultDtos = new WinLoseMatchStatDto();

            WinLoseResultDtos.wins = matchtList.Count(m => m.HomeClubID == clubId && m.HomeGoals > m.AwayGoals ||
                                                         m.AwayClubID == clubId && m.AwayGoals > m.HomeGoals);
            WinLoseResultDtos.losses = matchtList.Count(m => m.HomeClubID == clubId && m.HomeGoals < m.AwayGoals ||
                                                         m.AwayClubID == clubId && m.AwayGoals < m.HomeGoals);
            WinLoseResultDtos.draws = matchtList.Count(m => m.HomeGoals == m.AwayGoals);

            return Ok(WinLoseResultDtos);
        }
        [HttpGet("GetPlayerDetailByPlayerId")]
        public async Task<ActionResult<PlayerDetailDto>> GetPlayerDetailByPlayerId(int playerId)
        {
            var playerDetail = await _context.players
                .Include(p => p.Club)
                .Include(p => p.PlayerSeasonStats)
                .Where(p => p.PlayerID == playerId)
                .FirstOrDefaultAsync(p => p.PlayerID == playerId);

            var playerDetailDto = _mapper.Map<PlayerDetailDto>(playerDetail);

            return Ok(playerDetailDto);
        }
        [HttpGet("GetPlayers")]
        public async Task<ActionResult<IEnumerable<Player>>> GetPlayers()
        {
            return await _context.players.ToListAsync();
        }
        [HttpGet("GetPlayersByName")]
        public async Task<ActionResult<IEnumerable<Player>>> GetPlayersByName(String lastName)
        {
            var playerList = await _context.players
                .Where(p => p.LastName == lastName)
                .ToListAsync();
            return playerList;
        }
        [HttpGet("GetPlayersListByClubId")]
        public async Task<ActionResult<IEnumerable<PlayerListByClubDto>>> GetPlayerListByClubId(int clubId, string season)
        {
            var playerList = await _context.players
                .Where(s => s.ClubID == clubId && s.Season == season)
                .OrderBy(s => s.LastName)
                .ToListAsync();

            var playerListByClubDtos = _mapper.Map<IEnumerable<PlayerListByClubDto>>(playerList);

            return Ok(playerListByClubDtos);
        }
        [HttpGet("GetPlayersListByLeagueAndPosition")]
        public async Task<ActionResult<IEnumerable<PlayerListByLeagueAndPositionDto>>> GetPlayerListByLeagueAndPosition(int leagueId, string position, string season)
        {
            var playerList = await _context.players
                .Include(c => c.Club)
                .Where(s => s.Club.LeagueId == leagueId && s.Position == position && s.Season == season)
                .OrderBy(s => s.LastName)
                .ToListAsync();

            var playerListByLeagueDtos = _mapper.Map<IEnumerable<PlayerListByLeagueAndPositionDto>>(playerList);

            return Ok(playerListByLeagueDtos);
        }
        [HttpGet("GetPlayerPositions")]
        public async Task<ActionResult<IEnumerable<PlayerPositionsListDto>>> GetPlayerPositions(int leagueId, string season)
        {
            // Custom order
            List<string> positionOrder = new List<string>
            {
                "Forward",
                "Midfielder",
                "Defender",
                "Goalkeeper"
            };

            var playerPostionsList = await _context.players
                .Where(s => s.Club.LeagueId == leagueId && s.Season == season)
                .Select(p => p.Position)
                .Distinct()
                .ToListAsync();

            // Apply custom order
            playerPostionsList = playerPostionsList
                .OrderBy(p => positionOrder.IndexOf(p ?? string.Empty) == -1
                    ? int.MaxValue
                    : positionOrder.IndexOf(p ?? string.Empty))
                .ToList();

            var playerPostionsListDto = playerPostionsList.Select(pos => new PlayerPositionsListDto { Position = pos }).ToList();

            return Ok(playerPostionsListDto);
        }
        [HttpGet("GetPlayersSeasonAssistByClubId")]
        public async Task<ActionResult<IEnumerable<PlayerAssistsBySeasonDto>>> GetPlayerSeasonAssistByClubId(int clubId, string season)
        {
            var playerAssistsList = await _context.playerSeasonStats
                .Include(c => c.Player)
                .Where(s => s.ClubID == clubId && s.Season == season)
                .OrderByDescending(s => s.Assists)
                .ToListAsync();

            var playerAssistsListDtos = _mapper.Map<IEnumerable<PlayerAssistsBySeasonDto>>(playerAssistsList);

            return Ok(playerAssistsListDtos);
        }
        [HttpGet("GetPlayersSeasonGoalByClubId")]
        public async Task<ActionResult<IEnumerable<PlayerGoalsBySeasonDto>>> GetPlayerSeasonGoalByClubId(int clubId, string season)
        {
            var playerGoalList = await _context.playerSeasonStats
                .Include(c => c.Player)
                .Where(s => s.ClubID == clubId && s.Season == season)
                .OrderByDescending(s => s.Goals)
                .ToListAsync();

            var playerGoalListDtos = _mapper.Map<IEnumerable<PlayerGoalsBySeasonDto>>(playerGoalList);

            return Ok(playerGoalListDtos);
        }
        [Authorize]
        [HttpPost("PostAddCoach")]
        public async Task<ActionResult<Coach>> AddCoach(Coach coach)
        {
            _context.coaches.Add(coach);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCoaches), new { id = coach.CoachID }, coach);
        }
        [Authorize]
        [HttpPost("PostAddPlayer")]
        public async Task<ActionResult<Player>> AddPlayer(Player player)
        {
            try
            {
                _context.players.Add(player);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetPlayers), new { id = player.PlayerID }, player);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }

    }
}
