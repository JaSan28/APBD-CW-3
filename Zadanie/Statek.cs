namespace Zadanie;

public class Statek
{
    public Kontener[] Kontenery;
    private int LiczbaKontenerow;
    public double MaksymalnaPredkosc { get; private set; }
    public int MaksymalnaLiczbaKontenerow { get; private set; }
    public double MaksymalnaWaga { get; private set; }

    public Statek(double maksymalnaPredkosc, int maksymalnaLiczbaKontenerow, double maksymalnaWaga)
    {
        Kontenery = new Kontener[maksymalnaLiczbaKontenerow];
        LiczbaKontenerow = 0;
        MaksymalnaPredkosc = maksymalnaPredkosc;
        MaksymalnaLiczbaKontenerow = maksymalnaLiczbaKontenerow;
        MaksymalnaWaga = maksymalnaWaga;
    }

    public void ZaladujKontener(Kontener kontener)
    {
        if (LiczbaKontenerow >= MaksymalnaLiczbaKontenerow)
        {
            throw new InvalidOperationException("Nie można załadować więcej kontenerów: osiągnięto maksymalną liczbę kontenerów.");
        }

        double calkowitaWaga = 0;
        for (int i = 0; i < LiczbaKontenerow; i++)
        {
            calkowitaWaga += Kontenery[i].MasaLadunku + Kontenery[i].WagaWlasna;
        }
        calkowitaWaga += kontener.MasaLadunku + kontener.WagaWlasna;

        if (calkowitaWaga > MaksymalnaWaga * 1000)
        {
            throw new InvalidOperationException("Nie można załadować kontenera: przekroczono maksymalną wagę.");
        }

        Kontenery[LiczbaKontenerow] = kontener;
        LiczbaKontenerow++;
    }

    public void UsunKontener(string numerSeryjny)
    {
        for (int i = 0; i < LiczbaKontenerow; i++)
        {
            if (Kontenery[i].NumerSeryjny == numerSeryjny)
            {
                for (int j = i; j < LiczbaKontenerow - 1; j++)
                {
                    Kontenery[j] = Kontenery[j + 1];
                }
                LiczbaKontenerow--;
                return;
            }
        }
    }

    public void ZastapKontener(string numerSeryjny, Kontener nowyKontener)
    {
        for (int i = 0; i < LiczbaKontenerow; i++)
        {
            if (Kontenery[i].NumerSeryjny == numerSeryjny)
            {
                Kontenery[i] = nowyKontener;
                return;
            }
        }
    }

    public void PrzeniesKontener(Statek docelowyStatek, string numerSeryjny)
    {
        for (int i = 0; i < LiczbaKontenerow; i++)
        {
            if (Kontenery[i].NumerSeryjny == numerSeryjny)
            {
                docelowyStatek.ZaladujKontener(Kontenery[i]);
                UsunKontener(numerSeryjny);
                return;
            }
        }
    }

    public void WypiszInformacjeOStatku()
    {
        Console.WriteLine($"Informacje o statku: MaksymalnaPredkosc={MaksymalnaPredkosc} węzłów, MaksymalnaLiczbaKontenerow={MaksymalnaLiczbaKontenerow}, MaksymalnaWaga={MaksymalnaWaga} ton");
        for (int i = 0; i < LiczbaKontenerow; i++)
        {
            Console.WriteLine(Kontenery[i]);
        }
    }
}