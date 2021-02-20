﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CypherHash.Models;

namespace CypherHash.Data
{
    public class CypherHashContext: DbContext
    {
        public CypherHashContext(DbContextOptions<CypherHashContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
    }
}
