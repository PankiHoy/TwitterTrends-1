using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Core;

namespace DataProcessing
{
    class StatesToDisplayContext:DbContext
    {
        public StatesToDisplayContext(DbContextOptions options) : base(options) { }

        public DbSet<State> states { get; set; }
    }
}
