using FastEndpoints;
using RecipeApp.ApiService.Models;
using RecipeApp.ApiService.Services;

namespace RecipeApp.ApiService.Endpoints;

public class CreateRecipeEndpoint : Endpoint<RecipeDto>
{
    private readonly RecipeService _recipeService;

    public CreateRecipeEndpoint(RecipeService recipeService)
    {
        _recipeService = recipeService;
    }        

    public override void Configure()
    {
        Post("/recipe");
        AllowAnonymous();
        
        Summary(s =>
        {
            s.Summary = "Create a new recipe";
            s.Description = "Creates a new recipe with the provided details.";
            s.ExampleRequest = new RecipeDto
            {
                Name = "Test Recipe",
                Description = "A test recipe",
                Images = new List<Image>
                {
                    new Image
                    {
                        FileName = "test.jpg",
                        ContentType = "image/jpeg",
                        Data = Convert.FromBase64String("iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAYAAAAfFcSJAAAADUlEQVR42mNk+M9QDwADhgGAWjR9awAAAABJRU5ErkJggg==")
                    }
                },
                Ingredients = new List<Ingredient>
                {
                    new Ingredient
                    {
                        Quantity = 2.50m,
                        Measurand = "cups",
                        Name = "flour"
                    }
                },
                Instructions = new List<Instruction>
                {
                    new Instruction
                    {
                        Step = 1,
                        Description = "Mix ingredients"
                    }
                },
                PrepTimeMinutes = 10,
                CookTimeMinutes = 30
            };
        });
    }

    public override async Task HandleAsync(RecipeDto recipeDto,CancellationToken ct)
    {
        var recipe = Recipe.FromDto(recipeDto);
        await _recipeService.AddRecipeAsync(recipe);
        await Send.OkAsync();
    }
}