using FastEndpoints;
using RecipeApp.ApiService.Models;
using RecipeApp.ApiService.Services;

public class CreateRecipeEndpoint : Endpoint<GetRecipeRequest>
{
    private readonly RecipeService _recipeService;

    public CreateRecipeEndpoint(RecipeService recipeService)
    {
        _recipeService = recipeService;
    }        

    public override void Configure()
    {
        Get("/recipe");
        AllowAnonymous();
        
        Summary(s =>
        {
            s.Summary = "Grab recipe(s)";
            s.Description = "Gets all recipes or a specific recipe by ID.";
            s.ExampleRequest = new GetRecipeRequest
            {
                Id = 1
            };
        });
    }
    
    
    public override async Task HandleAsync(GetRecipeRequest request, CancellationToken ct)
    {
        if (request.Id.HasValue)
        {
            Recipe? recipe = await _recipeService.GetRecipeByIdAsync(request.Id.Value);
            await Send.OkAsync(recipe, ct);
        }
        else
        {
            List<Recipe> recipes = await _recipeService.GetAllRecipesAsync();
            await Send.OkAsync(recipes, ct);
        }

    }
}

public class GetRecipeRequest
{
    public int? Id { get; set; }
}