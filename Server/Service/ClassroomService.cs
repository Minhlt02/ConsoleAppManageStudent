using AutoMapper;
using NHibernate.Mapping.ByCode.Impl;
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
        public async Task<MultipleClassroomProfile> GetAllClassroomAsync(Empty empty, CallContext callContext = default)
        {
            MultipleClassroomProfile profile = new MultipleClassroomProfile();
            try
            {
                List<Classrooms>? classrooms = await classRepo.GetAllClassroomAsync();
                if (classrooms == null)
                {
                    throw new Exception("There is no classes in database");
                }

                profile.Count = classrooms.Count;
                profile.ClassroomList = mapper.Map<List<ClassroomProfile>>(classrooms);
    
            } catch(Exception ex)
            {
                profile.ClassroomList = new List<ClassroomProfile>();
                profile.Message = $"Lỗi khi lấy danh sách lớp học: {ex.Message}";
            }
            return profile;
        }
    }
}
