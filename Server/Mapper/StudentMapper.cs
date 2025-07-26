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
    public class StudentMapper : Profile
    {
        public StudentMapper()
        {
            CreateMap<StudentProfile, Students>()
             .ForMember(dest => dest._idStudent, opt => opt.MapFrom(src => src.studentID))
                .ForMember(dest => dest._name, opt => opt.MapFrom(src => src.studentName))
                .ForMember(dest => dest._birthday, opt => opt.MapFrom(src => src.studentBirthday))
                .ForMember(dest => dest._address, opt => opt.MapFrom(src => src.studentAddress))
                .ForPath(dest => dest._classrooms._idClassroom, opt => opt.MapFrom(src => src.classroomID))
                .ForPath(dest => dest._classrooms._nameClassroom, opt => opt.MapFrom(src => src.classroomName))
                .ForPath(dest => dest._classrooms._nameSubject, opt => opt.MapFrom(src => src.subjectName))
                .ForPath(dest => dest._classrooms._teacher._idTeacher, opt => opt.MapFrom(src => src.teacherID))
                .ForPath(dest => dest._classrooms._teacher._nameTeacher, opt => opt.MapFrom(src => src.teacherName))
                .ForPath(dest => dest._classrooms._teacher._birthdayTeacher, opt => opt.MapFrom(src => src.teacherBirthday));

            CreateMap<Students, StudentProfile>()
                .ForMember(dest => dest.studentID, opt => opt.MapFrom(src => src._idStudent))
                .ForMember(dest => dest.studentName, opt => opt.MapFrom(src => src._name))
                .ForMember(dest => dest.studentBirthday, opt => opt.MapFrom(src => src._birthday))
                .ForMember(dest => dest.studentAddress, opt => opt.MapFrom(src => src._address))
                .ForMember(dest => dest.classroomID, opt => opt.MapFrom(src => src._classrooms._idClassroom))
                .ForMember(dest => dest.classroomName, opt => opt.MapFrom(src => src._classrooms._nameClassroom))
                .ForMember(dest => dest.subjectName, opt => opt.MapFrom(src => src._classrooms._nameSubject))
                .ForMember(dest => dest.teacherID, opt => opt.MapFrom(src => src._classrooms._teacher._idTeacher))
                .ForMember(dest => dest.teacherName, opt => opt.MapFrom(src => src._classrooms._teacher._nameTeacher))
                .ForMember(dest => dest.teacherBirthday, opt => opt.MapFrom(src => src._classrooms._teacher._birthdayTeacher));
        } 
    }
}
