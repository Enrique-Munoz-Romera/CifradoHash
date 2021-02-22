using CifradoHash.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CifradoHash.Data
{
    public class CypherHashContext:DbContext
    {
        public CypherHashContext(DbContextOptions<CypherHashContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
    }
}
