using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TumbleTrack.Models
{
    public class GymnastEvent
    {
        [Key]
        public int GymnastEventId { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,4)")]
        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public decimal Score { get; set; }

        [StringLength(255)]
        public string Notes { get; set; }

        // Related Data

        [Required]
        public int GymnastId { get; set; }
        [Required]
        public Gymnast Gymnast { get; set; }
        
        [Required]
        public int EventId { get; set; }
        [Required]
        public Event Event { get; set; }
    }
}