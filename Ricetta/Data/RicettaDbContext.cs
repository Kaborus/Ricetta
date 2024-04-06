using Ricetta.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Identity.Client;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Ricetta.Data
{
    public class RicettaDbContext : IdentityDbContext<Member>
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Recipe>(
                r =>
                {
                    r.Property(r => r.Name).IsRequired().HasMaxLength(100);

                    r.HasMany(r => r.Ingredients).
                        WithOne(i => i.Recipe).
                        HasForeignKey(r => r.RecipeId);

                    r.HasMany(r => r.PreparationSteps).
                        WithOne(i => i.Recipe).
                        HasForeignKey(r => r.RecipeId);

                    r.HasOne(r => r.Category).
                        WithMany(c => c.Recipes).
                        HasForeignKey(r => r.CategoryId);
                });

            modelBuilder.Entity<Message>()
            .HasOne(m => m.Sender)
            .WithMany()
            .HasForeignKey(m => m.SenderId);

            modelBuilder.Entity<Inbox>()
                .HasOne(i => i.User)
                .WithMany()
                .HasForeignKey(i => i.UserId);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Inbox)
                .WithMany(i => i.Messages)
                .HasForeignKey(m => m.InboxId);
        }

        public RicettaDbContext(DbContextOptions<RicettaDbContext> options) : base(options)
        {

        }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<SavedRecipe> SavedRecipes { get; set; }
        //public DbSet<Member> Members { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<PreparationStep> PreparationSteps { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Inbox> Inboxes { get; set; }
        public DbSet<Notification> Notifications { get; set; }
    }
}
