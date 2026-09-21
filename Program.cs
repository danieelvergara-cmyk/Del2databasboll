using Del2databasboll.Data;
using Del2databasboll.Models;
using Del2databasboll.Repositories;
using Del2databasboll.Services;

namespace Del2databasboll;

class Program
{
    private static readonly ISpelarRepository repository = new SpelareRepository();
    private static readonly ISpelareService service = new SpelareService(repository);

    static void Main()
    {
        var databaseInitializer = new DatabaseInitializer(repository);
        databaseInitializer.Initiera();

        while (true)
        {
            Console.WriteLine("\nMENY");
            Console.WriteLine("1. Lägg till spelare");
            Console.WriteLine("2. Visa spelare");
            Console.WriteLine("3. Sök spelare");
            Console.WriteLine("4. Hämta spelare via id");
            Console.WriteLine("5. Ta bort spelare");
            Console.WriteLine("6. Uppdatera spelare");
            Console.WriteLine("7. Avsluta");

            if (!int.TryParse(Console.ReadLine(), out int val))
            {
                Console.WriteLine("Ogiltig input");
                continue;
            }

            switch (val)
            {
                case 1:
                    KörSäkert(LäggTillSpelare);
                    break;
                case 2:
                    KörSäkert(VisaSpelare);
                    break;
                case 3:
                    KörSäkert(SökSpelare);
                    break;
                case 4:
                    KörSäkert(HämtaSpelareViaId);
                    break;
                case 5:
                    KörSäkert(TaBortSpelare);
                    break;
                case 6:
                    KörSäkert(UppdateraSpelare);
                    break;
                case 7:
                    return;
                default:
                    Console.WriteLine("Välj ett tal mellan 1-7");
                    break;
            }
        }
    }

    static void KörSäkert(Action action)
    {
        try
        {
            action();
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Valideringsfel: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Något gick fel: {ex.Message}");
        }
    }

    static void LäggTillSpelare()
    {
        Console.Write("Namn: ");
        string namn = Console.ReadLine() ?? string.Empty;

        Console.Write("Tröjnummer: ");
        int tröjnummer = LäsHeltal();

        Console.Write("Antal mål: ");
        int mål = LäsHeltal();

        Console.Write("Antal matcher: ");
        int matcher = LäsHeltal();

        Console.Write("Id: ");
        int id = LäsHeltal();

        service.LäggTillSpelare(new Spelare
        {
            Id = id,
            Namn = namn,
            Tröjnummer = tröjnummer,
            Mål = mål,
            MatcherSpelade = matcher
        });

        Console.WriteLine("Spelare sparad i databasen.");
    }

    static void VisaSpelare()
    {
        var lista = service.HämtaAlla();
        if (!lista.Any())
        {
            Console.WriteLine("Inga spelare i databasen.");
            return;
        }

        foreach (var spelare in lista)
        {
            spelare.SkrivInfo();
        }
    }

    static void SökSpelare()
    {
        Console.Write("Sök namn: ");
        string namn = Console.ReadLine() ?? string.Empty;

        var spelare = service.SökSpelare(namn);
        if (spelare == null)
        {
            Console.WriteLine("Spelare hittades inte.");
            return;
        }

        spelare.SkrivInfo();
    }

    static void HämtaSpelareViaId()
    {
        Console.Write("Id att hämta: ");
        int id = LäsHeltal();

        var spelare = service.HämtaSpelareById(id);
        if (spelare == null)
        {
            Console.WriteLine("Spelare hittades inte.");
            return;
        }

        spelare.SkrivInfo();
    }

    static void TaBortSpelare()
    {
        Console.Write("Namn att ta bort: ");
        string namn = Console.ReadLine() ?? string.Empty;

        bool borttagen = service.TaBortSpelare(namn);
        Console.WriteLine(borttagen ? "Spelare borttagen." : "Spelare hittades inte.");
    }

    static void UppdateraSpelare()
    {
        Console.Write("Id på spelare att uppdatera: ");
        int id = LäsHeltal();

        Console.Write("Nytt namn: ");
        string namn = Console.ReadLine() ?? string.Empty;

        Console.Write("Nytt tröjnummer: ");
        int tröjnummer = LäsHeltal();

        Console.Write("Nytt antal mål: ");
        int mål = LäsHeltal();

        Console.Write("Nytt antal matcher: ");
        int matcher = LäsHeltal();

        bool uppdaterad = service.UppdateraSpelare(new Spelare
        {
            Id = id,
            Namn = namn,
            Tröjnummer = tröjnummer,
            Mål = mål,
            MatcherSpelade = matcher
        });

        Console.WriteLine(uppdaterad ? "Spelare uppdaterad." : "Spelare med id hittades inte.");
    }

    static int LäsHeltal()
    {
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out int värde))
            {
                return värde;
            }

            Console.Write("Skriv ett heltal: ");
        }
    }
}
