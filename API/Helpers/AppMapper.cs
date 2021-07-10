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
            CreateMap<Tip, TipDetailsDto>();
            CreateMap<FactorDto, Factor>();

            CreateMap<Tip, TipDto>();

            CreateMap<TipDetailsDto, Tip>();

            CreateMap<Feedback, FeedbackInfoDto>();

            CreateMap<EditUserDto, User>();

            CreateMap<User, UserDetailsDto>();

            CreateMap<User, UserQuickInfoDto>();

            CreateMap<User, WorkerDto>();

            CreateMap<User, User>();

            CreateMap<User, UserDto>();
            CreateMap<Tip, TipDetailsDto>();
            CreateMap<Tip, TipDetailsDto>();

            CreateMap<UserConditionDto, Tip>();

            CreateMap<CreateTipDto, Tip>();

            CreateMap<Tip, Tip>();
               
            CreateMap<RegisterDto, User>()
                .ForMember(p => p.UserName, s => s
                .MapFrom(x => x.Email));

            CreateMap<CreateFactorDto, Factor>()
                .ForMember(p => p.FactorTypeId, s => s
                .MapFrom( x => x.FactorType));

            CreateMap<UserCharacteristic, UserCharacteristicsDto>();

        }
    }
}
