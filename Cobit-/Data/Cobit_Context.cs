using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Cobit.Models;
using Cobit_.Models;

namespace Cobit_.Data
{
    public class Cobit_Context : DbContext
    {
        public Cobit_Context (DbContextOptions<Cobit_Context> options)
            : base(options)
        {
        }

        public DbSet<Cobit.Models.Person> Person { get; set; } = default!;
        public DbSet<Cobit.Models.AGModel> AGItems { get; set; } = default!;
        public DbSet<Cobit_.Models.EDMModel> EDMItems { get; set; } = default!;
    }
}
