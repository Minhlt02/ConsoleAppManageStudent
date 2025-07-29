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
    public class TeacherService : ITeacherContract
    {
        private readonly ITeacherRepository teacherRepo;
        private readonly IMapper mapper;

        public TeacherService(ITeacherRepository _teacherRepo, IMapper _mapper)
        {
            teacherRepo = _teacherRepo;
            mapper = _mapper;
        }
        public async Task<MultipleTeacherProfile> GetAllTeacherAsync(Empty empty, CallContext callContext = default)
        {
            MultipleTeacherProfile profile = new MultipleTeacherProfile();
            try
            {
                List<Teachers>? teachers = await teacherRepo.GetAllTeacherAsync();
                if (teachers == null)
                {
                    throw new Exception("There is no teachers in database");
                }

                profile.Count = teachers.Count;
                profile.TeacherList = mapper.Map<List<TeacherProfile>>(teachers);

            }
            catch (Exception ex)
            {
                profile.TeacherList = new List<TeacherProfile>();
                profile.Message = $"Lỗi khi lấy danh sách lớp học: {ex.Message}";
            }
            return profile;
        }
    }
}
