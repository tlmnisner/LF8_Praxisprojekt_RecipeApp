using Microsoft.EntityFrameworkCore;
using RecipeApp.ApiService.Models;

namespace RecipeApp.ApiService.Data;

public class RecipeDbContext : DbContext
{
    public RecipeDbContext(DbContextOptions<RecipeDbContext> options) : base(options)
    {
    }

    public DbSet<Recipe> Recipes { get; set; }
    
    //Relationship configuration
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure Recipe -> Images relationship
        modelBuilder.Entity<Recipe>()
            .OwnsMany(r => r.Images);

        // Configure Recipe -> Ingredients relationship
        modelBuilder.Entity<Recipe>()
            .OwnsMany(r => r.Ingredients, ingredient =>
            {
                ingredient.Property(i => i.Quantity).HasPrecision(18, 2); // Precision must be explicitly set
            });

        // Configure Recipe -> Instructions relationship
        modelBuilder.Entity<Recipe>()
            .OwnsMany(r => r.Instructions);
    }
}