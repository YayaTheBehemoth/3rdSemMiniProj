using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

using shared.Models;
using Npgsql;
namespace DBContext{
//how to npgsql with EF???????????????


public class dbContext : DbContext
{
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
 
    //public string DbPath { get;}
    public dbContext(DbContextOptions<dbContext> options)
    :base (options)
    {
     
    }
  
 protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=ella.db.elephantsql.com;Database=qgcutkgu;Username=qgcutkgu;Password=DVV-YnGW4UxuLtH-sdDJvFsAa1fxigjF");
    }
}

