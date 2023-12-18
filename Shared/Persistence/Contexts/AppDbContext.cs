using JuegoA_API.Juego_A.Domain.Models;
using JuegoA_API.Shared.Extensions;
using Microsoft.EntityFrameworkCore;

namespace JuegoA_API.Shared.Persistence.Contexts;

public class AppDbContext : DbContext
{
    public DbSet<Jugador> Jugadores { get; set; }
    public DbSet<Personaje> Personajes { get; set; }
    
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Jugador>().ToTable("Jugadores");
        builder.Entity<Jugador>().HasKey(p => p.Id);
        builder.Entity<Jugador>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Jugador>().Property(p => p.Usuario).IsRequired();
        builder.Entity<Jugador>().Property(p => p.Contrasenia).IsRequired();
        builder.Entity<Jugador>().Property(p => p.fotoPerfil);

        builder.Entity<Personaje>().ToTable("Personajes");
        builder.Entity<Personaje>().HasKey(p => p.Id);
        builder.Entity<Personaje>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Personaje>().Property(p => p.Vida).IsRequired();
        builder.Entity<Personaje>().Property(p => p.Nivel).IsRequired();
        builder.Entity<Personaje>().Property(p => p.Nombre).IsRequired();
        builder.Entity<Personaje>().Property(p => p.Ataque).IsRequired();
        builder.Entity<Personaje>().Property(p => p.Experiencia).IsRequired();
        builder.Entity<Personaje>().Property(p => p.Imagen).IsRequired();
        
        // Relaciones

        builder.Entity<Jugador>()
            .HasMany(p => p.Personajes)
            .WithOne(p => p.Jugador)
            .HasForeignKey(p => p.JugadorId);
        
        // Aplicar Snake Case Naming Convention
 
        builder.UseSnakeCaseNamingConvention();
    }
}