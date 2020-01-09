using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tabi.Models;

namespace Tabi.Data
{
    public class TabiContext : DbContext
    {
        public TabiContext (DbContextOptions<TabiContext> options)
            : base(options)
        {
        }

        public DbSet<Tabi.Models.Entry> Entry { get; set; }

        public DbSet<Tabi.Models.Survey> Survey { get; set; }
    }
}
