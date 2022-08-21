using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.models;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        //Models
        public DbSet<Cliente> Cliente { get; set; }
    }
}