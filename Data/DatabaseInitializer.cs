using Del2databasboll.Models;
using Del2databasboll.Repositories;

namespace Del2databasboll.Data;

public class DatabaseInitializer
{
    private readonly ISpelarRepository _repository;

    public DatabaseInitializer(ISpelarRepository repository)
    {
        _repository = repository;
    }

    public void Initiera()
    {
        _repository.InitieraDatabas();
        SeedData();
    }

    private void SeedData()
    {
        if (!_repository.FinnsSpelare("Haaland"))
        {
            _repository.LäggTillSpelare(new Spelare
            {
                Id = 4,
                Namn = "Haaland",
                Tröjnummer = 9,
                Mål = 300,
                MatcherSpelade = 500
            });
        }
    }
}
