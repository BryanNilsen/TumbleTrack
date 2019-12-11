using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TumbleTrack.Models;

namespace TumbleTrack.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Gymnast> Gymnasts { get; set; }
        public DbSet<GymnastEvent> GymnastEvents { get; set; }
        public DbSet<Media> Medias { get; set; }
        public DbSet<Meet> Meets { get; set; }

    }
}
