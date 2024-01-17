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
    public DbSet<Habilidades> Habilidades { get; set; }
    public DbSet<Objeto> Objetos { get; set; }
    public DbSet<Hechizo> Hechizos { get; set; }
    public DbSet<JugadorHechizo> JugadorHechizos { get; set; }
    
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

        builder.Entity<Habilidades>().ToTable("Habilidades");
        builder.Entity<Habilidades>().HasKey(p => p.Id);
        builder.Entity<Habilidades>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Habilidades>().Property(p => p.Habilidad1).IsRequired();
        builder.Entity<Habilidades>().Property(p => p.Habilidad2).IsRequired();
        builder.Entity<Habilidades>().Property(p => p.Habilidad3).IsRequired();
        builder.Entity<Habilidades>().Property(p => p.Habilidad4).IsRequired();

        builder.Entity<Objeto>().ToTable("Objetos");
        builder.Entity<Objeto>().HasKey(p => p.Id);
        builder.Entity<Objeto>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Objeto>().Property(p => p.Nombre).IsRequired();
        builder.Entity<Objeto>().Property(p => p.Descripcion).IsRequired();
        builder.Entity<Objeto>().Property(p => p.Cantidad).IsRequired();
        builder.Entity<Objeto>().Property(p => p.Imagen).IsRequired();

        builder.Entity<Hechizo>().ToTable("Hechizos");
        builder.Entity<Hechizo>().HasKey(p => p.Id);
        builder.Entity<Hechizo>().Property(p=>p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Hechizo>().Property(p=>p.Nombre).IsRequired();
        builder.Entity<Hechizo>().Property(p=>p.Descripcion).IsRequired();
        builder.Entity<Hechizo>().Property(p=>p.Cooldown).IsRequired();

        builder.Entity<JugadorHechizo>().ToTable("JugadorHechizo");
        builder.Entity<JugadorHechizo>().HasKey(p => p.JugadorId);
        builder.Entity<JugadorHechizo>().HasKey(p => p.HechizoId);
        builder.Entity<JugadorHechizo>().Property(p => p.JugadorId).IsRequired();
        builder.Entity<JugadorHechizo>().Property(p => p.HechizoId).IsRequired();
        
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
        
        // Relacion entre personaje y habilidad (uno a uno)
        builder.Entity<Personaje>()
            .HasOne(p => p.Habilidades)
            .WithOne(h => h.Personaje)
            .HasForeignKey<Habilidades>(h => h.PersonajeId).IsRequired(false);
        
        // Relacion entre jugador y objeto (uno a muchos)
        builder.Entity<Jugador>()
            .HasMany(j => j.Objetos)
            .WithOne(o => o.Jugador)
            .HasForeignKey(o => o.jugadorId).IsRequired(false);
        
        // Relacion entre jugador y hechizo (uno a muchos)
        builder.Entity<JugadorHechizo>()
            .HasOne(jh => jh.Jugador)
            .WithMany(j => j.Hechizos)
            .HasForeignKey(jh => jh.JugadorId).IsRequired(false);

        builder.Entity<JugadorHechizo>()
            .HasOne(jh => jh.Hechizo)
            .WithMany(h => h.Jugadores)
            .HasForeignKey(jh => jh.HechizoId).IsRequired(false);
        
        // Agregar datos por defecto a la base de datos
        builder.Entity<Personaje>().HasData(
             new Personaje {Id = 1, Vida = 1000, Nivel = 1, Nombre = "Boss 1", Ataque = 100, Experiencia = 0, Imagen = "../../src/assets/images/bosses/boss1.png"},
             new Personaje {Id = 2, Vida = 1500, Nivel = 2, Nombre = "Boss 2", Ataque = 150, Experiencia = 0, Imagen = "../../src/assets/images/bosses/boss2.jpg"},
             new Personaje {Id = 3, Vida = 2250, Nivel = 3, Nombre = "Boss 3", Ataque = 225, Experiencia = 0, Imagen = "../../src/assets/images/bosses/boss3.png"},
             new Personaje {Id = 4, Vida = 3375, Nivel = 4, Nombre = "Boss 4", Ataque = 338, Experiencia = 0, Imagen = "../../src/assets/images/bosses/boss4.png"},
             new Personaje {Id = 5, Vida = 5063, Nivel = 5, Nombre = "Boss 5", Ataque = 506, Experiencia = 0, Imagen = "../../src/assets/images/bosses/boss5.jpeg"}
        );

        builder.Entity<Habilidades>().HasData(
            new Habilidades { Id = 1, Habilidad1 = "Patada nuclear", Habilidad2 = "Patada del tigre", Habilidad3 = "Plaka", Habilidad4 = "En la 100 o en la 101", PersonajeId = 1},
            new Habilidades { Id = 2, Habilidad1 = "Hola Dabo soy chileno", Habilidad2 = "Juan", Habilidad3 = "Voy a mostrar las tetas", Habilidad4 = "Coca Cola espuma", PersonajeId = 2},
            new Habilidades { Id = 3, Habilidad1 = "EXPLOSIOOOOOOON", Habilidad2 = "Que toda su familia pille covid", Habilidad3 = "Coquerooo", Habilidad4 = "La tocó", PersonajeId = 3},
            new Habilidades { Id = 4, Habilidad1 = "Ankara Messi", Habilidad2 = "Bing Chilling", Habilidad3 = "Wenomechainsama", Habilidad4 = "Metal pipe falling", PersonajeId = 4},
            new Habilidades { Id = 5, Habilidad1 = "Ok I pull up", Habilidad2 = "Moai sound", Habilidad3 = "Wtf is a kilometer", Habilidad4 = "Hello im under the water", PersonajeId = 5},
            new Habilidades { Id = 6, Habilidad1 = "Bichito de luz", Habilidad2 = "EPAAAAAA", Habilidad3 = "Enden ring", Habilidad4 = "Desayuna con huevo"}
        );
        
        builder.Entity<Mundo>().HasData(
            new Mundo {Id = 1, Xp = 100, Estado = EstadoMundo.SININICIAR, ImagenFondo = "../../src/assets/images/backgrounds/bg1.jpg", SongId = 1, Nombre = "Mundo 1", Personaje_Id = 1},
            new Mundo {Id = 2, Xp = 100, Estado = EstadoMundo.SININICIAR, ImagenFondo = "../../src/assets/images/backgrounds/bg2.jpg", SongId = 2, Nombre = "Mundo 2", Personaje_Id = 2},
            new Mundo {Id = 3, Xp = 100, Estado = EstadoMundo.SININICIAR, ImagenFondo = "../../src/assets/images/backgrounds/bg3.png", SongId = 3, Nombre = "Mundo 3", Personaje_Id = 3},
            new Mundo {Id = 4, Xp = 100, Estado = EstadoMundo.SININICIAR, ImagenFondo = "../../src/assets/images/backgrounds/bg4.png", SongId = 4, Nombre = "Mundo 4", Personaje_Id = 4},
            new Mundo {Id = 5, Xp = 100, Estado = EstadoMundo.SININICIAR, ImagenFondo = "../../src/assets/images/backgrounds/bg5.jpeg", SongId = 5, Nombre = "Mundo 5", Personaje_Id = 5}
        );

        builder.Entity<Objeto>().HasData(
            new Objeto {Id = 1, Nombre = "Pocion de curacion S", Descripcion = "Sana heridas menores al instante. Sabor dulce y herbal.", Cantidad = 0, Imagen = "../../src/assets/images/objects/shtpt.png", jugadorId = null},
            new Objeto {Id = 2, Nombre = "Pocion de curacion M", Descripcion = "Cura heridas moderadas, restaura vitalidad. Sabor complejo con toques de frutas.", Cantidad = 0, Imagen = "../../src/assets/images/objects/mhtpt.png", jugadorId = null},
            new Objeto {Id = 3, Nombre = "Pocion de curacion L", Descripcion = "Sana heridas graves, restablece la salud. Sabor robusto con hierbas y especias.", Cantidad = 0, Imagen = "../../src/assets/images/objects/lhtpt.png", jugadorId = null},
            new Objeto {Id = 4, Nombre = "Pocion de ataque", Descripcion = "Al consumirla, otorga un aumento temporal de fuerza y destreza en combate. Su sabor es intenso, con un toque picante y eléctrico que energiza al bebedor.", Cantidad = 0, Imagen = "../../src/assets/images/objects/atkpt.png", jugadorId = null},
            new Objeto {Id = 5, Nombre = "Pocion de armadura", Descripcion = "Al beberla, crea un aura protectora alrededor del usuario, aumentando la resistencia contra ataques físicos y mágicos. Su sabor es fresco, con matices metálicos que sugieren fortaleza.", Cantidad = 0, Imagen = "../../src/assets/images/objects/ampt.png", jugadorId = null},
            new Objeto {Id = 6, Nombre = "Pocion de veneno", Descripcion = "Esta poción se utiliza para envenenar armas o trampas. Al contacto, causa daño gradual y debilitante al objetivo. ", Cantidad = 0, Imagen = "../../src/assets/images/objects/pspt.png", jugadorId = null}
        );

        builder.Entity<Hechizo>().HasData(
            new Hechizo {Id = 1, Nombre = "Silencio", Descripcion = "Envuelve al enemigo en silencio mágico, impidiéndole lanzar hechizos y atacar verbalmente durante 1 turno.", Cooldown = 3, Imagen = "../../src/assets/images/habilities/silence.png"},
            new Hechizo {Id = 2, Nombre = "Sacrificio", Descripcion = "Consume totalmente la vida de un personaje aliado para potenciar grandemente al personaje activo..", Cooldown = 5, Imagen = "../../src/assets/images/habilities/sacrifice.png"}
        );
        
        // Aplicar Snake Case Naming Convention
        builder.UseSnakeCaseNamingConvention();
    }
}