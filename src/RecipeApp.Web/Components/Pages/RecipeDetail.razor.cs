using Microsoft.AspNetCore.Components;
using RecipeApp.ApiService.Models;

namespace RecipeApp.Web.Components.Pages;

public partial class RecipeDetailBase : ComponentBase
{
    [SupplyParameterFromQuery]
    public int Id { get; set; }

    [Inject]
    public required IHttpClientFactory HttpClientFactory { get; set; }

    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    public Recipe? recipe;

    protected override async Task OnInitializedAsync()
    {
        var httpClient = HttpClientFactory.CreateClient("RecipeApiClient");
        recipe = await httpClient.GetFromJsonAsync<Recipe>($"api/recipe?id={Id}");
    }

    public void GoBack()
    {
        NavigationManager.NavigateTo("/");
    }
}