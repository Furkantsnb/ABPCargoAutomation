using AutoMapper;
using CargoAutomation.Agentas;
using CargoAutomation.Lines;
using CargoAutomation.Stations;
using CargoAutomation.TransferCenter;

using Entities.Dtos.Agentas;
using Entities.Dtos.Lines;
using Entities.Dtos.Stations;
using Entities.Dtos.TransferCenter;




namespace CargoAutomation;

public class CargoAutomationApplicationAutoMapperProfile : Profile
{
    public CargoAutomationApplicationAutoMapperProfile()
    {
        CreateMap<Agenta, AgentaDto>().ReverseMap();
        CreateMap<Agenta, UpdateAgentaDto>().ReverseMap();
        CreateMap<Agenta, CreateAgentaDto>().ReverseMap();

        CreateMap<Line, CreateLineDto>().ReverseMap();
        CreateMap<Line, UpdateLineDto>().ReverseMap();
        CreateMap<Line, LineDto>().ReverseMap();

        CreateMap<Station, CreateStationDto>().ReverseMap();
        CreateMap<Station, UpdateStationDto>().ReverseMap();
        CreateMap<Station, StationDto>().ReverseMap();

  //      CreateMap<TransferCenter, TransferCenterDto>().ReverseMap();


    

        CreateMap<CreateLineDto, Line>()
      .ForMember(dest => dest.Stations, opt => opt.Ignore());

        CreateMap<UpdateLineDto, Line>()
    .ForMember(dest => dest.Stations, opt => opt.Ignore());
    }
}
