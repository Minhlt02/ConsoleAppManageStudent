﻿using AutoMapper;
using ProtoBuf.Grpc;
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
        private readonly IMapper mapper;

        public StudentService(IStudentRepository _studentRepo, IMapper _mapper)
        {
            studentRepo = _studentRepo;
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

        public async Task<ListStudentReply> GetAllStudentAsync(Empty request, CallContext callContaxt)
        {
            ListStudentReply listStudentReply = new ListStudentReply();
            try
            {
                List<Students>? students = await studentRepo.GetAllStudentAsync();
                if (students != null && students.Count > 0)
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

        public async Task<ListStudentReply> GetSortStudentAsync(Empty request, CallContext callContaxt)
        {
            ListStudentReply listStudentReply = new ListStudentReply();
            try
            {
                List<Students>? students = await studentRepo.GetSortStudentAsync();
                if (students != null && students.Count > 0)
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
                Students student = await studentRepo.GetStudentByIdAsync(request.studentID);
                if (student == null)
                {
                    reply.Success = false;
                    reply.Message = "Không tìm thấy sinh viên với ID đã nhập.";
                    return reply;
                }

                student._name = request.studentName;
                student._birthday = request.studentBirthday;
                student._address = request.studentAddress;

                await studentRepo.UpdateStudentAsync(student);

                reply.Success = true;

            } catch(Exception ex)
            {
                reply.Message = $"Lỗi khi cập nhật thông tin sinh viên: {ex.Message}";
            }
            return reply;
        }
    }
}
