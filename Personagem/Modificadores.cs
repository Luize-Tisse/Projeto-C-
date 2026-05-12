namespace Personagem;

public static class Modificadores
{
    public static int Calcular(int atributo)
    {
        if (atributo == 3)
            return -3;

        if (atributo >= 4 && atributo <= 5)
            return -2;

        if (atributo >= 6 && atributo <= 8)
            return -1;

        if (atributo >= 9 && atributo <= 12)
            return 0;

        if (atributo >= 13 && atributo <= 15)
            return 1;

        if (atributo >= 16 && atributo <= 17)
            return 2;

        return 3;
    }
}