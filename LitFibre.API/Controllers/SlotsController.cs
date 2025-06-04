using LitFibre.API.Models;
using LitFibre.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace LitFibre.API.Controllers;

public class SlotsController(ISlotService slotService) : ControllerBase
{
    [HttpGet]
    [Route("{type}")]
    [ProducesResponseType(typeof(SlotArray), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAvailableSlots(string type)
    {
        if (!Enum.TryParse<AppointmentType>(type, true, out var appointmentType))
        {
            return NotFound();
        }

        var slots = await slotService.GetAvailableSlots(appointmentType);

        return Ok(slots);
    }
}
