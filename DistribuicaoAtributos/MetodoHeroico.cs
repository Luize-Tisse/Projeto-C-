using System;
using System.Linq;

namespace DistribuicaoAtributos;

public class MetodoHeroico : IDistribuicaoMetodo
{
    private Random random = new Random();

    public int[] DistribuirAtributos()
    {
        int[] atributos = new int[6];

        for (int i = 0; i < atributos.Length; i++)
        {
            atributos[i] = Rolar4d6MenosMenor();
        }

        return atributos;
    }

    private int Rolar4d6MenosMenor()
    {
        int[] dados =
        {
            random.Next(1, 7),
            random.Next(1, 7),
            random.Next(1, 7),
            random.Next(1, 7)
        };

        int menor = dados.Min();

        return dados.Sum() - menor;
    }
}