using Contact.Domain.Entities;
using Contact.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Infrastructure.Data
{
    /// <summary>
    /// ApplicationDbContext uses EF Core
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : base(dbContextOptions) { }

        public DbSet<User> Users { get; set; }
        public DbSet<UserContact> UserContacts { get; set; }
        public DbSet<UserOTP> OTPs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserContactConfiguration());
            modelBuilder.ApplyConfiguration(new UserOTPConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
