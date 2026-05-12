namespace PersonagemModels;

public class Classe
{
    public string nome { get; set; }
    public string descricao { get; set; }
    public int vidaBase { get; set; }

    public Classe(string nome, string descricao, int vidaBase)
    {
        this.nome = nome;
        this.descricao = descricao;
        this.vidaBase = vidaBase;
    }
}