using ContasPagar.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContasPagar.InfraStructure
{
    public class ContasContext : DbContext
    {
        public ContasContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<ContaPagar> ContaPagar { get; set; }

        public DbSet<Regra> Regras { get; set; }
    }
}