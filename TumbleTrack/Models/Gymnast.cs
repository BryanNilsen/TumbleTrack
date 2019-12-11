using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TumbleTrack.Models
{
    public class Gymnast
    {
        [Key]
        public int GymnastId { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [NotMapped]
        [Display(Name = "Full Name")]
        public string FullName => $"{FirstName} {LastName}";

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        // Related Data

        [Required]
        public string UserId { get; set; }

        [Required]
        public ApplicationUser User { get; set; }


        public virtual ICollection<Media> Medias { get; set; }
        public virtual ICollection<Meet> Meets { get; set; }
        public virtual ICollection<GymnastEvent> GymnastEvents { get; set; }

    }
}
