using Microsoft.EntityFrameworkCore;

namespace RecipeApp.ApiService.Models;

public class Recipe
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public List<Image> Images { get; set; }
    public List<Ingredient> Ingredients { get; set; }
    public List<Instruction> Instructions { get; set; }
    public int PrepTimeMinutes { get; set; }
    public int CookTimeMinutes { get; set; }
    
    //Constructor for Recipe from RecipeDto
    public static Recipe FromDto(RecipeDto dto) => new()
    {
        Name = dto.Name,
        Description = dto.Description,
        Images = dto.Images,
        Ingredients = dto.Ingredients,
        Instructions = dto.Instructions,
        PrepTimeMinutes = dto.PrepTimeMinutes,
        CookTimeMinutes = dto.CookTimeMinutes
    };
}

public class Ingredient
{
    public decimal Quantity { get; set; }
    public string Measurand { get; set; }
    public string Name { get; set; }
}

public class Instruction
{
    public int Step { get; set; }
    public string Description { get; set; } 
}

public class Image
{
    public string FileName { get; set; }
    public string ContentType { get; set; }
    public byte[] Data { get; set; }
}




