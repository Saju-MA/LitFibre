using LitFibre.API.Data;
using LitFibre.API.Models;
using Microsoft.EntityFrameworkCore;

namespace LitFibre.API.Services;

//IFT split into own interface
public interface ISlotService
{
    public Task<List<Slot>> GetAvailableSlots(AppointmentType type);
}

public class SlotService(AppDbContext context) : ISlotService
{
    public async Task<List<Slot>> GetAvailableSlots(AppointmentType type)
    {
        var availableSlots = new List<Slot>();
        var today = DateTime.Today;
        var thirtyDaysFromToday = today.AddDays(30).Date;

        var bookedSlots = await context.Slots
            .Where(s => 
                s.Start.Date > today 
                && s.Start.Date <= thirtyDaysFromToday 
                && s.Type == type)
            .ToListAsync();

        for (int i = 1; i <= 30; i++)
        {
            var date = today.AddDays(i);
            if(date.DayOfWeek != DayOfWeek.Saturday 
                && date.DayOfWeek != DayOfWeek.Sunday
                && !bookedSlots.Any(s => s.Start.Date.Equals(date)))
            {
                availableSlots.Add(
                    new Slot
                    {
                        Type = type,
                        Start = date.Date.AddHours(8),
                        End = date.Date.AddHours(12)
                    });

                availableSlots.Add(
                    new Slot
                    {
                        Type = type,
                        Start = date.Date.AddHours(13),
                        End = date.Date.AddHours(17)
                    });
            }
        }        

        return availableSlots;
    }
}
