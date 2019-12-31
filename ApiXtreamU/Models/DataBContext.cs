using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiXtream.Models
{
    public class DataBContext : DbContext
    {
        public DataBContext(DbContextOptions<DataBContext> options) : base(options) { }

        
        public DbSet<User> User { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<User>().ToTable("User");
            
        }
    }
}
