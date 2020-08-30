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
    public class RecordsController : Controller
    {
        private readonly PixelContext _db;
        private readonly IMapper _mapper;
        private readonly ILogger<RecordsController> _logger;

        public RecordsController(PixelContext db, IMapper mapper, ILogger<RecordsController> logger)
        {
            _db = db;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetRecords()
        {
            var query = _db.Records.AsQueryable();
            query = query.Include(rec => rec.Player);
            var records = await query.ToListAsync();
            return Json(_mapper.Map<List<RecordView>>(records));
        }
    }
}