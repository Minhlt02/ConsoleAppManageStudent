using NHibernate;
using NHibernate.Linq;
using Server.DBHelper;
using Server.DTO;
using Server.Entity;
using Shared;
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
                        .FirstOrDefaultAsync(s => s._studentCode == id);
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

        public async Task<int> CountAsync()
        {
            int count = await session.Query<Students>().CountAsync();

            return count;
        }

        public async Task<PageViewDTO<Students>> GetPaginationAsync(SearchStudentDTO searchStudent)
        {
            int pageSkip = (searchStudent.pageNumber - 1) * searchStudent.pageSize;
            var query = session.Query<Students>()
                            .Fetch(s => s._classrooms)
                            .ThenFetch(c => c._teacher).AsQueryable();
            query = Filter(query, searchStudent);
            var result = new PageViewDTO<Students>
            {
                total = await query.CountAsync(),
                listStudents = await query!.Skip(pageSkip).Take(searchStudent.pageSize).ToListAsync()
            };
            return result;
        }

        public async Task<PageViewDTO<Students>> GetPaginationSortAsync(SearchStudentDTO searchStudent)
        {
            int pageSkip = (searchStudent.pageNumber - 1) * searchStudent.pageSize;
            var query = session.Query<Students>()
                            .Fetch(s => s._classrooms)
                            .ThenFetch(c => c._teacher).AsQueryable();
            query = FilterSort(query, searchStudent);
            var result = new PageViewDTO<Students>
            {
                total = await query.CountAsync(),
                listStudents = await query!.Skip(pageSkip).Take(searchStudent.pageSize).ToListAsync()
            };
            return result;
        }

        private IQueryable<Students>? Filter(IQueryable<Students> query, SearchStudentDTO studentSearch)
        {
            if (studentSearch.Id.HasValue)
            {
                query = query.Where(student => student._id == studentSearch.Id.Value);
            }
            if (studentSearch.studentCode.HasValue)
            {
                query = query.Where(student => student._studentCode == studentSearch.studentCode.Value);
            }
            if (!string.IsNullOrWhiteSpace(studentSearch.studentName))
            {
                query = query.Where(s => s._name.Contains(studentSearch.studentName));
            }
            if (!string.IsNullOrEmpty(studentSearch.studentAddress))
            {   
                query = query.Where(s => s._address.Contains(studentSearch.studentAddress));
            }
            if (studentSearch.studentBirthday.HasValue)
            {
                query = query.Where(student => student._birthday.Date >= studentSearch.studentBirthday.Value.Date);
            }
            if (studentSearch.classroomId.HasValue)
            {
                query = query.Where(student => student._classrooms._idClassroom == studentSearch.classroomId.Value);
            }
            return query;
        }

        private IQueryable<Students>? FilterSort(IQueryable<Students> query, SearchStudentDTO studentSearch)
        {
            if (studentSearch.sortBy.Equals("id"))
            {
                query = query.OrderBy(s=>s._id);
            }
            if (studentSearch.sortBy.Equals("studentCode"))
            {
                query = query.OrderBy(s => s._studentCode);
            }
            if (studentSearch.sortBy.Equals("studentName"))
            {
                query = query.OrderBy(s => s._name);
            }
            
            return query;
        }

        public async Task<List<StudentAgeDTO>> GetStudentAgesChartAsync(int id = 1)
        {
            var query = session.Query<Students>();
            if (id != 1)
            {
                query = query.Where(s => s._classrooms._idClassroom == id);
            }
            List<StudentAgeDTO> result = await query
                .GroupBy(s => DateTime.Now.Year - s._birthday.Year)
                .Select(s => new StudentAgeDTO
                {
                    Age = s.Key,
                    Count = s.Count()
                })
                .OrderBy(r => r.Age)
                .ToListAsync();
            return result;
        }

    }
}
