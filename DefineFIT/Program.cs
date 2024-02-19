using DefineFIT.Api.Middlewares;
using DefineFIT.Application.Services;
using DefineFIT.Application.Services.Interfaces;
using DefineFIT.Domain.Repositories;
using DefineFIT.Infra;
using DefineFIT.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Adicione os serviços ao container.
var IsDevelopment = Environment
                    .GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";

var connectionString = IsDevelopment ?
      builder.Configuration.GetConnectionString("DefaultConnection") :
      GetHerokuConnectionString();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenService, TokenService>();

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

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();

static string GetHerokuConnectionString() =>
    Environment.GetEnvironmentVariable("DATABASE_URL");
