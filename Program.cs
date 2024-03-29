using JuegoA_API.Juego_A.Domain.Repositories;
using JuegoA_API.Juego_A.Domain.Services;
using JuegoA_API.Juego_A.Mapping;
using JuegoA_API.Juego_A.Persistence.Repositories;
using JuegoA_API.Juego_A.Services;
using JuegoA_API.Shared.Persistence.Contexts;
using JuegoA_API.Shared.Persistence.Respositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Add CORS
builder.Services.AddCors();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Database Connection
var connectionString = 
    builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(
    options => options.UseNpgsql(connectionString)
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors());

// Add lowercase routes
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Dependency Injection Configuration
builder.Services.AddScoped<IJugadorRepository, JugadorRepository>();
builder.Services.AddScoped<IJugadorService, JugadorService>();
builder.Services.AddScoped<IPersonajeRepository, PersonajeRepository>();
builder.Services.AddScoped<IPersonajeService, PersonajeService>();
builder.Services.AddScoped<IMundoRepository, MundoRepository>();
builder.Services.AddScoped<IMundoService, MundoService>();
builder.Services.AddScoped<IHabilidadService, HabilidadService>();
builder.Services.AddScoped<IHabilidadRepository, HabilidadRepository>();
builder.Services.AddScoped<IObjetoService, ObjetoService>();
builder.Services.AddScoped<IObjetoRepository, ObjetoRepository>();
builder.Services.AddScoped<IHechizoService, HechizoService>();
builder.Services.AddScoped<IHechizoRepository, HechizoRepository>();
builder.Services.AddScoped<IJugadorHechizoService, JugadorHechizoService>();
builder.Services.AddScoped<IJugadorHechizoRepository, JugadorHechizoRepository>();
builder.Services.AddScoped<IHabilidadPersonajeRepository, HabilidadPersonajeRepository>();
builder.Services.AddScoped<IHabilidadPersonajeService, HabilidadPersonajeService>();
builder.Services.AddScoped<IJugadorObjetoRepository, JugadorObjetoRepository>();
builder.Services.AddScoped<IJugadorObjetoService, JugadorObjetoService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
// AutoMapper Configuration
builder.Services.AddAutoMapper(
    typeof(ModelToResourceProfile), 
    typeof(ResourceToModelProfile));

var app = builder.Build();

// Validation for ensuring Database Objects are created
using (var scope = app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetService<AppDbContext>())
{
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure CORS
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();