using System.ComponentModel.DataAnnotations;

namespace LitFibre.API.Models.DBModels;

public class AppointmentDbModel
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public AppointmentType Type { get; set; }
    public AppointmentStatus Status { get; set; }

    public required SlotDbModel Slot { get; set; }
}
