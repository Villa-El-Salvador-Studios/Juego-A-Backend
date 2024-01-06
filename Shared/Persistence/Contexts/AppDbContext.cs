using JuegoA_API.Juego_A.Domain.Models;
using JuegoA_API.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Macs;

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
        builder.Entity<Mundo>().Property(p => p.ImagenFondo).IsRequired();
        builder.Entity<Mundo>().Property(p => p.SongId).IsRequired();
        builder.Entity<Mundo>().Property(p => p.Nombre).IsRequired();
        
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
        
        // Agregar datos por defecto a la base de datos
        builder.Entity<Personaje>().HasData(
            new Personaje {Id = 1, Vida = 1000, Nivel = 1, Nombre = "Boss 1", Ataque = 100, Experiencia = 0, Imagen = "../../src/assets/images/boss1.png"},
            new Personaje {Id = 2, Vida = 1500, Nivel = 2, Nombre = "Boss 2", Ataque = 150, Experiencia = 0, Imagen = "../../src/assets/images/boss2.jpg"},
            new Personaje {Id = 3, Vida = 2250, Nivel = 3, Nombre = "Boss 3", Ataque = 225, Experiencia = 0, Imagen = "../../src/assets/images/boss3.png"},
            new Personaje {Id = 4, Vida = 3375, Nivel = 4, Nombre = "Boss 4", Ataque = 338, Experiencia = 0, Imagen = "../../src/assets/images/boss4.png"},
            new Personaje {Id = 5, Vida = 5063, Nivel = 5, Nombre = "Boss 5", Ataque = 506, Experiencia = 0, Imagen = "../../src/assets/images/boss5.jpeg"}
        );

        builder.Entity<Mundo>().HasData(
            new Mundo {Id = 1, Xp = 100, Estado = EstadoMundo.SININICIAR, ImagenFondo = "../../src/assets/images/bg1.jpg", SongId = 1, Nombre = "Mundo 1", Personaje_Id = 1},
            new Mundo {Id = 2, Xp = 100, Estado = EstadoMundo.SININICIAR, ImagenFondo = "../../src/assets/images/bg2.jpg", SongId = 2, Nombre = "Mundo 2", Personaje_Id = 2},
            new Mundo {Id = 3, Xp = 100, Estado = EstadoMundo.SININICIAR, ImagenFondo = "../../src/assets/images/bg3.png", SongId = 3, Nombre = "Mundo 3", Personaje_Id = 3},
            new Mundo {Id = 4, Xp = 100, Estado = EstadoMundo.SININICIAR, ImagenFondo = "../../src/assets/images/bg4.png", SongId = 4, Nombre = "Mundo 4", Personaje_Id = 4},
            new Mundo {Id = 5, Xp = 100, Estado = EstadoMundo.SININICIAR, ImagenFondo = "../../src/assets/images/bg5.jpeg", SongId = 5, Nombre = "Mundo 5", Personaje_Id = 5}
        );
        
        // Aplicar Snake Case Naming Convention
 
        builder.UseSnakeCaseNamingConvention();
    }
}