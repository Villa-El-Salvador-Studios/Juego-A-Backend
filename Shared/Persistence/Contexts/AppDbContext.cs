using JuegoA_API.Juego_A.Domain.Models;
using JuegoA_API.Shared.Extensions;
using Microsoft.EntityFrameworkCore;

namespace JuegoA_API.Shared.Persistence.Contexts;

public class AppDbContext : DbContext
{
    public DbSet<Jugador> Jugadores { get; set; }
    public DbSet<Personaje> Personajes { get; set; }
    public DbSet<Mundo> Mundos { get; set; }
    
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
        builder.Entity<Jugador>().Property(p => p.MundoMaximo);

        builder.Entity<Personaje>().ToTable("Personajes");
        builder.Entity<Personaje>().HasKey(p => p.Id);
        builder.Entity<Personaje>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Personaje>().Property(p => p.Vida).IsRequired();
        builder.Entity<Personaje>().Property(p => p.Nivel).IsRequired();
        builder.Entity<Personaje>().Property(p => p.Nombre).IsRequired();
        builder.Entity<Personaje>().Property(p => p.Ataque).IsRequired();
        builder.Entity<Personaje>().Property(p => p.Experiencia).IsRequired();
        builder.Entity<Personaje>().Property(p => p.Imagen).IsRequired();

        builder.Entity<Mundo>().ToTable("Mundos");
        builder.Entity<Mundo>().HasKey(p => p.Id);
        builder.Entity<Mundo>().Property(p=>p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Mundo>().Property(p=>p.Xp).IsRequired();
        builder.Entity<Mundo>().Property(p=>p.Estado).IsRequired();
        
        // Relaciones
        // Relacion entre jugador y personaje (uno a muchos)
        builder.Entity<Jugador>()
            .HasMany(p => p.Personajes)
            .WithOne(p => p.Jugador)
            .HasForeignKey(p => p.JugadorId).IsRequired(false);

        // Relacion entre mundo y jugador (uno a uno)
        builder.Entity<Mundo>()
            .HasOne(m => m.Jugador)
            .WithOne(j => j.Mundo)
            .HasForeignKey<Jugador>(j => j.MundoId).IsRequired(false);

        // Relacion entre personaje y mundo (uno a uno)
        builder.Entity<Personaje>()
            .HasOne(p => p.Mundo)
            .WithOne(m => m.Personaje)
            .HasForeignKey<Mundo>(m => m.Personaje_Id);
        
        // Aplicar Snake Case Naming Convention
 
        builder.UseSnakeCaseNamingConvention();
    }
}