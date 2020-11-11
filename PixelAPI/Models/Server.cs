using System.ComponentModel.DataAnnotations;

namespace PixelAPI.Models
{
    public class Server
    {
        [Key]
        public int Id { get ; set; }

        public string Name { get; set; }

        public string Ip { get; set; }
        public short Port { get; set; }
    }
}