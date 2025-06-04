namespace LitFibre.API.Models;

public class Slot
{
    public required AppointmentType Type { get; set; }

    public required DateTime Start { get; set; }

    public required DateTime End { get; set; }
}
