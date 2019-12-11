using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TumbleTrack.Models
{
    public class Meet
    {
        [Key]
        public int MeetId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }

        // Related Data

        [Required]
        public string UserId { get; set; }

        [Required]
        public ApplicationUser User { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }
}
