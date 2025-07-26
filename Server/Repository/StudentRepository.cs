using Server.DBHelper;
using Server.Entity;
using NHibernate;
using NHibernate.Linq;
using System;

namespace Server.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ISession session;

        public StudentRepository(ISession _session)
        {
            session = _session;
        }
        public async Task AddStudentAsync(Students students)
        {
            using (ITransaction tx = session.BeginTransaction())
            {
                var classroom = session.Get<Classrooms>(1);

                students._classrooms = classroom;
                await session.SaveAsync(students);
                await tx.CommitAsync();
             }
        }

        public async Task DeleteStudentAsync(Students student)
        {

            using (ITransaction tx = session.BeginTransaction())
            {
                await session.DeleteAsync(student);
                await tx.CommitAsync();
            }
        }

        public async Task<Students> GetStudentByIdAsync(int id)
        {
           Students students = await session.Query<Students>()
                        .Fetch(s => s._classrooms)
                        .ThenFetch(c => c._teacher)
                        .FirstOrDefaultAsync(s => s._id == id);
            return students;
        }

        public async Task<List<Students>> GetAllStudentAsync()
        {
            List<Students> students = await session.Query<Students>()
                        .Fetch(s => s._classrooms)
                        .ThenFetch(c => c._teacher)
                        .ToListAsync();
            return students;
        }

        public async Task<List<Students>> GetSortStudentAsync()
        {
            List<Students> students = await session.Query<Students>()
                        .OrderBy(s => s._name)
                        .ToListAsync();
            return students;
        }

        public async Task UpdateStudentAsync(Students students)
        {
            using (ITransaction tx = session.BeginTransaction())
            {
                await session.UpdateAsync(students);
                await tx.CommitAsync();
            }
        }

    }
}
