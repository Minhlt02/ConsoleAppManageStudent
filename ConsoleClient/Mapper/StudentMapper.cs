using AutoMapper;
using ConsoleClient.Entity;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClient.Mapper
{
    public class StudentMapper : Profile
    {
        public StudentMapper()
        {
            CreateMap<StudentProfile, Students>();
            CreateMap<Students, StudentProfile>();
        }
    }
}
