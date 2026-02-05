var builder = DistributedApplication.CreateBuilder(args);

var sql = builder.AddSqlServer("sql")
    .WithDataVolume()
    .AddDatabase("recipeDb");

var apiService = builder.AddProject<Projects.RecipeApp_ApiService>("apiservice")
    .WithHttpHealthCheck("/health")
    .WithReference(sql)
    .WaitFor(sql);

var web  = builder.AddProject<Projects.RecipeApp_Web>("web")
    .WithHttpHealthCheck("/health")
    .WaitFor(apiService)
    .WithReference(apiService)
    ;

builder.Build().Run();