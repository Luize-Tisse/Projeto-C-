using System;

namespace DistribuicaoAtributos;

public class MetodoClassico : IDistribuicaoMetodo
{
    private Random random = new Random();

    public int[] DistribuirAtributos()
    {
        int[] atributos = new int[6];

        for (int i = 0; i < atributos.Length; i++)
        {
            atributos[i] = Rolar3d6();
        }

        return atributos;
    }

    private int Rolar3d6()
    {
        return random.Next(1, 7) +
               random.Next(1, 7) +
               random.Next(1, 7);
    }
}