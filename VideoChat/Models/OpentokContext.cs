using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VideoChat.Models
{
    public class OpentokContext : DbContext
    {
        public DbSet<Room> Rooms { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=VonageVideo.db");
    }
}
