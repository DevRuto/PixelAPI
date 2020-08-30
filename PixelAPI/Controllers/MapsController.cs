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
        public async Task<IActionResult> GetMaps()
        {
            var maps = await _db.Maps.ToListAsync();
            return Json(_mapper.Map<List<MapView>>(maps));
        }
    }
}