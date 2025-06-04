using LitFibre.API.Models.DBModels;
using Microsoft.EntityFrameworkCore;

namespace LitFibre.API.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<AppointmentDbModel> Appointments { get; set; }
    public DbSet<SlotDbModel> Slots { get; set; }
}
