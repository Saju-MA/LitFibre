using LitFibre.API.Models;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using Moq;

namespace LitFibre.API.UnitTest.ControllerTests.AppointmentControllerTests;

public class GetAppointmentTests : AppointmentControllerTestBase
{
    [Fact]
    public async Task GgetAppointment_WhenSuccesful_ShouldReturnOk()
    {
        // Arranggge
        var id = Guid.NewGuid();
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
        _service.Setup(s => s.RetrieveAppointment(id))
            .ReturnsAsync(appointment);

        // Act
        var result = await _controller.GetAppointment(id);

        // Assert
        result.Should().BeOfType<OkObjectResult>()
            .Which.Value.Should().Be(appointment);
    }

    [Fact]
    public async Task GgetAppointment_WhenUnSuccesful_ShouldReturnNotFound()
    {
        // Arranggge
        var id = Guid.NewGuid();
        _service.Setup(s => s.RetrieveAppointment(id))
            .ReturnsAsync((Appointment?)null);

        // Act
        var result = await _controller.GetAppointment(id);

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }
}
