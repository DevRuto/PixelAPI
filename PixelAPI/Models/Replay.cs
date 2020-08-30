using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PixelAPI.Models
{
    public class Replay
    {
        [Key]
        public long RecordId { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;

        [Column(TypeName = "longblob")]
        public byte[] Data { get; set; }

        [ForeignKey("RecordId")]
        public Record Record { get; set; }
    }
}