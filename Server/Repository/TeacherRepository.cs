using NHibernate;
using NHibernate.Linq;
using Server.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Repository
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly ISession session;

        public TeacherRepository(ISession _session)
        {
            session = _session;
        }
        public async Task<List<Teachers>> GetAllTeacherAsync()
        {
            List<Teachers> teachers = await session.Query<Teachers>().ToListAsync();
            return teachers;
        }

        public async Task<Teachers?> GetTeachersByIdAsync(int id)
        {
            Teachers teacher = await session.Query<Teachers>()
                .FirstOrDefaultAsync(c => c.Id == id);

            return teacher;
        }
    }
}
