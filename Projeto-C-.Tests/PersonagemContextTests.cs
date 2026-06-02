using System;
using System.IO;
using System.Linq;
using Data;
using Microsoft.EntityFrameworkCore;
using PersonagemModels;
using Xunit;

namespace ProjetoC.Tests;

public class PersonagemContextTests : IDisposable
{
    private readonly string _arquivoTemp = $"teste_{Guid.NewGuid()}.db";

    private class PersonagemContextTeste : PersonagemContext
    {
        private readonly string _arquivo;
        public PersonagemContextTeste(string arquivo) => _arquivo = arquivo;

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={_arquivo};Pooling=False");
    }

    private PersonagemContext NovoContexto()
    {
        var ctx = new PersonagemContextTeste(_arquivoTemp);
        ctx.Database.EnsureCreated();
        return ctx;
    }

    public void Dispose()
    {
        if (File.Exists(_arquivoTemp)) File.Delete(_arquivoTemp);
    }

    [Fact]
    public void Salvar_DevePersistirPersonagemComClasseEAtributos()
    {
        using (var db = NovoContexto())
        {
            var personagem = new Personagem(
                "Aragorn",
                new Classe("Guerreiro", "Combatente", 10),
                new Atributos(16, 14, 15, 10, 12, 13),
                12);

            db.Personagens.Add(personagem);
            db.SaveChanges();

            Assert.True(personagem.Id > 0);
        }

        using (var db = NovoContexto())
        {
            var salvo = db.Personagens.Single();

            Assert.Equal("Aragorn", salvo.nome);
            Assert.Equal("Guerreiro", salvo.classe.nome);
            Assert.Equal(10, salvo.classe.vidaBase);
            Assert.Equal(16, salvo.atributos.forca);
            Assert.Equal(13, salvo.atributos.carisma);
            Assert.Equal(12, salvo.pontosVida);
        }
    }

    [Fact]
    public void Excluir_DeveRemoverPersonagemDoBanco()
    {
        int id;
        using (var db = NovoContexto())
        {
            var p = new Personagem(
                "Frodo",
                new Classe("Halfling", "Pequeno e ágil", 6),
                new Atributos(8, 14, 10, 12, 11, 13),
                7);
            db.Personagens.Add(p);
            db.SaveChanges();
            id = p.Id;
        }

        using (var db = NovoContexto())
        {
            var p = db.Personagens.Find(id);
            Assert.NotNull(p);
            db.Personagens.Remove(p!);
            db.SaveChanges();
        }

        using (var db = NovoContexto())
        {
            Assert.Empty(db.Personagens.ToList());
        }
    }

    [Fact]
    public void Listar_DeveRetornarTodosOsPersonagensSalvos()
    {
        using (var db = NovoContexto())
        {
            db.Personagens.Add(new Personagem(
                "Legolas",
                new Classe("Elfo", "Arqueiro", 6),
                new Atributos(12, 17, 12, 13, 13, 12),
                8));
            db.Personagens.Add(new Personagem(
                "Gimli",
                new Classe("Anão", "Resistente", 8),
                new Atributos(15, 11, 16, 9, 10, 9),
                11));
            db.SaveChanges();
        }

        using (var db = NovoContexto())
        {
            var lista = db.Personagens.AsNoTracking().ToList();
            Assert.Equal(2, lista.Count);
            Assert.Contains(lista, p => p.nome == "Legolas");
            Assert.Contains(lista, p => p.nome == "Gimli");
        }
    }
}
