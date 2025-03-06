using Microsoft.EntityFrameworkCore;
using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.DB
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        { }
        public virtual DbSet<Account> Accounts => Set<Account>();
        public virtual DbSet<Blog> Blogs => Set<Blog>();
        public virtual DbSet<Booking> Bookings => Set<Booking>();
        public virtual DbSet<Customer> Customers => Set<Customer>();
        public virtual DbSet<Evaluation> Evaluations => Set<Evaluation>();
        public virtual DbSet<Feedback> Feedbacks => Set<Feedback>();
        public virtual DbSet<Payment> Payments => Set<Payment>();
        public virtual DbSet<Rating> Ratings => Set<Rating>();
        public virtual DbSet<Service> Services => Set<Service>();
        public virtual DbSet<ServiceType> ServiceTypes => Set<ServiceType>();
        public virtual DbSet<Slot> Slots => Set<Slot>();
        public virtual DbSet<Therapist> Therapists => Set<Therapist>();
        public virtual DbSet<TherapyResult> TherapyResults => Set<TherapyResult>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Therapist)
                .WithMany(t => t.Bookings)
                .HasForeignKey(b => b.theraID)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Payment>()
               .HasOne(p => p.Customer)
               .WithMany(c => c.Payment)
               .HasForeignKey(p => p.cusID)
               .OnDelete(DeleteBehavior.Restrict);
            // Configure other relationships as needed.

            base.OnModelCreating(modelBuilder);
        }
    }
}
