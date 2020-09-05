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
    public class ServersController : Controller
    {
        private readonly PixelContext _db;
        private readonly IMapper _mapper;
        private readonly ILogger<ServersController> _logger;

        public ServersController(PixelContext db, IMapper mapper, ILogger<ServersController> logger)
        {
            _db = db;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<ResultView>> GetServers([FromQuery] int offset, [FromQuery] int limit)
        {
            var query = _db.Servers.AsQueryable();
            var total = await query.CountAsync();

            if (offset > 0)
                query = query.Skip(offset);
            if (limit > 0)
                query = query.Take(limit);

            var servers = await query.ToListAsync();

            var result = new ResultView
            {
                Offset = 0,
                Count = servers.Count,
                Total = total,
                Data = _mapper.Map<List<ServerView>>(servers)
            };
            return result;
        }
    }
}