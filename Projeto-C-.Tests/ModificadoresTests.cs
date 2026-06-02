using PersonagemModels;
using Xunit;

namespace ProjetoC.Tests;

public class ModificadoresTests
{
    [Theory]
    [InlineData(3, -3)]
    [InlineData(4, -2)]
    [InlineData(5, -2)]
    [InlineData(6, -1)]
    [InlineData(8, -1)]
    [InlineData(9, 0)]
    [InlineData(12, 0)]
    [InlineData(13, 1)]
    [InlineData(15, 1)]
    [InlineData(16, 2)]
    [InlineData(17, 2)]
    [InlineData(18, 3)]
    public void Calcular_DeveRetornarModificadorCorreto(int atributo, int esperado)
    {
        int resultado = Modificadores.Calcular(atributo);
        Assert.Equal(esperado, resultado);
    }
}
