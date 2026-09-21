namespace Del2databasboll.Api.Contracts;

// [Nytt koncept: Enhetlig felmodell]
// Varför: Klienter får samma JSON-format för fel oavsett endpoint.
public class ApiError
{
    public string Code { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string TraceId { get; set; } = string.Empty;
}
