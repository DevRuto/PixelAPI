using System.Collections.Generic;
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
        public async Task<IActionResult> GetPlayers()
        {
            var players = await _db.Players.ToListAsync();
            return Json(_mapper.Map<List<PlayerView>>(players));
        }
    }
}