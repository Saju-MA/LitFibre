using Moq;
using FluentAssertions;
using LitFibre.API.Models;
using LitFibre.API.Models.DBModels;

namespace LitFibre.API.UnitTest.ServicesTests.AppointmentServiceTests;

public class CreateAppointmentTestsFixed : AppointmentServiceTestBase
{
    [Fact]
    public async Task CreateAppointment_WhenSuccesful_ShouldReturnAppointment_Fixed()
    {
        // Arranggge
        var request = new AppointmentRequest
        {
            Type = AppointmentType.Installation,
            Slot = new Slot
            {
                Type = AppointmentType.Installation,
                Start = DateTime.Today.AddDays(1).AddHours(13),
                End = DateTime.Today.AddDays(1).AddHours(17)
            }
        };
        var appointment = new AppointmentDbModel
        {
            Id = Guid.NewGuid().ToString(),
            Type = AppointmentType.Installation,
            Slot = new SlotDbModel
            {
                Id = Guid.NewGuid().ToString(),
                Type = AppointmentType.Installation,
                Start = DateTime.Today.AddDays(1).AddHours(13),
                End = DateTime.Today.AddDays(1).AddHours(17)
            }
        };
        _mapper.Setup(m => m.Map<AppointmentDbModel>(It.IsAny<Appointment>()))
            .Returns(appointment);

        // Act
        var result = await _service.CreateAppointment(request);

        // Assert
        result!.Slot.Should().BeEquivalentTo(request.Slot);
    }
}
