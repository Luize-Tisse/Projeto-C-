using System;
using System.Linq;
using Data;
using DistribuicaoAtributos;
using Microsoft.EntityFrameworkCore;
using PersonagemModels;

public class Program
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

    public static void Main(string[] args)
    {
        Console.WriteLine("=== CRIAÇÃO DE PERSONAGEM - OLD DRAGON ===\n");

        using var db = new PersonagemContext();
        db.Database.EnsureCreated();

        Console.WriteLine("1 - Criar novo personagem");
        Console.WriteLine("2 - Listar personagens salvos");
        Console.WriteLine("3 - Excluir personagem");
        Console.Write("Escolha: ");

        int opcao;
        while (!int.TryParse(Console.ReadLine(), out opcao) || opcao < 1 || opcao > 3)
        {
            Console.WriteLine("Escolha inválida.");
        }

        switch (opcao)
        {
            case 2:
                ListarPersonagens(db);
                return;

            case 3:
                ExcluirPersonagem(db);
                return;

            default:
                CriarPersonagem(db);
                return;
        }
    }

    private static void CriarPersonagem(PersonagemContext db)
    {
        Console.Write("Digite o nome do personagem: ");
        string nome = Console.ReadLine();

        Classe[] classes =
        {
            new Classe("Guerreiro", "Especialista em combate, capaz de usar qualquer arma e armadura. Possui alta resistência e grande capacidade ofensiva.", 10),
            new Classe("Clérigo", "Servo de uma divindade, utiliza magias divinas, cura aliados e combate criaturas malignas e mortos-vivos.", 8),
            new Classe("Mago", "Estudioso das artes arcanas, utiliza magias poderosas, porém possui baixa resistência física.", 4),
            new Classe("Ladrão", "Especialista em furtividade, armadilhas, escalada, abertura de fechaduras e ataques sorrateiros.", 6),
            new Classe("Elfo", "Combina habilidades marciais e mágicas, possuindo afinidade natural com magia e boa percepção.", 6),
            new Classe("Anão", "Guerreiro resistente e disciplinado, famoso por sua resistência física e proteção contra magia.", 8),
            new Classe("Halfling", "Pequeno, ágil e discreto, excelente em furtividade, esquiva e combate à distância.", 6)
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


        int[] atributos = distribuicaoEscolhida.DistribuirAtributos();


        Atributos atributosPersonagem = new Atributos(
            atributos[0],
            atributos[1],
            atributos[2],
            atributos[3],
            atributos[4],
            atributos[5]
        );


        int[] modificadores = new int[6];

        for (int i = 0; i < atributos.Length; i++)
        {
            modificadores[i] =
                Modificadores.Calcular(atributos[i]);
        }


        int modificadorConstituicao = modificadores[2];


        int pontosVida =
            classeEscolhida.vidaBase + modificadorConstituicao;


        var personagem = new Personagem(
            nome,
            classeEscolhida,
            atributosPersonagem,
            pontosVida
        );

        db.Personagens.Add(personagem);
        db.SaveChanges();


        Console.WriteLine("\n=== PERSONAGEM CRIADO E SALVO ===");
        Console.WriteLine($"ID: {personagem.Id}");
        ImprimirPersonagem(personagem, modificadores);
    }

    private static void ListarPersonagens(PersonagemContext db)
    {
        var personagens = db.Personagens.AsNoTracking().ToList();

        if (personagens.Count == 0)
        {
            Console.WriteLine("\nNenhum personagem salvo.");
            return;
        }

        Console.WriteLine($"\n=== {personagens.Count} PERSONAGEM(NS) SALVO(S) ===");

        foreach (var personagem in personagens)
        {
            int[] atributos =
            {
                personagem.atributos.forca,
                personagem.atributos.destreza,
                personagem.atributos.constituicao,
                personagem.atributos.inteligencia,
                personagem.atributos.sabedoria,
                personagem.atributos.carisma
            };

            int[] modificadores = new int[6];
            for (int i = 0; i < atributos.Length; i++)
            {
                modificadores[i] = Modificadores.Calcular(atributos[i]);
            }

            Console.WriteLine($"\n--- ID {personagem.Id} ---");
            ImprimirPersonagem(personagem, modificadores);
        }
    }

    private static void ExcluirPersonagem(PersonagemContext db)
    {
        var personagens = db.Personagens.AsNoTracking().ToList();
        if (personagens.Count == 0)
        {
            Console.WriteLine("Nenhum personagem salvo.");
            return;
        }
        Console.WriteLine("=== PERSONAGEM(NS) SALVO(S) ===");
        foreach (var p in personagens)
        {
            Console.WriteLine($"ID: {p.Id} - Nome: {p.nome}");
        }

        Console.Write("Digite o ID do personagem a excluir: ");
        int id = int.Parse(Console.ReadLine());

        var personagem = db.Personagens.Find(id);

        if (personagem == null)
        {
            Console.WriteLine("Personagem não encontrado.");
            return;
        }

        db.Personagens.Remove(personagem);
        db.SaveChanges();

        Console.WriteLine("Personagem excluído.");
    }

    private static void ImprimirPersonagem(Personagem personagem, int[] modificadores)
    {
        int[] atributos =
        {
            personagem.atributos.forca,
            personagem.atributos.destreza,
            personagem.atributos.constituicao,
            personagem.atributos.inteligencia,
            personagem.atributos.sabedoria,
            personagem.atributos.carisma
        };

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
