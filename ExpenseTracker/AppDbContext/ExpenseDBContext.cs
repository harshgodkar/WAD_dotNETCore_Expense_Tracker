using ExpenseTracker.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.AppDbContext
{
    public class ExpenseDBContext : IdentityDbContext
    {
        public ExpenseDBContext(DbContextOptions<ExpenseDBContext> options):base(options) { }
        public virtual DbSet<ExpenseReport> ExpenseReport { get; set; }

        public virtual DbSet<ExpenseCategory> ExpenseCategory { get; set; }

        public virtual DbSet<UserProfile> UserProfile { get; set; }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\harsh\\OneDrive\\Documents\\ExpenseDb.mdf;Integrated Security=True;Connect Timeout=30");
        //    }
        //}
    }
}
