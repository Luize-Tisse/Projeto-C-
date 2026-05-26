using System;

namespace DistribuicaoAtributos;

public static class EscolhaAtributos
{
    private static readonly string[] nomesAtributos =
    {
        "Força",
        "Destreza",
        "Constituição",
        "Inteligência",
        "Sabedoria",
        "Carisma"
    };

    public static int[] DistribuirEntreAtributos(int[] valoresRolados)
    {
        int[] disponiveis = (int[])valoresRolados.Clone();
        int[] atributosEscolhidos = new int[disponiveis.Length];

        for (int i = 0; i < atributosEscolhidos.Length; i++)
        {
            bool valido = false;

            while (!valido)
            {
                Console.WriteLine($"\nEscolha um valor para {nomesAtributos[i]}:");

                for (int j = 0; j < disponiveis.Length; j++)
                {
                    if (disponiveis[j] != -1)
                    {
                        Console.WriteLine($"{j} - {disponiveis[j]}");
                    }
                }

                Console.Write("Digite o índice: ");

                int escolha;
                bool numeroValido = int.TryParse(Console.ReadLine(), out escolha);

                if (!numeroValido)
                {
                    Console.WriteLine("Digite um número válido.");
                    continue;
                }

                if (escolha < 0 || escolha >= disponiveis.Length)
                {
                    Console.WriteLine("Índice inválido.");
                    continue;
                }

                if (disponiveis[escolha] == -1)
                {
                    Console.WriteLine("Esse atributo já foi utilizado.");
                    continue;
                }

                atributosEscolhidos[i] = disponiveis[escolha];
                disponiveis[escolha] = -1;
                valido = true;
            }
        }

        return atributosEscolhidos;
    }
}
