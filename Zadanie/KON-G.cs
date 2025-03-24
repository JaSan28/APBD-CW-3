namespace Zadanie;

public class KON_G : Kontener, IHazardNotifier
{
    public double Cisnienie { get; private set; }

    public KON_G(double wysokosc, double wagaWlasna, double dlugosc, double maksymalnaLadownosc, double cisnienie)
        : base("G", wysokosc, wagaWlasna, dlugosc, maksymalnaLadownosc)
    {
        Cisnienie = cisnienie;
    }

    public override void OproznijLadunek()
    {
        MasaLadunku *= 0.05;
    }

    public void PowiadomONiebezpieczenstwie(string numerKontenera)
    {
        Console.WriteLine($"Wykryto niebezpieczną sytuację w kontenerze {numerKontenera}");
    }
}