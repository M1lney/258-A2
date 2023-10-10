using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using _258_A2_Tom_Milne.Models;

namespace Data
{
    public class A2DbContext : DbContext
    {
        public A2DbContext (DbContextOptions<A2DbContext> options)
            : base(options)
        {
        }

        public DbSet<_258_A2_Tom_Milne.Models.Project> Project { get; set; } = default!;

        public DbSet<_258_A2_Tom_Milne.Models.ProjectTask> ProjectTask { get; set; } = default!;
    }
}
