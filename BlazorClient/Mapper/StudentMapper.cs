using AutoMapper;
using BlazorClient.DTO;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorClient.Mapper
{
    public class StudentMapper : Profile
    {
        public StudentMapper()
        {
            CreateMap<StudentProfile, StudentDTO>();

            CreateMap<StudentDTO, StudentProfile>();

            CreateMap<PaginationRequest, SearchStudentDTO>();

            CreateMap<SearchStudentDTO, PaginationRequest>()
                .ForMember(dest => dest.PageSize, opt => opt.Ignore())
                .ForMember(dest => dest.PageNumber, opt => opt.Ignore());
        } 
    }
}
