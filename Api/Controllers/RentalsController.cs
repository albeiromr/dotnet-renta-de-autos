using Api.Requests;
using Application.Rentals.BookRental;
using Application.Rentals.GetRental;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/rentals")]
public class RentalsController : ControllerBase
{
    //Sender de mediatr
    private readonly ISender? _sender;

    public RentalsController(ISender? sender)
    {
        _sender = sender;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRental(
        Guid id,
        CancellationToken cancellationToken
    )
    {
        var query = new GetRentalQuery(id);
        var actionResult = await _sender!.Send(query, cancellationToken);
        return actionResult.Success ? Ok(actionResult) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> BookRental(
        Guid id,
        BookRentalRequest request,
        CancellationToken cancellationToken
    )
    {
        var command = new BookRentalCommand(
            request.VehicleId, 
            request.UserId, 
            request.StartDate, 
            request.EndDate
        );

        var actionResult = await _sender!.Send(command, cancellationToken);

        if(actionResult.Failure)
            return BadRequest(actionResult.Error);

        return CreatedAtAction(nameof(GetRental), new {id = actionResult.Value}, actionResult.Value);
    }
}
