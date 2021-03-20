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
            CreateMap<FeedbackInfoDto, Feedback>();

            CreateMap<EditUserDto, User>();

            CreateMap<UserDetailsDto, User>();

            CreateMap<UserQuickInfoDto, User>();

            CreateMap<WorkerDto, User>();

            CreateMap<UserDto, User>();

            CreateMap<UserConditionDto, Tip>();

            CreateMap<CreateTipDto, Tip>();

            CreateMap<TipCalculator, Tip>();

            CreateMap<TipSearch, Tip>();

            CreateMap<Tip, Tip>();
               
            CreateMap<RegisterDto, User>()
                .ForMember(p => p.UserName, s => s
                .MapFrom(x => x.Email));

            CreateMap<CreateFactorDto, Factor>()
                .ForMember(p => p.FactorTypeId, s => s
                .MapFrom( x => x.FactorType));
        }
    }
}
