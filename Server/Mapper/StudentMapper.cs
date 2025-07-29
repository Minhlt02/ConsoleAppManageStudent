using AutoMapper;
using Server.DTO;
using Server.Entity;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Mapper
{
    public class StudentMapper : Profile
    {
        public StudentMapper()
        {
            CreateMap<StudentProfile, Students>()
                .ForMember(dest => dest._id, opt => opt.MapFrom(src => src.id))
                .ForMember(dest => dest._name, opt => opt.MapFrom(src => src.studentName))
                .ForMember(dest => dest._birthday, opt => opt.MapFrom(src => src.studentBirthday))
                .ForMember(dest => dest._address, opt => opt.MapFrom(src => src.studentAddress))
                .ForPath(dest => dest._classrooms.Id, opt => opt.MapFrom(src => src.classroomID))
                .ForPath(dest => dest._classrooms.NameClassroom, opt => opt.MapFrom(src => src.classroomName))
                .ForPath(dest => dest._classrooms.NameSubject, opt => opt.MapFrom(src => src.subjectName))
                .ForPath(dest => dest._classrooms.Teacher._id, opt => opt.MapFrom(src => src.teacherID))
                .ForPath(dest => dest._classrooms.Teacher._nameTeacher, opt => opt.MapFrom(src => src.teacherName))
                .ForPath(dest => dest._classrooms.Teacher._birthdayTeacher, opt => opt.MapFrom(src => src.teacherBirthday));

            CreateMap<Students, StudentProfile>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src._id))
                .ForMember(dest => dest.studentName, opt => opt.MapFrom(src => src._name))
                .ForMember(dest => dest.studentBirthday, opt => opt.MapFrom(src => src._birthday))
                .ForMember(dest => dest.studentAddress, opt => opt.MapFrom(src => src._address))
                .ForMember(dest => dest.classroomID, opt => opt.MapFrom(src => src._classrooms.Id))
                .ForMember(dest => dest.classroomName, opt => opt.MapFrom(src => src._classrooms.NameClassroom))
                .ForMember(dest => dest.subjectName, opt => opt.MapFrom(src => src._classrooms.NameSubject))
                .ForMember(dest => dest.teacherID, opt => opt.MapFrom(src => src._classrooms.Teacher._id))
                .ForMember(dest => dest.teacherName, opt => opt.MapFrom(src => src._classrooms.Teacher._nameTeacher))
                .ForMember(dest => dest.teacherBirthday, opt => opt.MapFrom(src => src._classrooms.Teacher._birthdayTeacher));

            CreateMap<PaginationRequest, SearchStudentDTO>();

            CreateMap<SearchStudentDTO, PaginationRequest>()
                .ForMember(dest => dest.PageNumber, opt => opt.Ignore())
                .ForMember(dest => dest.PageSize, opt => opt.Ignore())

                .ForMember(dest => dest.SortBy, opt => opt.MapFrom(s => s.sortBy)); ;


            CreateMap<StudentAgeDTO, StudentAge>() ;
            CreateMap<Students, StudentAgeDTO>()
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => DateTime.Now.Year - src._birthday.Year))
                .ForMember(dest => dest.Count, opt => opt.MapFrom(src => 1));

        }
    }
}
