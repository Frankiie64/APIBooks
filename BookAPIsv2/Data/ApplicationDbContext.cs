using BooksAPIsv2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAPIsv2.Data
{
    public class ApplicationDbContext : DbContext 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        //Mapeo de modelos
        public DbSet<Category> Category { get; set; }
        public DbSet<Book> Book { get; set; }

    }
}
