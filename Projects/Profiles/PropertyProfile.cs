using AutoMapper;
using Projects.Entities;
using Projects.Models.Properties;

namespace Projects.Profiles;

public class PropertyProfile : Profile
{
    public PropertyProfile()
    {
        CreateMap<Property, PropertyModel>();
    }
}
