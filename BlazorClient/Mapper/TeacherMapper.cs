using AutoMapper;
using BlazorClient.DTO;
using Shared;

namespace BlazorClient.Mapper
{
    public class TeacherMapper : Profile
    {
        public TeacherMapper()
        {
            CreateMap<TeacherProfile, TeacherDTO>();
            CreateMap<TeacherDTO, TeacherProfile>();
        }
    }
}
