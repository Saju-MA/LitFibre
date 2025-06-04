using LitFibre.API.Models;
using LitFibre.API.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace LitFibre.API.Controllers;

public class AppointmentsController(IAppointmentService appointmentService) : ControllerBase
{
    [HttpPost]
    [Route("/appointment")]
    [ProducesResponseType(typeof(Appointment), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(List<ValidationError>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAppointment([FromBody] AppointmentRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(GetValidationErrors(ModelState));
        }

        var appointment = await appointmentService.CreateAppointment(request);

        if (appointment == null)
        {
            return BadRequest(
            new List<ValidationError>
            {
                new()
                {
                    Field = "Request Slot",
                    Messagge = "Invalid request Slot. Check if the Slot is available"
                }
            });
        }

        return CreatedAtAction(nameof(GetAppointment), new { id = appointment.Id }, appointment);
    }

    [HttpPost]
    [Route("/appointment/{id}")]
    [ProducesResponseType(typeof(Appointment), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAppointment(Guid id)
    {
        var appointment = await appointmentService.RetrieveAppointment(id);

        if (appointment == null)
        {
            return NotFound();
        }

        return Ok(appointment);
    }

    [HttpPut]
    [Route("/appointment/{id}")]
    [ProducesResponseType(typeof(Appointment), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(List<ValidationError>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAppointment(Guid id, [FromBody] JObject request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(GetValidationErrors(ModelState));
        }

        var slotRequest = request.ToObject<UpdateSlotRequest>();
        if (slotRequest != null && request.ContainsKey("status"))
        { 
            var updatedAppointment = await appointmentService.UpdateAppointment(id, slotRequest);
            if(updatedAppointment == null)
            {
                return NotFound();
            }
            return Ok(updatedAppointment);
        }

        var statusRequest = request.ToObject<UpdateStatusRequest>();
        if (statusRequest != null && request.ContainsKey("status"))
        {
            var updatedAppointment = await appointmentService.UpdateAppointment(id, statusRequest);
            if (updatedAppointment == null)
            {
                return NotFound();
            }
            return Ok(updatedAppointment);
        }

        return BadRequest(
            new List<ValidationError>
            {
                new() 
                {
                    Field = "Request Body",
                    Messagge = "Invalid request body format. Must be an UpdateSlotRequest or an UpdateStatusRequest"
                }
            });
    }

    [HttpDelete]
    [Route("/appointment/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAppointment(Guid id)
    {
        var success = await appointmentService.CancelAppointment(id);
        if (success)
        {
            return NoContent();
        }
        return NotFound();
    }

    private static List<ValidationError> GetValidationErrors(Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary modelState)
    {
        var errors = new List<ValidationError>();
        foreach (var state in modelState)
        {
            foreach(var error in state.Value.Errors)
            {
                errors.Add(
                    new ValidationError
                    {
                        Field = state.Key,
                        Messagge = error.ErrorMessage
                    });
            }
        }
        return errors;
    }
}
