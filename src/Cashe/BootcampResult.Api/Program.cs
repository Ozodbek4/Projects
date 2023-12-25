using BootcampResult.Api.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

await builder.ConfigureAsync();

var app = builder.Build();


// Configure the HTTP request pipeline.

await app.ConfigureAsync();

app.Run();