using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BHopClassLib;
using Microsoft.EntityFrameworkCore;

namespace BHopsREST.Managers
{
    public class HopsContext : DbContext
    {
        public HopsContext(DbContextOptions<HopsContext> options) : base(options)
        {

        }

        public DbSet<Hop> Hops { get; set; }
    }
}
