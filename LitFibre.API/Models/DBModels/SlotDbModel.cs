using System.ComponentModel.DataAnnotations;

namespace LitFibre.API.Models.DBModels;

public class SlotDbModel
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public required AppointmentType Type { get; set; }

    public required DateTime Start { get; set; }

    public required DateTime End { get; set; }
}
