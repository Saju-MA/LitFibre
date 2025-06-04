using AutoMapper;
using LitFibre.API.Data;
using LitFibre.API.Models;
using LitFibre.API.Models.DBModels;
using Microsoft.EntityFrameworkCore;

namespace LitFibre.API.Services;

public interface IAppointmentService
{
    public Task<Appointment?> CreateAppointment(AppointmentRequest request);
    public Task<Appointment?> RetrieveAppointment(Guid id);
    public Task<Appointment?> UpdateAppointment(Guid id, UpdateSlotRequest request);
    public Task<Appointment?> UpdateAppointment(Guid id, UpdateStatusRequest request);
    public Task<bool> CancelAppointment(Guid id);
}
public class AppointmentService(AppDbContext context, IMapper mapper) : IAppointmentService
{

    public async Task<Appointment?> CreateAppointment(AppointmentRequest request)
    {

        var slot = new Slot
        {
            Type = request.Slot.Type,
            Start = request.Slot.Start,
            End = request.Slot.End
        };

        if (!await IsValidSlot(slot))
            return null;

        var appointment = new Appointment
        {
            Type = request.Type,
            Slot = slot
        };

        context.Appointments.Add(mapper.Map<AppointmentDbModel>(appointment));

        await context.SaveChangesAsync();
        return appointment;
    }

    public async Task<Appointment?> RetrieveAppointment(Guid id)
    {
        var appointment = await context.Appointments
            .Include(a => a.Slot)
            .FirstOrDefaultAsync(a => a.Id == id.ToString());

        return mapper.Map<Appointment>(appointment);
    }

    public async Task<Appointment?> UpdateAppointment(Guid id, UpdateSlotRequest request)
    {
        var appointment = await context.Appointments
            .Include(a => a.Slot)
            .FirstOrDefaultAsync(a => a.Id == id.ToString());
        if (appointment == null) return null;

        appointment.Slot.Start = request.Slot.Start;
        appointment.Slot.End = request.Slot.End;

        await context.SaveChangesAsync();
        return mapper.Map<Appointment>(appointment);
    }

    public async Task<Appointment?> UpdateAppointment(Guid id, UpdateStatusRequest request)
    {
        var appointment = await context.Appointments
            .Include(a => a.Slot)
            .FirstOrDefaultAsync(a => a.Id == id.ToString());
        if (appointment == null) return null;

        appointment.Status = request.Status;

        await context.SaveChangesAsync();
        return mapper.Map<Appointment>(appointment);
    }

    public async Task<bool> CancelAppointment(Guid id)
    {
        var appointment = await context.Appointments.FindAsync(id);
        if (appointment == null) return false;

        context.Appointments.Remove(appointment);

        await context.SaveChangesAsync();
        return true;
    }

    private async Task<bool> IsValidSlot(Slot slot)
    {
        bool isMorningSlot = (slot.Start.Hour == 8 && slot.End.Hour == 12);
        bool isAfternoonSlot = (slot.Start.Hour == 13 && slot.End.Hour == 17);

        if (!(isMorningSlot || isAfternoonSlot)
            || slot.Start.Date != slot.End.Date
            || slot.Start.DayOfWeek == DayOfWeek.Saturday
            || slot.Start.DayOfWeek == DayOfWeek.Sunday
            || slot.Start.Date <= DateTime.Today.Date
            || slot.Start.Date > DateTime.Today.AddDays(30).Date)
            return false;
        
        var checkSlot = await context.Slots
            .FirstOrDefaultAsync(s => s.Start.Equals(slot.Start) && s.Type == slot.Type);

        if(checkSlot != null)
            return false;

        return true;
    }
}
