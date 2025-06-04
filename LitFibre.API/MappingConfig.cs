using AutoMapper;
using LitFibre.API.Models;
using LitFibre.API.Models.DBModels;

namespace LitFibre.API;

public class MappingConfig
{
    public static MapperConfiguration Configuration()
    {
        var mappingConfig = new MapperConfiguration(config =>
        {
            config.CreateMap<AppointmentDbModel, Appointment>();
            config.CreateMap<Appointment, AppointmentDbModel>();

            config.CreateMap<SlotDbModel, Slot>();
            config.CreateMap<Slot, SlotDbModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
        });
        return mappingConfig;
    }
}
