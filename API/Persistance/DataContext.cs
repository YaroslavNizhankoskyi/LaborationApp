using API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Persistance
{
    public class DataContext : IdentityDbContext<User>
    {

        public DbSet<Company> Companys { get; set; }

        public DbSet<Feedback> Feedbacks { get; set; }

        public DbSet<UserCharacteristic> UserCharacteristics { get; set; }

        public DbSet<Factor> Factors { get; set; }

        public DbSet<Tip> Tips { get; set; }

        public DbSet<UserTip> UserTips { get; set; }


        public DataContext(DbContextOptions<DataContext> options)
       : base(options)
        {


        }

        protected override void OnModelCreating(ModelBuilder builder)
        {


            builder.Entity<Feedback>()
                .HasOne(c => c.User)
                .WithMany(c => c.Feedbacks)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Feedback>()
               .HasOne(c => c.Enterpreneur)
               .WithMany(c => c.EnterpreneurFeedbacks)
               .HasForeignKey(c => c.EnterpreneurId)
               .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<User>()
                .HasMany(c => c.UserTips)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Factor>()
                .HasMany(c => c.HealthTips)
                .WithOne(c => c.HealthFactor)
                .HasForeignKey(c => c.HealthFactorId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Factor>()
                .HasMany(c => c.MentalTips)
                .WithOne(c => c.MentalFactor)
                .HasForeignKey(c => c.MentalFactorId)
                .OnDelete(DeleteBehavior.NoAction);


            builder.Entity<Factor>()
                .HasMany(c => c.SleepTips)
                .WithOne(c => c.SleepFactor)
                .HasForeignKey(c => c.SleepFactorId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Factor>()
                .HasMany(c => c.LaborTips)
                .WithOne(c => c.LaborFactor)
                .HasForeignKey(c => c.LaborFactorId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Company>()
                .HasMany(c => c.Workers)
                .WithOne(c => c.WorkerCompany)
                .HasForeignKey(c => c.CompanyId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Company>()
               .HasOne(c => c.Enterpreneur)
               .WithOne(c => c.EnterpreneurCompany)
               .OnDelete(DeleteBehavior.SetNull);

            


            builder.Entity<User>()
                .HasMany(c => c.UserCharacteristics)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.SetNull);
            

            builder.Entity<Tip>()
                .HasMany(c => c.UserTips)
                .WithOne(c => c.Tip)
                .HasForeignKey(c => c.TipId)
                .OnDelete(DeleteBehavior.Cascade);

               base.OnModelCreating(builder);

        }
    }
}
