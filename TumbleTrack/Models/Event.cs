using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TumbleTrack.Models
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }

        [Required]
        public string Name { get; set; }

        // Related Data

        [Required]
        public int MeetId { get; set; }

        [Required]
        public Meet Meet { get; set; }

        public virtual ICollection<GymnastEvent> GymnastEvents { get; set; }
    }
}