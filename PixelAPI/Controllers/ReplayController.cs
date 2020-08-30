using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PixelAPI.Data;
using PixelAPI.Models;
using PixelAPI.Models.Enums;

namespace PixelAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReplayController : Controller
    {
        private readonly PixelContext _db;
        private readonly ILogger<ReplayController> _logger;

        public ReplayController(PixelContext db, ILogger<ReplayController> logger)
        {
            _db = db;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReplay(long id)
        {
            var record = await _db.Records.FindAsync(id);
            if (record == null)
                return NotFound("No record was found with record id of " + id);
            
            var replay = await _db.Replays.FindAsync(id);
            if (replay == null)
                return NotFound("No replay was found for this record id");
            return File(replay.Data, "application/octet-stream", $"{record.MapName}_{record.Mode}_{record.SteamID64}_{record.Id}.replay");
        }

        [HttpPost]
        [RequestSizeLimit(104857600)] // 100MB
        public async Task<IActionResult> SubmitReplay()
        {
            // Simple protection
            // if (Request.Headers["X-Api-Key"] != "Kurisu")
            //     return Unauthorized();

            // Request.Body
            long? id = await SaveReplay(Request.Body);

            return Json(new
            {
                success = id.HasValue,
                id = id.HasValue ? id.Value : -1
            });
        }

        private async Task<long?> SaveReplay(Stream requestBody)
        {
            try
            {
                using var ms = new MemoryStream();
                await requestBody.CopyToAsync(ms);
                ms.Position = 0;
                var replayFile = new ReplayFile(ms);

                // Add/Ignore player
                var player = await _db.Players.FindAsync(ConvertToSteamID64(replayFile.SteamId));
                if (player == null)
                {
                    player = new Player
                    {
                        SteamID64 = ConvertToSteamID64(replayFile.SteamId),
                        Name = replayFile.PlayerName
                    };
                    await _db.AddAsync(player);
                    _logger.LogInformation("Added player {SteamID64}", player.SteamID64);
                }
                else
                {
                    player.Updated = DateTime.UtcNow;
                    player.Name = replayFile.PlayerName;
                    _db.Update(player);
                    _logger.LogInformation("Updated player {SteamID64}", player.SteamID64);
                }

                // Add/Ignore Map
                var map = await _db.Maps.FindAsync(replayFile.MapName);
                if (map == null)
                {
                    map = new Map
                    {
                        Name = replayFile.MapName
                    };
                    await _db.AddAsync(map);
                }
                
                await _db.SaveChangesAsync();

                // Add Record
                var record = new Record
                {
                    SteamID64 = player.SteamID64,
                    MapName = replayFile.MapName,
                    Mode = (GokzMode) replayFile.Mode,
                    Course = replayFile.Course,
                    Style = (int) replayFile.Style,
                    Time = (long) Math.Floor(replayFile.Time.TotalMilliseconds),
                    Teleports = replayFile.TeleportsUsed
                };
                await _db.AddAsync(record);
                await _db.SaveChangesAsync();
                _logger.LogInformation("Record added for {Name} ({SteamID64}) on {MapName}", player.Name, player.SteamID64, replayFile.MapName);

                // Add Replay
                using var strippedStream = new MemoryStream();
                replayFile.Write(strippedStream);
                strippedStream.Position = 0;
                var replay = new Replay
                {
                    RecordId = record.Id,
                    Data = strippedStream.ToArray()
                };
                await _db.AddAsync(replay);

                await _db.SaveChangesAsync();
                return record.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to process replay");
                return null;
            }
        }

        private static long ConvertToSteamID64(int steamId32)
        {
            return steamId32 + 76561197960265728L;
        }

    }
}