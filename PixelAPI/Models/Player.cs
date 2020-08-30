using System;
using System.ComponentModel.DataAnnotations;

namespace PixelAPI.Models
{
    public class Player
    {
        [Key]
        public long SteamID64 { get; set; }

        public string Name { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime Updated { get; set; }

    }
}