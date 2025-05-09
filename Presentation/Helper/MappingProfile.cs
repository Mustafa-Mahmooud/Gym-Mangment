using AutoMapper;
using Core.Entites;
using Presentation.DTOS;

namespace Presentation.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Member, MemberDTO>().ReverseMap();
            
        }
    }
}
