using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BlazorClient.DTO;
using Shared;

namespace BlazorClient.Mapper
{
    public class ClassroomMapper : Profile
    {
        public ClassroomMapper() 
        {
            CreateMap<ClassroomProfile, ClassroomDTO>();
            CreateMap<ClassroomDTO, ClassroomProfile>();
        }
    }
}
