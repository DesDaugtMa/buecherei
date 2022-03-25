using Buecherei.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Buecherei.Data
{
    public class BuechereiDBContext : DbContext
    {
        public BuechereiDBContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder mB)
        {
            mB.Entity<Ausleihe>().HasKey(a => new
            {
                a.Zaehler
            });
        }

        public DbSet<Ausleihe> Ausleihe { get; set; }

        public DbSet<Buch> Buch { get; set; }

        public DbSet<SchuelerIn> SchuelerIn { get; set; }
    }
}
