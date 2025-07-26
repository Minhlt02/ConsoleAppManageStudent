using AutoMapper;
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
    public class ClassroomService : IClassroomContract
    {
        private readonly IClassroomRepository classRepo;
        private readonly IMapper mapper;

        public ClassroomService(IClassroomRepository _classRepo, IMapper _mapper)
        {
            classRepo = _classRepo;
            mapper = _mapper;
        }
        public async Task<ListClassroomProfile> GetAllClassroomAsync(Empty empty, CallContext callContext = default)
        {
            ListClassroomProfile profile = new ListClassroomProfile();
            try
            {
                List<Classrooms>? classrooms = await classRepo.GetAllClassroomAsync();
                if (classrooms != null && classrooms.Count > 0)
                {
                    profile.ClassroomList = mapper.Map<List<ClassroomProfile>>(classrooms);
                    profile.Message = "Lấy danh sách sinh viên thành công!";
                }
                else
                {
                    profile.ClassroomList = new List<ClassroomProfile>();
                    profile.Message = "Không có sinh viên nào trong hệ thống.";
                }
            } catch(Exception ex)
            {
                profile.ClassroomList = new List<ClassroomProfile>();
                profile.Message = $"Lỗi khi lấy danh sách lớp học: {ex.Message}";
            }
            return profile;
        }
    }
}
