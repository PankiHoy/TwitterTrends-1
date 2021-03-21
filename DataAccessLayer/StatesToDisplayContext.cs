using System;
using Microsoft.EntityFrameworkCore;
using Core;

namespace DataAccessLayer
{
    public class StatesToDisplayContext:DbContext
    {
        public StatesToDisplayContext(DbContextOptions options) : base(options) { }

        public DbSet<State> states { get; set; }
    }
}
