using Infrastructure;
using Infrastructure.Repositories;
using Application.Interfaces;
using MediatR;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerGen;
using Application.Handlers.Author;
using ApplicationLayers.Handlers.Book;
using ApplicstionLayers.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Register services for dependency injection
builder.Services.AddDbContext<FakeDbContext>(options =>
    options.UseInMemoryDatabase("FakeDatabase")); // Use In-Memory DB

// Register repositories
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();

// Register FluentValidation and pipeline behaviors
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddValidatorsFromAssemblyContaining<CreateBookCommandValidator>();

// Register MediatR for CQRS
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
    typeof(CreateBookCommandHandler).Assembly,
    typeof(CreateAuthorCommandHandler).Assembly
));

// Add Controllers
builder.Services.AddControllers();

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "My API",
        Version = "v1"
    });
});

var app = builder.Build();

// Seed the database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<FakeDbContext>();
    FakeDbContext.Seed(context); // Ensure this method exists and is implemented correctly
}

// Enable middleware
app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});

app.MapControllers();
app.Run();
