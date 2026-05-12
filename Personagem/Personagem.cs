namespace PersonagemModels;

public class Personagem
{
    public string nome { get; set; }
    public Classe classe { get; set; }
    public Atributos atributos { get; set; }
    public int pontosVida { get; set; }

    public Personagem(string nome, Classe classe, Atributos atributos, int pontosVida)
    {
        this.nome = nome;
        this.classe = classe;
        this.atributos = atributos;
        this.pontosVida = pontosVida;
    }
}