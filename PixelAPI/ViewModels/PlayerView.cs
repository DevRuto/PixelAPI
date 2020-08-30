using System;

namespace PixelAPI.ViewModels
{
    public class PlayerView
    {
        public string SteamID64 { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }

}