using Del2databasboll.Models;
using Del2databasboll.Repositories;

namespace Del2databasboll.Services;

public class SpelareService : ISpelareService
{
    private readonly ISpelarRepository _repository;

    public SpelareService(ISpelarRepository repository)
    {
        _repository = repository;
    }

    public void LäggTillSpelare(Spelare spelare)
    {
        ValideraSpelare(spelare);
        _repository.LäggTillSpelare(spelare);
    }

    public List<Spelare> HämtaAlla()
    {
        return _repository.HämtaAlla();
    }

    public Spelare? HämtaSpelareById(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Id måste vara större än 0.");
        }

        return _repository.HämtaSpelareById(id);
    }

    public Spelare? SökSpelare(string namn)
    {
        if (string.IsNullOrWhiteSpace(namn))
        {
            throw new ArgumentException("Namn får inte vara tomt.");
        }

        return _repository.SökSpelare(namn.Trim());
    }

    public List<Spelare> SökSpelareLista(string namn)
    {
        if (string.IsNullOrWhiteSpace(namn))
        {
            throw new ArgumentException("Söktext får inte vara tom.");
        }

        return _repository.SökSpelareLista(namn.Trim());
    }

    public bool TaBortSpelare(string namn)
    {
        if (string.IsNullOrWhiteSpace(namn))
        {
            throw new ArgumentException("Namn får inte vara tomt.");
        }

        return _repository.TaBortSpelare(namn.Trim());
    }

    public bool UppdateraSpelare(Spelare spelare)
    {
        ValideraSpelare(spelare);
        return _repository.UppdateraSpelare(spelare);
    }

    private static void ValideraSpelare(Spelare spelare)
    {
        if (spelare.Id <= 0)
        {
            throw new ArgumentException("Id måste vara större än 0.");
        }

        spelare.Namn = spelare.Namn.Trim();
        if (string.IsNullOrWhiteSpace(spelare.Namn))
        {
            throw new ArgumentException("Namn får inte vara tomt.");
        }

        if (spelare.Tröjnummer <= 0)
        {
            throw new ArgumentException("Tröjnummer måste vara större än 0.");
        }

        if (spelare.Mål < 0)
        {
            throw new ArgumentException("Antal mål kan inte vara negativt.");
        }

        if (spelare.MatcherSpelade < 0)
        {
            throw new ArgumentException("Antal matcher kan inte vara negativt.");
        }
    }
}
