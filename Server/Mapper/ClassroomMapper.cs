using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Server.Entity;
using Shared;

namespace Server.Mapper
{
    public class ClassroomMapper : Profile
    {
        public ClassroomMapper() 
        {
            CreateMap<ClassroomProfile, Classrooms>();
        }
    }
}
