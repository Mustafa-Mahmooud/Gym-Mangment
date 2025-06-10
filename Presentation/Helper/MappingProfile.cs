using AutoMapper;
using Core.Entites;
using Core.Entites.Identity;
using Presentation.DTOS;

namespace Presentation.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Member, MemberDTO>().ReverseMap();
            CreateMap<Trainers, TrainersDTO>().ReverseMap();

            CreateMap<AppUser,UserDto>().ReverseMap();

            
        }
    }
}
