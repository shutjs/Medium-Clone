using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using blogv2.Models;

namespace blogv2.Data
{
    public class blogv2Context : DbContext
    {
        public blogv2Context (DbContextOptions<blogv2Context> options)
            : base(options)
        {
        }

        public DbSet<blogv2.Models.Makaleler> Makaleler { get; set; } = default!;
        public DbSet<blogv2.Models.Kullanicilar> Kullanicilar { get; set; } = default!;

    }
}
