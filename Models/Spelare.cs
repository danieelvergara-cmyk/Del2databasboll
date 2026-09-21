namespace Del2databasboll.Models;

public class Spelare
{
    public int Id { get; set; }
    public string Namn { get; set; } = string.Empty;
    public int Tröjnummer { get; set; }
    public int Mål { get; set; }
    public int MatcherSpelade { get; set; }

    public void SkrivInfo()
    {
        Console.WriteLine($"Id: {Id}, Namn: {Namn}, Tröjnummer: {Tröjnummer}, Mål: {Mål}, Matcher: {MatcherSpelade}");
    }
}
