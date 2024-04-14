using EFCore.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCore.WebAPI.Data;

public class HeroiContext: DbContext
{
    public DbSet<Heroi> Herois { get; set; }
    public DbSet<Batalha> Batalhas { get; set; }
    public DbSet<Arma> Armas { get; set; }
    public DbSet<IdentidadeSecreta> IdentidadesSecretas { get; set; }
    public DbSet<HeroiBatalha> HeroisBatalhas { get; set; }
   

    public HeroiContext(DbContextOptions<HeroiContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Heroi>().ToTable("Heroi");
        modelBuilder.Entity<Batalha>().ToTable("Batalha");
        modelBuilder.Entity<Arma>().ToTable("Arma");
        modelBuilder.Entity<IdentidadeSecreta>().ToTable("IdentidadeSecreta");
        modelBuilder.Entity<HeroiBatalha>().ToTable("HeroiBatalha");
 
        modelBuilder.Entity<HeroiBatalha>(entity => entity.HasKey(e => new { e.BatalhaId,e.HeroiId}));
    }




}
