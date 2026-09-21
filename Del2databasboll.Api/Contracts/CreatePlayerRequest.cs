using System.ComponentModel.DataAnnotations;

namespace Del2databasboll.Api.Contracts;

// [Nytt koncept: DTO/Request-model]
// Varför: API-klienten skickar denna typ, inte hela domänmodellen direkt.
public class CreatePlayerRequest
{
    [Range(1, int.MaxValue, ErrorMessage = "Id måste vara större än 0.")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Namn är obligatoriskt.")]
    public string Namn { get; set; } = string.Empty;

    [Range(1, 99, ErrorMessage = "Tröjnummer måste vara mellan 1 och 99.")]
    public int Tröjnummer { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Mål kan inte vara negativt.")]
    public int Mål { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Matcher kan inte vara negativt.")]
    public int MatcherSpelade { get; set; }
}
