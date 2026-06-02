using PersonagemModels;
using Xunit;

namespace ProjetoC.Tests;

public class PersonagemTests
{
    [Fact]
    public void Construtor_DeveAtribuirTodosOsCampos()
    {
        var classe = new Classe("Mago", "Estudioso arcano", 4);
        var atributos = new Atributos(10, 11, 12, 13, 14, 15);

        var personagem = new Personagem("Gandalf", classe, atributos, 8);

        Assert.Equal("Gandalf", personagem.nome);
        Assert.Equal(classe, personagem.classe);
        Assert.Equal(atributos, personagem.atributos);
        Assert.Equal(8, personagem.pontosVida);
    }

    [Fact]
    public void Classe_Construtor_DeveAtribuirCampos()
    {
        var classe = new Classe("Guerreiro", "Forte", 10);

        Assert.Equal("Guerreiro", classe.nome);
        Assert.Equal("Forte", classe.descricao);
        Assert.Equal(10, classe.vidaBase);
    }

    [Fact]
    public void Atributos_Construtor_DeveAtribuirCampos()
    {
        var a = new Atributos(1, 2, 3, 4, 5, 6);

        Assert.Equal(1, a.forca);
        Assert.Equal(2, a.destreza);
        Assert.Equal(3, a.constituicao);
        Assert.Equal(4, a.inteligencia);
        Assert.Equal(5, a.sabedoria);
        Assert.Equal(6, a.carisma);
    }
}
