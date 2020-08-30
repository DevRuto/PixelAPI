using System;
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
        public async Task<ActionResult<ResultView>> GetRecords([FromQuery] int offset, [FromQuery] int limit)
        {
            var query = _db.Records.AsQueryable();
            var total = await query.CountAsync();

            query = query.Include(rec => rec.Player);
            if (offset > 0)
                query = query.Skip(offset);
            if (limit > 0)
                query = query.Take(limit);

            var records = await query.ToListAsync();

            var result = new ResultView
            {
                Offset = Math.Max(offset, 0),
                Count = records.Count,
                Total = total,
                Data = _mapper.Map<List<RecordView>>(records)
            };
            return result;
        }
    }
}