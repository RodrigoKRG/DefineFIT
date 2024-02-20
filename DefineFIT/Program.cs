using DefineFIT.Api.Extensions;
using DefineFIT.Api.IoC;
using DefineFIT.Api.Middlewares;
using DefineFIT.Infra;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;

var IsDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";

var connectionString = IsDevelopment ?
      builder.Configuration.GetConnectionString("DefaultConnection") :
      GetHerokuConnectionString();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();
builder.Services.AddAuthentication(configuration);
builder.Services.RegisterServices();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();

// Aplicar migrações EF Core no início
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

static string GetHerokuConnectionString() =>
    Environment.GetEnvironmentVariable("DATABASE_URL");
