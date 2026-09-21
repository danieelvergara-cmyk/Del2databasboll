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

    // GET /api/players/4
    [HttpGet("{id:int}")]
    public ActionResult<Spelare> GetById(int id)
    {
        try
        {
            var player = _service.HämtaSpelareById(id);
            if (player == null)
            {
                return NotFound(); // HTTP 404
            }

            return Ok(player); // HTTP 200
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message); // HTTP 400
        }
    }

    // POST /api/players
    [HttpPost]
    public ActionResult Create([FromBody] Spelare spelare)
    {
        try
        {
            _service.LäggTillSpelare(spelare);
            return CreatedAtAction(nameof(GetById), new { id = spelare.Id }, spelare); // HTTP 201
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message); // HTTP 400
        }
    }

    // PUT /api/players/4
    [HttpPut("{id:int}")]
    public ActionResult Update(int id, [FromBody] Spelare spelare)
    {
        try
        {
            spelare.Id = id;
            bool updated = _service.UppdateraSpelare(spelare);
            if (!updated)
            {
                return NotFound();
            }

            return NoContent(); // HTTP 204
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // DELETE /api/players/haaland
    [HttpDelete("{namn}")]
    public ActionResult Delete(string namn)
    {
        try
        {
            bool deleted = _service.TaBortSpelare(namn);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent(); // HTTP 204
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
