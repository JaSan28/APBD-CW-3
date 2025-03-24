namespace Zadanie;

public class KON_C : Kontener
{
    public string TypProduktu { get; private set; }
    public double Temperatura { get; private set; }


    private static readonly (string Produkt, double Temperatura)[] ProduktyITemperatury =
    {
        ("Banany", 13.3),
        ("Czekolada", 18),
        ("Ryba", 2),
        ("Mięso", -15),
        ("Lody", -18),
        ("Mrożona Pizza", -30),
        ("Ser", 7.2),
        ("Kiełbasa", 5),
        ("Masło", 20.5),
        ("Jajka", 19)
    };

    public KON_C(double wysokosc, double wagaWlasna, double dlugosc, double maksymalnaLadownosc, string typProduktu, double temperatura)
        : base("C", wysokosc, wagaWlasna, dlugosc, maksymalnaLadownosc)
    {

        bool produktIstnieje = false;
        double wymaganaTemperatura = 0;

        foreach (var produkt in ProduktyITemperatury)
        {
            if (produkt.Produkt == typProduktu)
            {
                produktIstnieje = true;
                wymaganaTemperatura = produkt.Temperatura;
                break;
            }
        }

        if (!produktIstnieje)
        {
            throw new ArgumentException($"Typ produktu {typProduktu} nie jest obsługiwany.");
        }


        if (temperatura < wymaganaTemperatura)
        {
            throw new ArgumentException($"Temperatura {temperatura} jest zbyt niska dla {typProduktu}. Wymagana temperatura: {wymaganaTemperatura}°C.");
        }

        TypProduktu = typProduktu;
        Temperatura = temperatura;
    }

    public override string ToString()
    {
        return base.ToString() + $", TypProduktu={TypProduktu}, Temperatura={Temperatura}°C";
    }
}