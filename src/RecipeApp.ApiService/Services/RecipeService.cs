using Microsoft.EntityFrameworkCore;
using RecipeApp.ApiService.Data;
using RecipeApp.ApiService.Models;

namespace RecipeApp.ApiService.Services;

public class RecipeService
{
    //Setup DbContext
    private readonly RecipeDbContext _context;
    public RecipeService(RecipeDbContext context)
    {
        _context = context;
    }
    
    //Add Recipe to Db
    public async Task<Recipe> AddRecipeAsync(Recipe recipe)
    { 
        await _context.Recipes.AddAsync(recipe);
        await _context.SaveChangesAsync();
        return recipe;
    }
    
    //Get All Recipes from Db
    public async Task<List<Recipe>> GetAllRecipesAsync()
    {
        return await _context.Recipes
            .Include(r => r.Images)
            .Include(r => r.Ingredients)
            .Include(r => r.Instructions)
            .ToListAsync();
    }
    
    //Get Recipe by Id from Db
    public async Task<Recipe?> GetRecipeByIdAsync(int id)
    {
        return await _context.Recipes
            .Include(r => r.Images)
            .Include(r => r.Ingredients)
            .Include(r => r.Instructions)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

}