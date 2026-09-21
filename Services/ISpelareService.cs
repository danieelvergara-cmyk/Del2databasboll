using Del2databasboll.Models;

namespace Del2databasboll.Services;

public interface ISpelareService
{
    void LäggTillSpelare(Spelare spelare);
    List<Spelare> HämtaAlla();
    Spelare? SökSpelare(string namn);
    bool TaBortSpelare(string namn);
    bool UppdateraSpelare(Spelare spelare);
}
