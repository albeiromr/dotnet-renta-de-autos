using Application.Vehicles.SearchVehicles;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/vehicles")]
public class VehicleController : ControllerBase
{
    //Sender de mediatr
    private readonly ISender? _sender;

    public VehicleController(ISender? sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> SearchVehicles( 
        DateOnly startDate, 
        DateOnly endDate,
        CancellationToken cancellationToken
    )
    {
        var query =  new SearchVehiclesQuery(startDate, endDate);
        var actionResult = await _sender!.Send(query, cancellationToken);
        return Ok(actionResult);
    }
}
