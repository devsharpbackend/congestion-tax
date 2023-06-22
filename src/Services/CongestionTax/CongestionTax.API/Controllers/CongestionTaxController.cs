namespace Fintranet.Services.CongestionTax.API.Controllers;

public class CongestionTaxController : ApiAdminController
{

  private readonly IMediator _mediator;
  public CongestionTaxController(IMediator mediator)
  {
    _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
  }

    #region Api

    [Route("CalculateCongestionTax")]
    [HttpPost(), Produces("application/json")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Conflict)]
    [ProducesResponseType(typeof(decimal), (int)HttpStatusCode.Created)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> CalculateCongestionTax(CalculateCongestionTaxCommand command)
    {
        return Ok(_mediator.Send(command));
    }

    

    #endregion

}

