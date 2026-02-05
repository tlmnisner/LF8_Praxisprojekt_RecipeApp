using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.EntityFrameworkCore;
using RecipeApp.ApiService.Data;
using RecipeApp.ApiService.Services;
using RecipeApp.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add Problem Detail Service to the container.
builder.Services.AddProblemDetails();

// Add Fastendpoints + Swagger
builder.Services.AddFastEndpoints();
builder.Services.SwaggerDocument();

// Add DbContext for Database access
builder.Services.AddDbContext<RecipeDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("recipeDb")));

// Register RecipeService for business logic
builder.Services.AddScoped<RecipeService>();

var app = builder.Build();

// Ensure database is created
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<RecipeDbContext>();
    dbContext.Database.EnsureCreated();
}
// Configure the HTTP request pipeline.
app.UseExceptionHandler();

app.UseFastEndpoints( config =>
{
    config.Endpoints.RoutePrefix = "api"; // All endpoints will be prefixed with /api
});

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerGen();
}


app.MapDefaultEndpoints();

app.Run();