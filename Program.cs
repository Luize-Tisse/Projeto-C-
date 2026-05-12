using System;
using Personagem;
using DistribuicaoAtributos;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("=== CRIAÇÃO DE PERSONAGEM - OLD DRAGON ===\n");

        // Nome
        Console.Write("Digite o nome do personagem: ");
        string nome = Console.ReadLine();

        // Classes
        Classe[] classes =
        {
            new Classe("Guerreiro", "Especialista em combate físico.", 10),
            new Classe("Clérigo", "Usuário de magia divina e cura.", 8),
            new Classe("Mago", "Usuário de magia arcana.", 4),
            new Classe("Ladrão", "Especialista em furtividade.", 6),
            new Classe("Elfo", "Combina combate e magia.", 6),
            new Classe("Anão", "Resistente e forte contra magia.", 8),
            new Classe("Halfling", "Ágil e furtivo.", 6)
        };

        Console.WriteLine("\nEscolha uma classe:");

        for (int i = 0; i < classes.Length; i++)
        {
            Console.WriteLine($"{i} - {classes[i].nome}");
        }

        int escolhaClasse;

        while (!int.TryParse(Console.ReadLine(), out escolhaClasse)
               || escolhaClasse < 0
               || escolhaClasse >= classes.Length)
        {
            Console.WriteLine("Escolha inválida.");
        }

        Classe classeEscolhida = classes[escolhaClasse];

        // Método de distribuição
        Console.WriteLine("\nEscolha o método de distribuição:");

        Console.WriteLine("1 - Clássico");
        Console.WriteLine("2 - Aventureiro");
        Console.WriteLine("3 - Heroico");

        int metodo;

        while (!int.TryParse(Console.ReadLine(), out metodo)
               || metodo < 1
               || metodo > 3)
        {
            Console.WriteLine("Escolha inválida.");
        }

        IDistribuicaoMetodo distribuicaoEscolhida;

        switch (metodo)
        {
            case 1:
                distribuicaoEscolhida = new MetodoClassico();
                break;

            case 2:
                distribuicaoEscolhida = new MetodoAventureiro();
                break;

            default:
                distribuicaoEscolhida = new MetodoHeroico();
                break;
        }

        // Distribuição dos atributos
        int[] atributos = distribuicaoEscolhida.DistribuirAtributos();

        // Criação do objeto Atributos
        Atributos atributosPersonagem = new Atributos(
            atributos[0],
            atributos[1],
            atributos[2],
            atributos[3],
            atributos[4],
            atributos[5]
        );

        // Lista de modificadores
        int[] modificadores = new int[6];

        for (int i = 0; i < atributos.Length; i++)
        {
            modificadores[i] =
                Modificadores.Calcular(atributos[i]);
        }

        // Constituição = índice 2
        int modificadorConstituicao = modificadores[2];

        // PV
        int pontosVida =
            classeEscolhida.vidaBase + modificadorConstituicao;

        // Personagem
        Personagem personagem = new Personagem(
            nome,
            classeEscolhida,
            atributosPersonagem,
            pontosVida
        );

        string[] nomesAtributos =
        {
            "Força",
            "Destreza",
            "Constituição",
            "Inteligência",
            "Sabedoria",
            "Carisma"
        };

        // Exibição
        Console.WriteLine("\n=== PERSONAGEM CRIADO ===");

        Console.WriteLine($"Nome: {personagem.nome}");
        Console.WriteLine($"Classe: {personagem.classe.nome}");
        Console.WriteLine($"Descrição: {personagem.classe.descricao}");

        Console.WriteLine("\nAtributos e Modificadores:");

        for (int i = 0; i < atributos.Length; i++)
        {
            Console.WriteLine(
                $"{nomesAtributos[i]}: {atributos[i]} " +
                $"| Modificador: {modificadores[i]:+#;-#;0}"
            );
        }

        Console.WriteLine($"\nPontos de Vida: {personagem.pontosVida}");


    }
}
