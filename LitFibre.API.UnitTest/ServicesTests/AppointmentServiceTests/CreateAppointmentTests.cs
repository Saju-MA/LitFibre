﻿using Moq;
using FluentAssertions;
using LitFibre.API.Models;
using LitFibre.API.Models.DBModels;

namespace LitFibre.API.UnitTest.ServicesTests.AppointmentServiceTests;

public class CreateAppointmentTests : AppointmentServiceTestBase
{
    [Fact]
    public async Task CreateAppointment_WhenSuccesful_ShouldReturnAppointment()
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
        var appointment = new AppointmentDbModel
        {
            Id = Guid.NewGuid().ToString(),
            Type = AppointmentType.Installation,
            Slot = new SlotDbModel
            {
                Id = Guid.NewGuid().ToString(),
                Type = AppointmentType.Installation,
                Start = DateTime.Today.AddDays(1).AddHours(8),
                End = DateTime.Today.AddDays(1).AddHours(12)
            }
        };
        _mapper.Setup(m => m.Map<AppointmentDbModel>(It.IsAny<Appointment>()))
            .Returns(appointment);

        // Act
        var result = await _service.CreateAppointment(request);

        // Assert
        result!.Slot.Should().BeEquivalentTo(request.Slot);
    }

    [Fact]
    public async Task CreateAppointment_WhenSlotExist_ShouldReturnNull()
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
        var slot = new SlotDbModel
        {
            Id = Guid.NewGuid().ToString(),
            Type = AppointmentType.Installation,
            Start = DateTime.Today.AddDays(1).AddHours(8),
            End = DateTime.Today.AddDays(1).AddHours(12)
        };

        _context.Slots.Add(slot);
        await _context.SaveChangesAsync();

        // Act
        var result = await _service.CreateAppointment(request);

        // Assert
        result.Should().BeNull();
    }
}
