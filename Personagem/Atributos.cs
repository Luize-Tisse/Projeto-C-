namespace Personagem;

public class Atributos
{
    public int forca { get; set; }
    public int destreza { get; set; }
    public int constituicao { get; set; }
    public int inteligencia { get; set; }
    public int sabedoria { get; set; }
    public int carisma { get; set; }

    public Atributos(
        int forca,
        int destreza,
        int constituicao,
        int inteligencia,
        int sabedoria,
        int carisma)
    {
        this.forca = forca;
        this.destreza = destreza;
        this.constituicao = constituicao;
        this.inteligencia = inteligencia;
        this.sabedoria = sabedoria;
        this.carisma = carisma;
    }
}