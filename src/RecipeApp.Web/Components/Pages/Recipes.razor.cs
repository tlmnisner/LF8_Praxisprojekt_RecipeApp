using Microsoft.AspNetCore.Components;
using RecipeApp.ApiService.Models;

namespace RecipeApp.Web.Components.Pages;

public partial class RecipesBase : ComponentBase
{
    [Inject]
    public required IHttpClientFactory HttpClientFactory { get; set; }

    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    public List<Recipe>? recipes;

    protected override async Task OnInitializedAsync()
    {
        var httpClient = HttpClientFactory.CreateClient("RecipeApiClient");
        recipes = await httpClient.GetFromJsonAsync<List<Recipe>>("api/recipe");
    }

    public void NavigateToRecipe(int recipeId)
    {
        NavigationManager.NavigateTo($"/recipe?id={recipeId}");
    }
}