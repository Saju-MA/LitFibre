namespace LitFibre.API.Models;

public class UpdateStatusRequest
{
    public required AppointmentStatus Status { get; set; }
}
