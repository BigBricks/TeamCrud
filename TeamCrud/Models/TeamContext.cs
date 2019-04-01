using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TeamCrud.Models
{
    public class TeamContext : DbContext
         {
        public TeamContext(DbContextOptions<TeamContext> options)
            : base(options)
        {
        }

        public DbSet<TeamItem> TeamItems { get; set; }
        public DbSet<PlayerItem> PlayerItems { get; set; }
    }
}
