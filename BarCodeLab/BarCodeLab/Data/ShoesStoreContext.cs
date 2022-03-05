using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BarCodeLab.Models;

namespace BarCodeLab.Data
{
    public class ShoesStoreContext : DbContext
    {
        public ShoesStoreContext (DbContextOptions<ShoesStoreContext> options)
            : base(options)
        {
        }

        public DbSet<BarCodeLab.Models.Shoes> Shoes { get; set; }

        public DbSet<BarCodeLab.Models.Brand> Brand { get; set; }

        public DbSet<BarCodeLab.Models.Country> Country { get; set; }
    }
}
