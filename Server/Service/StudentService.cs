using AutoMapper;
using FluentNHibernate.Conventions;
using Microsoft.AspNetCore.Http.HttpResults;
using NHibernate.Mapping.ByCode.Impl;
using ProtoBuf.Grpc;
using Server.DTO;
using Server.Entity;
using Server.Repository;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Service
{
    public class StudentService : IStudentContract
    {
        private readonly IStudentRepository studentRepo;
        private readonly IClassroomRepository classRepo;
        private readonly IMapper mapper;

        public StudentService(IStudentRepository _studentRepo, IClassroomRepository _classRepo, IMapper _mapper)
        {
            studentRepo = _studentRepo;
            classRepo = _classRepo;
            mapper = _mapper;
        }

        public async Task<OperationReply> AddStudentAsync(StudentProfile request, CallContext callContaxt = default)
        {
            OperationReply reply = new OperationReply();
            Students student = mapper.Map<Students>(request);
            await studentRepo.AddStudentAsync(student);
            reply.Success = true;

            return reply;
        }

        public async Task<OperationReply> DeleteStudentAsync(RequestId request, CallContext callContaxt = default)
        {
            OperationReply reply = new OperationReply();
            try
            {
                Students? student = await studentRepo.GetStudentByIdAsync(request.id);
                if (student != null)
                {
                    await studentRepo.DeleteStudentAsync(student);
                    reply.Success = true;
                    reply.Message = "Xóa sinh viên thành công!";
                }
                else
                {
                    reply.Success = false;
                    reply.Message = "Không tìm thấy sinh viên với ID đã nhập.";
                }
            }
            catch (Exception ex)
            {
                reply.Success = false;
                reply.Message = $"Lỗi khi xóa sinh viên: {ex.Message}";

            }
            return reply;
        }

        public async Task<MultipleStudentReply> GetAllStudentAsync(Empty request, CallContext callContaxt)
        {
            MultipleStudentReply listStudentReply = new MultipleStudentReply();
            try
            {
                List<Students>? students = await studentRepo.GetAllStudentAsync();
                if (students.Any() == true)
                {
                    listStudentReply.listStudents = mapper.Map<List<StudentProfile>>(students);
                    listStudentReply.Message = "Lấy danh sách sinh viên thành công!";
                }
                else
                {
                    listStudentReply.listStudents = new List<StudentProfile>();
                    listStudentReply.Message = "Không có sinh viên nào trong hệ thống.";
                }
            }
            catch (Exception ex)
            {
                listStudentReply.Message = $"Lỗi khi lấy danh sách sinh viên: {ex.Message}";
            }
            return listStudentReply;
        }

        public async Task<MultipleStudentReply> GetPaginationAsync(PaginationRequest request, CallContext callContext = default)
        {
            var reply = new MultipleStudentReply();
            try
            {
                SearchStudentDTO studentField = mapper.Map<SearchStudentDTO>(request);
                var searchResult = await studentRepo.GetPaginationAsync(studentField);
                reply.Count = searchResult.total;
                if (searchResult.listStudents == null || !searchResult.listStudents.Any())
                {
                    throw new Exception("There is no student");
                }
                reply.listStudents = mapper.Map<List<StudentProfile>>(searchResult.listStudents);
            }
            catch (Exception ex)
            {
                reply.Message = ex.Message;
            }

            return reply;
        }

        public async Task<MultipleStudentReply> GetSortStudentAsync(Empty request, CallContext callContaxt)
        {
                MultipleStudentReply listStudentReply = new MultipleStudentReply();
            try
            {
                List<Students>? students = await studentRepo.GetSortStudentAsync();
                if (students.Any() == true)
                {
                    listStudentReply.listStudents = mapper.Map<List<StudentProfile>>(students);
                    listStudentReply.Message = "Sắp xếp sinh viên thành công!";
                }
                else
                {
                    listStudentReply.listStudents = new List<StudentProfile>();
                    listStudentReply.Message = "Không có sinh viên nào trong hệ thống.";
                }
            }
            catch (Exception ex)
            {
                listStudentReply.Message = $"Lỗi khi lấy danh sách sinh viên: {ex.Message}";
            }
            return listStudentReply;
        }

        public async Task<StudentReply> GetStudentByIdAsync(RequestId request, CallContext callContaxt)
        {
            StudentReply reply = new StudentReply();
            try
            {
                Students students = await studentRepo.GetStudentByIdAsync(request.id);
                if (students != null)
                {
                    reply.Student = mapper.Map<StudentProfile>(students);
                }
                else
                {
                    return new StudentReply { Message = "Không tìm thấy sinh viên với ID đã nhập." };
                }
            }
            catch (Exception ex)
            {
                return new StudentReply { Message = $"Lỗi khi lấy thông tin sinh viên: {ex.Message}" };
            }
            return reply;
        }

        public async Task<OperationReply> UpdateStudentAsync(StudentProfile request, CallContext callContaxt)
        {
            OperationReply reply = new OperationReply();
            
            try
            {
                Classrooms? classroom = await classRepo.GetClassroomByIdAsync(request.classroomID);
                if (classroom == null)
                {
                    reply.Success = false;
                    reply.Message = "Không tìm thấy lớp với ID đã nhập.";
                    return reply;
                }

                Students? student = await studentRepo.GetStudentByIdAsync(request.id);

                student._name = request.studentName;
                student._birthday = request.studentBirthday;
                student._address = request.studentAddress;
                student._classrooms = classroom;

                await studentRepo.UpdateStudentAsync(student);

                reply.Success = true;

            } catch(Exception ex)
            {
                reply.Message = $"Lỗi khi cập nhật thông tin sinh viên: {ex.Message}";
            }
            return reply;
        }

        public async Task<MultipleStudentChart> GetStudentAgeChartAsync(RequestId request, CallContext callContext = default)
        {
            var chartData = await studentRepo.GetStudentAgesChartAsync(request.id);
            MultipleStudentChart studentAgeChart = new MultipleStudentChart
            {
                ChartData = mapper.Map<List<StudentChart>>(chartData)
            };

            return studentAgeChart;
        }

        public async Task<MultipleStudentChart> GetStudentCountAsync(RequestId request, CallContext callContext = default)
        {
            var chartData = await studentRepo.GetStudentCountChartAsync(request.id);
            MultipleStudentChart studentCountChart = new MultipleStudentChart
            {
                ChartData = mapper.Map<List<StudentChart>>(chartData)
            };

            return studentCountChart;
        }

        public async Task<MultipleStudentChart> GetStudentCountOfTeacherAsync(RequestId request, CallContext callContext = default)
        {
            var chartData = await studentRepo.GetStudentCountOfTeacherChartAsync(request.id);
            MultipleStudentChart studentCountOfTeacherChart = new MultipleStudentChart
            {
                ChartData = mapper.Map<List<StudentChart>>(chartData)
            };

            return studentCountOfTeacherChart;
        }

        public Task<MultipleStudentReply> ExportStudentsExcelAsync(PaginationRequest request, CallContext callContext = default)
        {
            throw new NotImplementedException();
        }
    }
}
