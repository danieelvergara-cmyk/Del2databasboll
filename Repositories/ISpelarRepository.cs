using Del2databasboll.Models;

namespace Del2databasboll.Repositories;

public interface ISpelarRepository
{
    void InitieraDatabas();
    void LäggTillSpelare(Spelare spelare);
    List<Spelare> HämtaAlla();
    Spelare? HämtaSpelareById(int id);
    Spelare? SökSpelare(string namn);
    bool TaBortSpelare(string namn);
    bool UppdateraSpelare(Spelare spelare);
    bool FinnsSpelare(string namn);
}
