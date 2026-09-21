using Del2databasboll.Api.Contracts;
using Del2databasboll.Models;
using Del2databasboll.Services;
using Microsoft.AspNetCore.Mvc;

namespace Del2databasboll.Api.Controllers;

// [Nytt koncept: Controller]
// Varför: Samlar API-endpoints för en resurs (här: players/spelare).
[ApiController]
[Route("api/[controller]")]
public class PlayersController : ControllerBase
{
    private readonly ISpelareService _service;

    public PlayersController(ISpelareService service)
    {
        _service = service;
    }

    // [Nytt koncept: Endpoint]
    // GET /api/players
    [HttpGet]
    public ActionResult<List<Spelare>> GetAll()
    {
        var players = _service.HämtaAlla();
        return Ok(players); // HTTP 200
    }

    // GET /api/players/search?name=haal
    [HttpGet("search")]
    public ActionResult<List<Spelare>> SearchByName([FromQuery] string? name)
    {
        var players = _service.SökSpelareLista(name ?? string.Empty);
        return Ok(players); // HTTP 200, tom lista om ingen match
    }

    // GET /api/players/4
    [HttpGet("{id:int}")]
    public ActionResult<Spelare> GetById(int id)
    {
        var player = _service.HämtaSpelareById(id);
        if (player == null)
        {
            return NotFound(new ApiError
            {
                Code = "not_found",
                Message = $"Ingen spelare hittades med id {id}.",
                TraceId = HttpContext.TraceIdentifier
            }); // HTTP 404
        }

        return Ok(player); // HTTP 200
    }

    // POST /api/players
    [HttpPost]
    public ActionResult Create([FromBody] CreatePlayerRequest request)
    {
        var spelare = new Spelare
        {
            Id = request.Id,
            Namn = request.Namn,
            Tröjnummer = request.Tröjnummer,
            Mål = request.Mål,
            MatcherSpelade = request.MatcherSpelade
        };

        _service.LäggTillSpelare(spelare);
        return CreatedAtAction(nameof(GetById), new { id = spelare.Id }, spelare); // HTTP 201
    }

    // PUT /api/players/4
    [HttpPut("{id:int}")]
    public ActionResult Update(int id, [FromBody] UpdatePlayerRequest request)
    {
        var spelare = new Spelare
        {
            Id = id,
            Namn = request.Namn,
            Tröjnummer = request.Tröjnummer,
            Mål = request.Mål,
            MatcherSpelade = request.MatcherSpelade
        };

        bool updated = _service.UppdateraSpelare(spelare);
        if (!updated)
        {
            return NotFound(new ApiError
            {
                Code = "not_found",
                Message = $"Ingen spelare hittades med id {id}.",
                TraceId = HttpContext.TraceIdentifier
            });
        }

        return NoContent(); // HTTP 204
    }

    // DELETE /api/players/haaland
    [HttpDelete("{namn}")]
    public ActionResult Delete(string namn)
    {
        bool deleted = _service.TaBortSpelare(namn);
        if (!deleted)
        {
            return NotFound(new ApiError
            {
                Code = "not_found",
                Message = $"Ingen spelare hittades med namn '{namn}'.",
                TraceId = HttpContext.TraceIdentifier
            });
        }

        return NoContent(); // HTTP 204
    }
}
