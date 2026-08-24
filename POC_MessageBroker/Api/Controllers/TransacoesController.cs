using Api.DTOs.Inputs;
using Microsoft.AspNetCore.Mvc;

namespace POC_MessageBroker.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransacoesController : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> CriarTransacao([FromBody] TransacaoInput transacaoInput)
    {

    }
}
