using System.ComponentModel.DataAnnotations;

namespace TumbleTrack.Models
{
    public class Media
    {
        [Key]
        public int MediaId { get; set; }

        [Required]
        [Display(Name = "File URL")]
        public string FileUrl { get; set; }

        // Related Data

        [Required]
        public int GymnastId { get; set; }

        [Required]
        public Gymnast Gymnast { get; set; }

        public int? EventId { get; set; }
        public Event Event { get; set; }
    }
}