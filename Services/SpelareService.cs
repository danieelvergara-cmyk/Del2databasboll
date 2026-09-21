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
        spelare.Namn = spelare.Namn.Trim();
        _repository.LäggTillSpelare(spelare);
    }

    public List<Spelare> HämtaAlla()
    {
        return _repository.HämtaAlla();
    }

    public Spelare? SökSpelare(string namn)
    {
        return _repository.SökSpelare(namn.Trim());
    }

    public bool TaBortSpelare(string namn)
    {
        return _repository.TaBortSpelare(namn.Trim());
    }

    public bool UppdateraSpelare(Spelare spelare)
    {
        spelare.Namn = spelare.Namn.Trim();
        return _repository.UppdateraSpelare(spelare);
    }
}
