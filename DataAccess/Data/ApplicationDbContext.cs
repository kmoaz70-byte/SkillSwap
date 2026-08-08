using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<UserSkill> UserSkills { get; set; }
        public DbSet<SwapRequest> SwapRequests { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            base.OnModelCreating(modelbuilder);

            modelbuilder.Entity<ApplicationUser>(entity =>
            {
                entity.Property(u => u.FullName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(u => u.Bio)
                    .HasMaxLength(500);

                entity.Property(u => u.Location)
                    .HasMaxLength(100);

                entity.Property(u => u.ProfilePicture)
                    .HasMaxLength(300);

                entity.Property(u => u.CreatedAt)
                    .IsRequired();

                entity.Property(u => u.IsActive)
                    .IsRequired();
            });

            // Fix multiple cascade paths for SwapRequest -> Skills
            modelbuilder.Entity<SwapRequest>()
    .HasOne(s => s.Sender)
    .WithMany()
    .HasForeignKey(s => s.SenderId)
    .OnDelete(DeleteBehavior.Restrict);

            modelbuilder.Entity<SwapRequest>()
                .HasOne(s => s.Receiver)
                .WithMany()
                .HasForeignKey(s => s.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            modelbuilder.Entity<SwapRequest>()
                .HasOne(s => s.SenderSkill)
                .WithMany()
                .HasForeignKey(s => s.SenderSkillId)
                .OnDelete(DeleteBehavior.Restrict);

            modelbuilder.Entity<SwapRequest>()
                .HasOne(s => s.ReceiverSkill)
                .WithMany()
                .HasForeignKey(s => s.ReceiverSkillId)
                .OnDelete(DeleteBehavior.Restrict);

            // Rating relationships
            modelbuilder.Entity<Rating>()
                .HasOne(r => r.Rater)
                .WithMany(u => u.GivenRatings)
                .HasForeignKey(r => r.RaterId)
                .OnDelete(DeleteBehavior.Restrict);
            modelbuilder.Entity<Rating>()
    .HasOne(r => r.RatedUser)
    .WithMany()
    .HasForeignKey(r => r.RatedUserId)
    .OnDelete(DeleteBehavior.Restrict);

            modelbuilder.Entity<Rating>()
                .HasOne(r => r.SwapRequest)
                .WithMany()
                .HasForeignKey(r => r.SwapRequestId)
                .OnDelete(DeleteBehavior.Cascade);

            // Message relationships
            modelbuilder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany(u => u.SentMessages)
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelbuilder.Entity<Message>()
                .HasOne(m => m.Receiver)
                .WithMany(u => u.ReceivedMessages)
                .HasForeignKey(m => m.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            // UserSkill relationships
            modelbuilder.Entity<UserSkill>()
                .HasOne(us => us.User)
                .WithMany(u => u.UserSkills)
                .HasForeignKey(us => us.UserId);

            modelbuilder.Entity<UserSkill>()
                .HasOne(us => us.Skill)
                .WithMany(s => s.UserSkills)
                .HasForeignKey(us => us.SkillId);

            // Skill relationships
            modelbuilder.Entity<Skill>()
                .HasOne(s => s.SuggestedByUser)
                .WithMany()
                .HasForeignKey(s => s.SuggestedByUserId)
                .OnDelete(DeleteBehavior.SetNull);
            modelbuilder.Entity<Notification>()
    .HasOne(n => n.User)
    .WithMany()
    .HasForeignKey(n => n.UserId)
    .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
