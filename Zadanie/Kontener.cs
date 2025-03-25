namespace Zadanie;

public abstract class Kontener
{
    public string NumerSeryjny { get; protected set; }
    public double MasaLadunku { get; protected set; }
    public double Wysokosc { get; protected set; }
    public double WagaWlasna { get; protected set; }
    public double Dlugosc { get; protected set; }
    public double MaksymalnaLadownosc { get; protected set; }
    private static Dictionary<string, int> _licznikiTypow = new Dictionary<string, int>();

    protected Kontener(string typ, double wysokosc, double wagaWlasna, double dlugosc, double maksymalnaLadownosc)
    {
        NumerSeryjny = GenerujNumerSeryjny(typ);
        Wysokosc = wysokosc;
        WagaWlasna = wagaWlasna;
        Dlugosc = dlugosc;
        MaksymalnaLadownosc = maksymalnaLadownosc;
    }

    private string GenerujNumerSeryjny(string typ)
    {
        if (!_licznikiTypow.ContainsKey(typ))
        _licznikiTypow[typ] = 0;
    
        _licznikiTypow[typ]++;
        return $"KON-{typ}-{_licznikiTypow[typ]}";
    }

    public virtual void ZaladujLadunek(double masa)
    {
        if (masa > MaksymalnaLadownosc)
        {
            throw new OverfillException($"Masa ładunku przekracza maksymalną ładowność kontenera {NumerSeryjny}");
        }
        MasaLadunku = masa;
    }

    public virtual void OproznijLadunek()
    {
        MasaLadunku = 0;
    }

    public override string ToString()
    {
        return $"Kontener {NumerSeryjny}: MasaLadunku={MasaLadunku}kg, Wysokosc={Wysokosc}cm, WagaWlasna={WagaWlasna}kg, Glebokosc={Dlugosc}cm, MaksymalnaLadownosc={MaksymalnaLadownosc}kg";
    }
}