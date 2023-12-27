using AutoMapper;
using Okai.Boilerplate.Application.Commands.Example;
using Okai.Boilerplate.Application.DTOs;

namespace Okai.Boilerplate.Application.Mappings
{
    public class ExampleMappings : Profile
    {
        public ExampleMappings() 
        {
            CreateMap<ExampleDto, ExampleCommand>().ReverseMap();
        }
    }
}
