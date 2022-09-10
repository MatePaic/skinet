using Core.Interfaces;
using Infrastructure;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<StoreContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Skinet"));
});
builder.Services.AddScoped<IProductRepository, ProductRepository>();

var app = builder.Build();

using var scope = app.Services.CreateScope();

var services = scope.ServiceProvider;

var loggerFactory = services.GetRequiredService<ILoggerFactory>();

try

{

    var context = services.GetRequiredService<StoreContext>();

    await context.Database.MigrateAsync();

    await StoreContextSeed.SeedAsync(context, loggerFactory);

    //var userManager = services.GetRequiredService<UserManager<AppUser>>();

    //var identityContext = services.GetRequiredService<AppIdentityDbContext>();

    //await identityContext.Database.MigrateAsync();

    //await AppIdentityDbContextSeed.SeedUsersAsync(userManager);

}

catch (Exception ex)

{

    var logger = loggerFactory.CreateLogger<Program>();

    logger.LogError(ex, "An error occurred during migration");

}



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();