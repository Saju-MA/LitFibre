namespace LitFibre.API.Models;

public class SlotArray : List<Slot>
{
    public SlotArray() : base() { }

    public SlotArray(IEnumerable<Slot> slots) : base(slots) { }
}
