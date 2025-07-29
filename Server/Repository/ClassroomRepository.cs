using NHibernate;
using NHibernate.Linq;
using Server.Entity;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Repository
{
    public class ClassroomRepository : IClassroomRepository
    {
        private readonly ISession session;

        public ClassroomRepository(ISession _session)
        {
            session = _session;
        }
        public async Task<List<Classrooms>> GetAllClassroomAsync()
        {
            List<Classrooms> classrooms = await session.Query<Classrooms>()
                .Fetch(c => c.Teacher)
                .ToListAsync();
            return classrooms;
        }

        public async Task<Classrooms?> GetClassroomByIdAsync(int id)
        {
            Classrooms classroom = await session.Query<Classrooms>()
                .FirstOrDefaultAsync(c => c.Id == id);

            return classroom;
        }
    }
}
