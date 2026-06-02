using Microsoft.EntityFrameworkCore;
using PersonagemModels;

namespace Data;

public class PersonagemContext : DbContext
{
    public DbSet<Personagem> Personagens => Set<Personagem>();

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=personagens.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Personagem>().OwnsOne(p => p.classe);
        modelBuilder.Entity<Personagem>().OwnsOne(p => p.atributos);
    }
}
