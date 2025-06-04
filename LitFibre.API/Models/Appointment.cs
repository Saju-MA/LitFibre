namespace LitFibre.API.Models;

public class Appointment
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public AppointmentType Type { get; set; }
    public AppointmentStatus Status { get; set; }

    public required Slot Slot { get; set; }
}
