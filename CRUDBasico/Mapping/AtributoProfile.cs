using AutoMapper;
using CRUDBasico.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDBasico.Mapping
{
    public class AtributoProfile : Profile
    {
        public AtributoProfile()
        {
            CreateMap<Atributo, AtributoDto>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.atributoId))
                .ForMember(dest => dest.descripcion, opt => opt.MapFrom(src => src.descripcion))
                .ReverseMap();
        }
    }
}
