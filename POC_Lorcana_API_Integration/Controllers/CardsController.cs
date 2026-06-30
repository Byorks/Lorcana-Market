using Microsoft.AspNetCore.Mvc;
using POC_Lorcana_API_Integration.Context;

namespace POC_Lorcana_API_Integration.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CardsController : ControllerBase
{
    private readonly ApiDbContext _db;

    public CardsController(ApiDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public IActionResult GetAllCards()
    {
        var cards = _db.Cards.ToList();
        return Ok(cards);
    }
}
