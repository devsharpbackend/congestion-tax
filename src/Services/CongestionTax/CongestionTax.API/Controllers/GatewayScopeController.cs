using MediatR;
using System.Net;

namespace IHO.Services.Gateway.API.Controllers;

public class GatewayScopeController : ApiAdminController
{

  private readonly IMediator _mediator;
  public GatewayScopeController(IMediator mediator)
  {
    _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
  }

    #region GatewayScope Api

    [Route("RegisterGatewayScope")]
    [HttpPost(), Produces("application/json")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Conflict)]
    //[ProducesResponseType(typeof(GatewayScopeItemViewModel), (int)HttpStatusCode.Created)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> RegisterGatewayScope()
    {
        return Ok();
    }

    //[Route("UpdateGatewayScope")]
    //[HttpPut]
    //[ProducesResponseType((int)HttpStatusCode.NotFound)]
    //[ProducesResponseType((int)HttpStatusCode.Created)]
    //[ProducesResponseType((int)HttpStatusCode.Conflict)]
    //[ProducesResponseType(typeof(GatewayScopeItemViewModel), (int)HttpStatusCode.OK)]
    //[ProducesDefaultResponseType]
    //public async Task<ActionResult> UpdateApiScope([FromBody] UpdateGatewayScopeCommand command)
    //{
    //  var gatewayScopeUpdated = await _mediator.Send(command);
    //  return Updated(gatewayScopeUpdated);
    //}

    //[HttpDelete]
    //[Route("DeleteGatewayScope")]
    //[ProducesResponseType((int)HttpStatusCode.NotFound)]
    //[ProducesResponseType((int)HttpStatusCode.BadRequest)]
    //[ProducesResponseType((int)HttpStatusCode.NoContent)]
    //public async Task<ActionResult> DeleteGatewayScope([FromBody] DeleteGatewayScopeCommand command)
    //{
    //  if (string.IsNullOrEmpty(command.Id))
    //  {
    //    return BadRequest(ValidationMessages.ID_IS_EMPTY);
    //  }
    //  await _mediator.Send(command);
    //  return Deleted();
    //}


    //[HttpGet]
    //[Route("FindGatewayById/{Id}")]
    //[ProducesResponseType((int)HttpStatusCode.NotFound)]
    //[ProducesResponseType((int)HttpStatusCode.BadRequest)]
    //[ProducesResponseType(typeof(GatewayScopeItemViewModel), (int)HttpStatusCode.OK)]
    //public async Task<ActionResult<GatewayScopeItemViewModel>> FindGatewayById([FromRoute] FindGatewayScopeByIdQuery query)
    //{
    //  if (string.IsNullOrEmpty(query.Id))
    //  {
    //    return BadRequest(ValidationMessages.ID_IS_EMPTY);
    //  }
    //  var item = await _mediator.Send(query);
    //  return Ok(item);
    //}

    //[HttpGet]
    //[Route("GetGatewayScopes")]
    //[ProducesResponseType((int)HttpStatusCode.NotFound)]
    //[ProducesResponseType((int)HttpStatusCode.BadRequest)]
    //[ProducesResponseType(typeof(GatewayScopeViewModel), (int)HttpStatusCode.OK)]
    //public async Task<ActionResult<GatewayScopeViewModel>> GetGatewayScopes([FromQuery] GetGatewayScopesQuery query)
    //{


    //  var items = await _mediator.Send(query);
    //  return Ok(items);
    //}

    #endregion

}

