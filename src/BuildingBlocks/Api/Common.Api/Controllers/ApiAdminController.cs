namespace Fintranet.BuildingBlocks.Common.Api.Controllers;

[ApiController]
[Route("api/admin/v1/[controller]")]
public class ApiAdminController: ControllerBase
{
    /// <summary>
    /// Creates a <see cref="CreatedResult"/> object that produces a <see cref="StatusCodes.Status201Created"/> response.
    /// </summary>
    /// <returns>The created <see cref="CreatedResult"/> for the response.</returns>
    [NonAction]
    public virtual StatusCodeResult Created()
    {
        return StatusCode(201);
    }

    /// <summary>
    /// Updated a <see cref="CreatedResult"/> object that produces a <see cref="StatusCodes.Status201Created"/> response.
    /// </summary>
    /// <returns>The created <see cref="StatusCode(204)"/> for the response.</returns>
    [NonAction]
    public virtual StatusCodeResult Updated()
    {
        return StatusCode(204);
    }

    /// <summary>
    /// Updated a <see cref="CreatedResult"/> object that produces a <see cref="StatusCodes.Status201Created"/> response.
    /// </summary>
    /// <returns>The created <see cref="StatusCode(204)"/> for the response.</returns>
    [NonAction]
    public virtual ObjectResult Updated(object updated)
    {
        if (updated == null)
        {
            throw new ArgumentNullException(nameof(updated));
        }
        ObjectResult result = new(updated)
        {
            StatusCode = StatusCodes.Status200OK
        };
        return result;
    }

    /// <summary>
    /// Deletes a <see cref="CreatedResult"/> object that produces a <see cref="StatusCodes.Status201Created"/> response.
    /// </summary>
    /// <returns>The created <see cref="CreatedResult"/> for the response.</returns>
    [NonAction]
    public virtual StatusCodeResult Deleted()
    {
        return StatusCode(StatusCodes.Status204NoContent);
    }
}
