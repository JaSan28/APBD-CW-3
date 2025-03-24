namespace Zadanie;

public class KON_L : Kontener, IHazardNotifier
{
    public bool CzyNiebezpieczny { get; private set; }

    public KON_L(double wysokosc, double wagaWlasna, double dlugosc, double maksymalnaLadownosc, bool czyNiebezpieczny)
        : base("L", wysokosc, wagaWlasna, dlugosc, maksymalnaLadownosc)
    {
        CzyNiebezpieczny = czyNiebezpieczny;
    }

    public override void ZaladujLadunek(double masa)
    {
        double maksymalnaDozwolona = CzyNiebezpieczny ? MaksymalnaLadownosc * 0.5 : MaksymalnaLadownosc * 0.9;
        if (masa > maksymalnaDozwolona)
        {
            PowiadomONiebezpieczenstwie(NumerSeryjny);
            throw new OverfillException($"Masa ładunku przekracza dozwoloną pojemność kontenera {NumerSeryjny}");
        }
        base.ZaladujLadunek(masa);
    }

    public void PowiadomONiebezpieczenstwie(string numerKontenera)
    {
        Console.WriteLine($"Wykryto niebezpieczną sytuację w kontenerze {numerKontenera}");
    }
}