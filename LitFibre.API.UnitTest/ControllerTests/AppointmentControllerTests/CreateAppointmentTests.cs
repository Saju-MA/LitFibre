using LitFibre.API.Models;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using Moq;

namespace LitFibre.API.UnitTest.ControllerTests.AppointmentControllerTests;

public class CreateAppointmentTests : AppointmentControllerTestBase
{
    [Fact]
    public async Task CreateAppointment_WhenSuccesful_ShouldReturnCreated()
    {
        // Arranggge
        var request = new AppointmentRequest
        {
            Type = AppointmentType.Installation,
            Slot = new Slot
            {
                Type = AppointmentType.Installation,
                Start = DateTime.Today.AddDays(1).AddHours(8),
                End = DateTime.Today.AddDays(1).AddHours(12)
            }
        };
        var appointment = new Appointment
        {
            Id = Guid.NewGuid().ToString(),
            Type = AppointmentType.Installation,
            Slot = new Slot
            {
                Type = AppointmentType.Installation,
                Start = DateTime.Today.AddDays(1).AddHours(8),
                End = DateTime.Today.AddDays(1).AddHours(12)
            }
        };
        _service.Setup(s => s.CreateAppointment(request))
            .ReturnsAsync(appointment);

        // Act
        var result = await _controller.CreateAppointment(request);

        // Assert
        var createdResult = result.Should().BeOfType<CreatedAtActionResult>().Subject;
        createdResult.ActionName.Should().Be(nameof(_controller.GetAppointment));
        if (createdResult.RouteValues != null)
        {
            createdResult.RouteValues.Should().ContainKey("id");
            createdResult.RouteValues["id"].Should().Be(appointment.Id);
        }
        createdResult.Value.Should().Be(appointment);
    }

    [Fact]
    public async Task CreateAppointment_WhenCreationFails_ShouldReturnBadReques()
    {
        // Arranggge
        var request = new AppointmentRequest
        {
            Type = AppointmentType.Installation,
            Slot = new Slot
            {
                Type = AppointmentType.Installation,
                Start = DateTime.Today.AddDays(1).AddHours(8),
                End = DateTime.Today.AddDays(1).AddHours(12)
            }
        };
        _service.Setup(s => s.CreateAppointment(request))
            .ReturnsAsync((Appointment?)null);

        // Act
        var result = await _controller.CreateAppointment(request);

        // Assert
        result.Should().BeOfType<BadRequestObjectResult>();
    }
}
