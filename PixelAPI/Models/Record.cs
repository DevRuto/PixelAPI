using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PixelAPI.Models.Enums;

namespace PixelAPI.Models
{
    public class Record
    {
        [Key]
        public long Id { get; set; }

        public long SteamID64 { get; set; }

        [Required]
        public string MapName { get; set; }

        public GokzMode Mode { get; set; }
        public int Course { get; set; }
        public int Style { get; set; }
        public long Time { get; set; }
        public int Teleports { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;

        [ForeignKey("MapName")]
        public Map Map { get; set; }

        [ForeignKey("SteamID64")]
        public Player Player { get; set; }

    }
}