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
    public DbSet<Habilidad> Habilidades { get; set; }
    public DbSet<Objeto> Objetos { get; set; }
    public DbSet<Hechizo> Hechizos { get; set; }
    public DbSet<JugadorHechizo> JugadorHechizos { get; set; }
    public DbSet<HabilidadPersonaje> HabilidadPersonajes { get; set; }
    public DbSet<JugadorObjeto> JugadorObjetos { get; set; }
    
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

        builder.Entity<Habilidad>().ToTable("Habilidades");
        builder.Entity<Habilidad>().HasKey(p => p.Id);
        builder.Entity<Habilidad>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Habilidad>().Property(p => p.Nombre).IsRequired();
        builder.Entity<Habilidad>().Property(p => p.Multiplicador).IsRequired();
        builder.Entity<Habilidad>().Property(p => p.RutaAudio).IsRequired();

        builder.Entity<Objeto>().ToTable("Objetos");
        builder.Entity<Objeto>().HasKey(p => p.Id);
        builder.Entity<Objeto>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Objeto>().Property(p => p.Nombre).IsRequired();
        builder.Entity<Objeto>().Property(p => p.Descripcion).IsRequired();
        builder.Entity<Objeto>().Property(p => p.Imagen).IsRequired();

        builder.Entity<Hechizo>().ToTable("Hechizos");
        builder.Entity<Hechizo>().HasKey(p => p.Id);
        builder.Entity<Hechizo>().Property(p=>p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Hechizo>().Property(p=>p.Nombre).IsRequired();
        builder.Entity<Hechizo>().Property(p=>p.Descripcion).IsRequired();
        builder.Entity<Hechizo>().Property(p=>p.Cooldown).IsRequired();

        builder.Entity<JugadorHechizo>().ToTable("JugadorHechizo");
        builder.Entity<JugadorHechizo>().HasKey(p => new {p.JugadorId, p.HechizoId});
        builder.Entity<JugadorHechizo>().Property(p => p.JugadorId).IsRequired();
        builder.Entity<JugadorHechizo>().Property(p => p.HechizoId).IsRequired();

        builder.Entity<HabilidadPersonaje>().ToTable("HabilidadPersonaje");
        builder.Entity<HabilidadPersonaje>().HasKey(p => new {p.HabilidadId, p.PersonajeId});
        builder.Entity<HabilidadPersonaje>().Property(p => p.HabilidadId).IsRequired();
        builder.Entity<HabilidadPersonaje>().Property(p => p.PersonajeId).IsRequired();

        builder.Entity<JugadorObjeto>().ToTable("JugadorObjeto");
        builder.Entity<JugadorObjeto>().HasKey(p => new { p.JugadorId, p.ObjetoId });
        builder.Entity<JugadorObjeto>().Property(p => p.JugadorId).IsRequired();
        builder.Entity<JugadorObjeto>().Property(p => p.ObjetoId).IsRequired();
        builder.Entity<JugadorObjeto>().Property(p => p.Cantidad).IsRequired();
        
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
        
        // Relacion entre jugador y hechizo (uno a muchos)
        builder.Entity<JugadorHechizo>()
            .HasOne(jh => jh.Jugador)
            .WithMany(j => j.Hechizos)
            .HasForeignKey(jh => jh.JugadorId).IsRequired(false);

        builder.Entity<JugadorHechizo>()
            .HasOne(jh => jh.Hechizo)
            .WithMany(h => h.Jugadores)
            .HasForeignKey(jh => jh.HechizoId).IsRequired(false);
        
        // Relacion entre personaje y habilidad (muchos a muchos)
        builder.Entity<HabilidadPersonaje>()
            .HasOne(hp => hp.Habilidad)
            .WithMany(h => h.Personajes)
            .HasForeignKey(hp => hp.HabilidadId).IsRequired(false);

        builder.Entity<HabilidadPersonaje>()
            .HasOne(hp => hp.Personaje)
            .WithMany(p => p.Habilidades)
            .HasForeignKey(hp => hp.PersonajeId).IsRequired(false);
        
        // Relacion entre jugador y objeto (muchos a muchos)
        builder.Entity<JugadorObjeto>()
            .HasOne(jo => jo.Objeto)
            .WithMany(o => o.Jugadores)
            .HasForeignKey(jo => jo.ObjetoId).IsRequired(false);
        
        builder.Entity<JugadorObjeto>()
            .HasOne(jo => jo.Jugador)
            .WithMany(j => j.Objetos)
            .HasForeignKey(jo => jo.JugadorId).IsRequired(false);
        
        // Agregar datos por defecto a la base de datos
        builder.Entity<Personaje>().HasData(
             new Personaje {Id = 1, Vida = 1000, Nivel = 1, Nombre = "Boss 1", Ataque = 100, Experiencia = 0, Imagen = "../../src/assets/images/bosses/boss1.png"},
             new Personaje {Id = 2, Vida = 1500, Nivel = 2, Nombre = "Boss 2", Ataque = 150, Experiencia = 0, Imagen = "../../src/assets/images/bosses/boss2.png"},
             new Personaje {Id = 3, Vida = 2250, Nivel = 3, Nombre = "Boss 3", Ataque = 225, Experiencia = 0, Imagen = "../../src/assets/images/bosses/boss3.png"},
             new Personaje {Id = 4, Vida = 3375, Nivel = 4, Nombre = "Boss 4", Ataque = 338, Experiencia = 0, Imagen = "../../src/assets/images/bosses/boss4.png"},
             new Personaje {Id = 5, Vida = 5063, Nivel = 5, Nombre = "Boss 5", Ataque = 506, Experiencia = 0, Imagen = "../../src/assets/images/bosses/boss5.png"}
        );

        builder.Entity<Habilidad>().HasData(
            new Habilidad { Id = 1, Multiplicador = 0.1, Nombre = "Patada nuclear", RutaAudio = "../../src/assets/audios/HbtSFX/patadaNuclear.mp3"},
            new Habilidad { Id = 2, Multiplicador = 0.2, Nombre = "Patada del tigre", RutaAudio = "../../src/assets/audios/HbtSFX/patadaDelTigre.mp3"},
            new Habilidad { Id = 3, Multiplicador = 0.3, Nombre = "Plaka", RutaAudio = "../../src/assets/audios/HbtSFX/plaka.mp3"},
            new Habilidad { Id = 4, Multiplicador = 0.4, Nombre = "En la 100 o en la 101", RutaAudio = "../../src/assets/audios/HbtSFX/enLa100OEnLa101.mp3"},
            new Habilidad { Id = 5, Multiplicador = 0.5, Nombre = "Bichito de luz", RutaAudio = "../../src/assets/audios/HbtSFX/bichitoDeLuz.mp3"},
            new Habilidad { Id = 6, Multiplicador = 0.6, Nombre = "EPAAAAAA", RutaAudio = "../../src/assets/audios/HbtSFX/epa.mp3"},
            new Habilidad { Id = 7, Multiplicador = 0.7, Nombre = "Elden ring", RutaAudio = "../../src/assets/audios/HbtSFX/eldenRing.mp3"},
            new Habilidad { Id = 8, Multiplicador = 0.8, Nombre = "Desayuna con huevo", RutaAudio = "../../src/assets/audios/HbtSFX/desayunaConHuevo.mp3"},
            new Habilidad { Id = 9, Multiplicador = 0.9, Nombre = "Hola Dabo soy chileno", RutaAudio = "../../src/assets/audios/HbtSFX/holaDaboSoyChileno.mp3"},
            new Habilidad { Id = 10, Multiplicador = 1, Nombre = "Juan", RutaAudio = "../../src/assets/audios/HbtSFX/juan.mp3"},
            new Habilidad { Id = 11, Multiplicador = 1.1, Nombre = "Voy a mostrar las tetas", RutaAudio = "../../src/assets/audios/HbtSFX/voyAMostrarLasTetas.mp3"},
            new Habilidad { Id = 12, Multiplicador = 1.2, Nombre = "Coca Cola espuma", RutaAudio = "../../src/assets/audios/HbtSFX/cocaColaEspuma.mp3"},
            new Habilidad { Id = 13, Multiplicador = 1.3, Nombre = "EXPLOSIOOOOOOON", RutaAudio = "../../src/assets/audios/HbtSFX/explosion.mp3"},
            new Habilidad { Id = 14, Multiplicador = 1.4, Nombre = "Que toda su familia pille covid", RutaAudio = "../../src/assets/audios/HbtSFX/queTodaSuFamiliaPilleCovid.mp3"},
            new Habilidad { Id = 15, Multiplicador = 1.5, Nombre = "Coquerooo", RutaAudio = "../../src/assets/audios/HbtSFX/coquero.mp3"},
            new Habilidad { Id = 16, Multiplicador = 1.6, Nombre = "La tocó", RutaAudio = "../../src/assets/audios/HbtSFX/laToco.mp3"},
            new Habilidad { Id = 17, Multiplicador = 1.7, Nombre = "Ankara Messi", RutaAudio = "../../src/assets/audios/HbtSFX/ankaraMessi.mp3"},
            new Habilidad { Id = 18, Multiplicador = 1.8, Nombre = "Bing Chilling", RutaAudio = "../../src/assets/audios/HbtSFX/bingChilling.mp3"},
            new Habilidad { Id = 19, Multiplicador = 1.9, Nombre = "Wenomechainsama", RutaAudio = "../../src/assets/audios/HbtSFX/wenomechainsama.mp3"},
            new Habilidad { Id = 20, Multiplicador = 2, Nombre = "Metal pipe falling", RutaAudio = "../../src/assets/audios/HbtSFX/metalPipeFalling.mp3"},
            new Habilidad { Id = 21, Multiplicador = 1, Nombre = "Ok I pull up", RutaAudio = "../../src/assets/audios/HbtSFX/okIPullUp.mp3"},
            new Habilidad { Id = 22, Multiplicador = 1, Nombre = "Moai sound", RutaAudio = "../../src/assets/audios/HbtSFX/moaiSound.mp3"},
            new Habilidad { Id = 23, Multiplicador = 1.5, Nombre = "Wtf is a kilometer", RutaAudio = "../../src/assets/audios/HbtSFX/wtfIsAKilometer.mp3"},
            new Habilidad { Id = 24, Multiplicador = 2, Nombre = "Hello im under the water", RutaAudio = "../../src/assets/audios/HbtSFX/helloImUnderTheWater.mp3"}
        );
        
        builder.Entity<Mundo>().HasData(
            new Mundo {Id = 1, Xp = 100, Estado = EstadoMundo.SININICIAR, ImagenFondo = "../../src/assets/images/backgrounds/bg1.jpg", SongId = 1, Nombre = "Mundo 1", Personaje_Id = 1},
            new Mundo {Id = 2, Xp = 100, Estado = EstadoMundo.SININICIAR, ImagenFondo = "../../src/assets/images/backgrounds/bg2.jpg", SongId = 2, Nombre = "Mundo 2", Personaje_Id = 2},
            new Mundo {Id = 3, Xp = 100, Estado = EstadoMundo.SININICIAR, ImagenFondo = "../../src/assets/images/backgrounds/bg3.png", SongId = 3, Nombre = "Mundo 3", Personaje_Id = 3},
            new Mundo {Id = 4, Xp = 100, Estado = EstadoMundo.SININICIAR, ImagenFondo = "../../src/assets/images/backgrounds/bg4.png", SongId = 4, Nombre = "Mundo 4", Personaje_Id = 4},
            new Mundo {Id = 5, Xp = 100, Estado = EstadoMundo.SININICIAR, ImagenFondo = "../../src/assets/images/backgrounds/bg5.jpeg", SongId = 5, Nombre = "Mundo 5", Personaje_Id = 5}
        );

        builder.Entity<Objeto>().HasData(
            new Objeto {Id = 1, Nombre = "Pocion de curacion S", Descripcion = "Sana heridas menores al instante. Sabor dulce y herbal.", Imagen = "../../src/assets/images/objects/shtpt.png"},
            new Objeto {Id = 2, Nombre = "Pocion de curacion M", Descripcion = "Cura heridas moderadas, restaura vitalidad. Sabor complejo con toques de frutas.", Imagen = "../../src/assets/images/objects/mhtpt.png"},
            new Objeto {Id = 3, Nombre = "Pocion de curacion L", Descripcion = "Sana heridas graves, restablece la salud. Sabor robusto con hierbas y especias.", Imagen = "../../src/assets/images/objects/lhtpt.png"},
            new Objeto {Id = 4, Nombre = "Pocion de ataque", Descripcion = "Al consumirla, otorga un aumento temporal de fuerza y destreza en combate. Su sabor es intenso, con un toque picante y eléctrico que energiza al bebedor.", Imagen = "../../src/assets/images/objects/atkpt.png"},
            new Objeto {Id = 5, Nombre = "Pocion de armadura", Descripcion = "Al beberla, crea un aura protectora alrededor del usuario, aumentando la resistencia contra ataques físicos y mágicos. Su sabor es fresco, con matices metálicos que sugieren fortaleza.", Imagen = "../../src/assets/images/objects/ampt.png"},
            new Objeto {Id = 6, Nombre = "Pocion de veneno", Descripcion = "Esta poción se utiliza para envenenar armas o trampas. Al contacto, causa daño gradual y debilitante al objetivo. ", Imagen = "../../src/assets/images/objects/pspt.png"}
        );

        builder.Entity<Hechizo>().HasData(
            new Hechizo {Id = 1, Nombre = "Silencio", Descripcion = "Envuelve al enemigo en silencio mágico, impidiéndole lanzar hechizos y atacar verbalmente durante 1 turno.", Cooldown = 3, Imagen = "../../src/assets/images/habilities/silence.png"},
            new Hechizo {Id = 2, Nombre = "Sacrificio", Descripcion = "Consume totalmente la vida de un personaje aliado para potenciar grandemente al personaje activo..", Cooldown = 5, Imagen = "../../src/assets/images/habilities/sacrifice.png"}
        );

        builder.Entity<HabilidadPersonaje>().HasData(
            new HabilidadPersonaje { HabilidadId = 1, PersonajeId = 1 },
            new HabilidadPersonaje { HabilidadId = 2, PersonajeId = 1 },
            new HabilidadPersonaje { HabilidadId = 3, PersonajeId = 1 },
            new HabilidadPersonaje { HabilidadId = 4, PersonajeId = 1 },
            new HabilidadPersonaje { HabilidadId = 5, PersonajeId = 2 },
            new HabilidadPersonaje { HabilidadId = 6, PersonajeId = 2 },
            new HabilidadPersonaje { HabilidadId = 7, PersonajeId = 2 },
            new HabilidadPersonaje { HabilidadId = 8, PersonajeId = 2 },
            new HabilidadPersonaje { HabilidadId = 9, PersonajeId = 3 },
            new HabilidadPersonaje { HabilidadId = 10, PersonajeId = 3 },
            new HabilidadPersonaje { HabilidadId = 11, PersonajeId = 3 },
            new HabilidadPersonaje { HabilidadId = 12, PersonajeId = 3 },
            new HabilidadPersonaje { HabilidadId = 13, PersonajeId = 4 },
            new HabilidadPersonaje { HabilidadId = 14, PersonajeId = 4 },
            new HabilidadPersonaje { HabilidadId = 15, PersonajeId = 4 },
            new HabilidadPersonaje { HabilidadId = 16, PersonajeId = 4 },
            new HabilidadPersonaje { HabilidadId = 17, PersonajeId = 5 },
            new HabilidadPersonaje { HabilidadId = 18, PersonajeId = 5 },
            new HabilidadPersonaje { HabilidadId = 19, PersonajeId = 5 },
            new HabilidadPersonaje { HabilidadId = 20, PersonajeId = 5 }
        );
        
        // Aplicar Snake Case Naming Convention
        builder.UseSnakeCaseNamingConvention();
    }
}