using AutoMapper;
using Server.Entity;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Mapper
{
    public class TeacherMapper : Profile
    {
        public TeacherMapper()
        {
            CreateMap<Teachers, TeacherProfile>();
        }

    }
}
