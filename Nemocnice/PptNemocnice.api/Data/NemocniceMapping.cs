using AutoMapper;
using PptNemocnice.Shared;
namespace PptNemocnice.api.Data;

public class NemocniceMapping : Profile         //od AutoMapperu
{
    public NemocniceMapping()
    {
        CreateMap<Vybaveni, VybaveniModel>().ReverseMap();
        CreateMap<Revize, RevizeModel>().ReverseMap();
        CreateMap<Vybaveni, VybaveniSRevizesModel>();
        CreateMap<Ukon, UkonModel>().ReverseMap();
    }
}
