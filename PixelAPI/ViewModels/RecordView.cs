using System;

namespace PixelAPI.ViewModels
{
    public class RecordView
    {
        public int Id { get; set; }
        public string SteamID64 { get; set; }
        public string PlayerName { get; set; }
        public string Map { get; set; }
        public int Mode { get; set; }
        public string ModeName { get; set; }
        public int Course { get; set; }
        public int Style { get; set; }
        public int Time { get; set; }
        public int Teleports { get; set; }
        public DateTime Created { get; set; }

        public int ServerId { get; set; }
        public string ServerName { get; set; }
    }

}