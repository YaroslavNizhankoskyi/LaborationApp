using API.DTO;
using API.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helpers
{
    public class AppMapper : Profile
    {

        public AppMapper()
        {
            CreateMap<UserDto, User>();

            CreateMap<CreateFactorDto, Factor>()
                .ForMember(p => p.FactorTypeId, s => s
                .MapFrom( x => x.FactorType));
        }
    }
}
