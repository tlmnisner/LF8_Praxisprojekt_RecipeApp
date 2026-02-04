namespace RecipeApp.ApiService.Models;

public class RecipeDto
{
  public string Name { get; set; }
  public string? Description { get; set; }
  public List<Image> Images { get; set; }
  public List<Ingredient> Ingredients { get; set; }
  public List<Instruction> Instructions { get; set; }
  public int PrepTimeMinutes { get; set; }
  public int CookTimeMinutes { get; set; }
}