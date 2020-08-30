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
    public class MapsController : Controller
    {
        private readonly PixelContext _db;
        private readonly IMapper _mapper;
        private readonly ILogger<MapsController> _logger;

        public MapsController(PixelContext db, IMapper mapper, ILogger<MapsController> logger)
        {
            _db = db;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<ResultView>> GetMaps([FromQuery] int offset, [FromQuery] int limit)
        {
            var query = _db.Maps.AsQueryable();
            var total = await query.CountAsync();

            if (offset > 0)
                query = query.Skip(offset);
            if (limit > 0)
                query = query.Take(limit);

            var maps = await query.ToListAsync();

            var result = new ResultView
            {
                Offset = 0,
                Count = maps.Count,
                Total = total,
                Data = _mapper.Map<List<MapView>>(maps)
            };
            return result;
        }
    }
}