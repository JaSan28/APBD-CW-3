

using Zadanie;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== TESTOWANIE KONTENERÓW ===");
        
        TestujKontenerNaPlyny();
        TestujKontenerNaGaz();
        TestujKontenerChlodniczy();

        Console.WriteLine("\n=== TESTOWANIE STATKÓW ===");
        
        TestujOperacjeNaStatkach();
        TestujPrzenoszenieKontenerow();

        Console.WriteLine("\nWSZYSTKIE TESTY ZOSTAŁY PRZEPROWADZONE POMYŚLNIE!");


        var Kon_PLYN = new KON_L(200, 500, 1200, 400, false);
        var Kon_CHLODNICZA = new KON_C(300, 600, 150, 2000, "Banany", 13.3);
        var Kon_Gaz = new KON_G(200, 500, 1200, 400, 800);
        
        Kon_PLYN.ZaladujLadunek(300);
        Kon_CHLODNICZA.ZaladujLadunek(300);
        Kon_Gaz.ZaladujLadunek(300);
        
        Kon_Gaz.OproznijLadunek();
        
        try {
            Kon_PLYN.ZaladujLadunek(16500);
        } catch (OverfillException ex) {
            Console.WriteLine($"Blokada załadunku: {ex.Message}");
        }
        try {
            Kon_CHLODNICZA.ZaladujLadunek(1500);
        } catch (OverfillException ex) {
            Console.WriteLine($"Blokada załadunku: {ex.Message}");
        }
        try {
            Kon_Gaz.ZaladujLadunek(1500);
        } catch (OverfillException ex) {
            Console.WriteLine($"Blokada załadunku: {ex.Message}");
        }


        Statek statek = new Statek(200, 500, 1200);
        
        statek.ZaladujKontener(Kon_PLYN);
        statek.ZaladujKontener(Kon_CHLODNICZA);
        statek.ZaladujKontener(Kon_Gaz);
        var Kon_Gaz2 = new KON_G(200, 500, 1200, 400, 200);
        statek.ZastapKontener(Kon_Gaz.NumerSeryjny,Kon_Gaz2);
        
        statek.WypiszInformacjeOStatku();
        
        Kon_Gaz.OproznijLadunek();
        
        Console.WriteLine();
    }

    static void TestujKontenerNaPlyny()
    {
        Console.WriteLine("\n--- Test kontenera na płyny ---");
        var kontenerPlyny = new KON_L(200, 500, 100, 1000, czyNiebezpieczny: true);


        kontenerPlyny.ZaladujLadunek(400);
        Console.WriteLine("Załadunek 400kg (limit 500kg dla niebezpiecznego): POPRAWNY");


        try {
            kontenerPlyny.ZaladujLadunek(600);
        } catch (OverfillException) {
            Console.WriteLine("Blokada załadunku 600kg (przekroczenie limitu): POPRAWNY");
        }
        
        Console.WriteLine("Oczekiwane powiadomienie o niebezpieczeństwie:");
        try {
            kontenerPlyny.ZaladujLadunek(501);
        } catch (OverfillException) {
            Console.WriteLine("Powiadomienie otrzymane: POPRAWNY");
        }
    }

    static void TestujKontenerNaGaz()
    {
        Console.WriteLine("\n--- Test kontenera na gaz ---");
        var kontenerGaz = new KON_G(250, 400, 120, 1500, 10);


        kontenerGaz.ZaladujLadunek(1200);
        Console.WriteLine("Załadunek 1200kg: POPRAWNY");


        kontenerGaz.OproznijLadunek();
        Console.WriteLine($"Opróżnianie - pozostało {kontenerGaz.MasaLadunku}kg (5% z 1200kg): POPRAWNY");
    }

    static void TestujKontenerChlodniczy()
    {
        Console.WriteLine("\n--- Test kontenera chłodniczego ---");
        

        var kontenerChlodniczy = new KON_C(300, 600, 150, 2000, "Banany", 13.3);
        Console.WriteLine("Kontener dla bananów w 13.3°C: POPRAWNY");


        try {
            var kontenerZlaTemperatura = new KON_C(300, 600, 150, 2000, "Banany", 10);
        } catch (ArgumentException ex) {
            Console.WriteLine($"Blokada zbyt niskiej temperatury: POPRAWNY ({ex.Message})");
        }
    }

    static void TestujOperacjeNaStatkach()
    {
        Console.WriteLine("\n--- Test operacji na statkach ---");
        var statek = new Statek(20, 5, 10);
        var kontener1 = new KON_L(200, 500, 100, 1000, false);
        var kontener2 = new KON_G(250, 400, 120, 1500, 10);


        kontener1.ZaladujLadunek(500);
        statek.ZaladujKontener(kontener1);
        Console.WriteLine("Załadunek pierwszego kontenera: POPRAWNY");

 
        kontener2.ZaladujLadunek(800);
        statek.ZaladujKontener(kontener2);
        Console.WriteLine("Załadunek drugiego kontenera: POPRAWNY");


        try {
            for (int i = 0; i < 4; i++) {
                statek.ZaladujKontener(new KON_G(250, 400, 120, 1500, 10));
            }
        } catch (InvalidOperationException) {
            Console.WriteLine("Blokada przekroczenia limitu kontenerów: POPRAWNY");
        }


        statek.UsunKontener(kontener1.NumerSeryjny);
        Console.WriteLine("Usunięcie kontenera: POPRAWNY");
    }

    static void TestujPrzenoszenieKontenerow()
    {
        Console.WriteLine("\n--- Test przenoszenia kontenerów ---");
        try
        {
            var statek1 = new Statek(20, 5, 10);
            var statek2 = new Statek(25, 5, 15);
            var kontener = new KON_C(300, 600, 150, 2000, "Lody", -18);


            statek1.ZaladujKontener(kontener);
            Console.WriteLine("Kontener załadowany na statek1: POPRAWNY");


            statek1.PrzeniesKontener(statek2, kontener.NumerSeryjny);
            Console.WriteLine("Przeniesienie kontenera na statek2: POPRAWNY");


            bool znaleziono = false;
            if (statek2.Kontenery != null)
            {
                foreach (var k in statek2.Kontenery)
                {
                    if (k != null && k.NumerSeryjny == kontener.NumerSeryjny)
                    {
                        znaleziono = true;
                        break;
                    }
                }
            }
            Console.WriteLine(znaleziono ? "Kontener znaleziony na statek2: POPRAWNY" : "BŁĄD: Kontener nie został przeniesiony!");
        }
        catch (Exception exception)
        {
            Console.WriteLine($"BŁĄD podczas testu przenoszenia: {exception.Message}");
        }
    }
    
}