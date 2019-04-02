using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TeamCrud.Models
{
    public class PlayerContext : DbContext
    {
        public PlayerContext(DbContextOptions<PlayerContext> options)
            : base(options)
        {
        }
        public DbSet<PlayerItem> PlayerItems { get; set; }
    }
}
