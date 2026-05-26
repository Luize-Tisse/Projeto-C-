using System;

namespace DistribuicaoAtributos;

public class MetodoAventureiro : IDistribuicaoMetodo
{
    private Random random = new Random();

    public int[] DistribuirAtributos()
    {
        int[] atributosRolados = new int[6];

        for (int i = 0; i < atributosRolados.Length; i++)
        {
            atributosRolados[i] = Rolar3d6();
        }

        return EscolhaAtributos.DistribuirEntreAtributos(atributosRolados);
    }

    private int Rolar3d6()
    {
        return random.Next(1, 7) +
               random.Next(1, 7) +
               random.Next(1, 7);
    }
}
