using System;

namespace DistribuicaoAtributos;

public class MetodoAventureiro : IDistribuicaoMetodo
{
    private Random random = new Random();

    public int[] DistribuirAtributos()
    {
        int[] atributosRolados = new int[6];
        int[] atributosEscolhidos = new int[6];

        string[] nomesAtributos =
        {
            "Força",
            "Destreza",
            "Constituição",
            "Inteligência",
            "Sabedoria",
            "Carisma"
        };

        for (int i = 0; i < atributosRolados.Length; i++)
        {
            atributosRolados[i] = Rolar3d6();
        }


        for (int i = 0; i < atributosEscolhidos.Length; i++)
        {
            bool valido = false;

            while (!valido)
            {
                Console.WriteLine($"\nEscolha um valor para {nomesAtributos[i]}:");


                for (int j = 0; j < atributosRolados.Length; j++)
                {
                    if (atributosRolados[j] != -1)
                    {
                        Console.WriteLine($"{j} - {atributosRolados[j]}");
                    }
                }

                Console.Write("Digite o índice: ");

                int escolha;
                bool numeroValido = false;

                numeroValido = int.TryParse(Console.ReadLine(), out escolha);


                if (!numeroValido)
                {
                    Console.WriteLine("Digite um número válido.");
                    continue;
                }


                if (escolha < 0 || escolha >= atributosRolados.Length)
                {
                    Console.WriteLine("Índice inválido.");
                    continue;
                }


                if (atributosRolados[escolha] == -1)
                {
                    Console.WriteLine("Esse atributo já foi utilizado.");
                    continue;
                }

                atributosEscolhidos[i] = atributosRolados[escolha];


                atributosRolados[escolha] = -1;

                valido = true;
            }
        }

        return atributosEscolhidos;
    }

    private int Rolar3d6()
    {
        return random.Next(1, 7) +
               random.Next(1, 7) +
               random.Next(1, 7);
    }
}