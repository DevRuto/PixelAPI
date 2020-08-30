using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PixelAPI.Data;
using PixelAPI.ViewModels;

namespace PixelAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayersController : Controller
    {
        private readonly PixelContext _db;
        private readonly IMapper _mapper;
        private readonly ILogger<PlayersController> _logger;

        public PlayersController(PixelContext db, IMapper mapper, ILogger<PlayersController> logger)
        {
            _db = db;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<ResultView>> GetPlayers([FromQuery] int offset, [FromQuery] int limit)
        {
            var query = _db.Players.AsQueryable();
            var total = await query.CountAsync();

            if (offset > 0)
                query = query.Skip(offset);
            if (limit > 0)
                query = query.Take(limit);

            var players  = await query.ToListAsync();

            var result = new ResultView
            {
                Offset = 0,
                Count = players.Count,
                Total = total,
                Data = _mapper.Map<List<PlayerView>>(players)
            };
            return result;
        }
    }
}