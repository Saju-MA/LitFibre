namespace LitFibre.API.Models;

public class AppointmentRequest
{
    public required AppointmentType Type { get; set; }

    public required Slot Slot { get; set; }
}
