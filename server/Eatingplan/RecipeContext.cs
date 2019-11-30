using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Eatingplan.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Internal;

namespace Eatingplan
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class RecipeContext : DbContext, IRecipeContext

    {
        public RecipeContext(DbContextOptions<RecipeContext> options) : base(options)
        {
            
        }

        public DbSet<Recipe> Recipes { get; set; }
        
        EntityEntry<Recipe> IRecipeContext.Add(Recipe item)
        {
            return Recipes.Add(item);
        }

        ValueTask<EntityEntry<Recipe>> IRecipeContext.AddAsync(Recipe item)
        {
            return Recipes.AddAsync(item);
        }

        bool IRecipeContext.Any()
        {
            return Recipes.Any();
        }

        ValueTask<Recipe> IRecipeContext.FindAsync(long id)
        {
            return Recipes.FindAsync(id);
        }

        EntityEntry<Recipe> IRecipeContext.Remove(Recipe item)
        {
            return Recipes.Remove(item);
        }

        int IRecipeContext.SaveChanges()
        {
            return SaveChanges();
        }

        Task<int> IRecipeContext.SaveChangesAsync(CancellationToken cancellationToken)
        {
            return SaveChangesAsync(cancellationToken);
        }

        Task<List<Recipe>> IRecipeContext.ToListAsync()
        {
            return Recipes.ToListAsync();
        }

        EntityEntry<Recipe> IRecipeContext.Update(Recipe item)
        {
            return Recipes.Update(item);
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}