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
                        .ThenFetch(c => c.Teacher)
                        .FirstOrDefaultAsync(s => s._id == id);
            return students;
        }

        public async Task<List<Students>> GetAllStudentAsync()
        {
            List<Students> students = await session.Query<Students>()
                        .Fetch(s => s._classrooms)
                        .ThenFetch(c => c.Teacher)
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
                            .ThenFetch(c => c.Teacher).AsQueryable();
            query = Filter(query, searchStudent);
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
            if (studentSearch.keywordId.HasValue)
            {
                query = query.Where(student => student._id == studentSearch.keywordId.Value);
            }
            if (!string.IsNullOrEmpty(studentSearch.keyword))
            {
                query = query.Where(s => s._name.Contains(studentSearch.keyword));
            }
            if (!string.IsNullOrEmpty(studentSearch.studentAddress))
            {   
                query = query.Where(s => s._address.Contains(studentSearch.studentAddress));
            }
            if (studentSearch.teacherId.HasValue)
            {
                query = query.Where(student => student._classrooms.Teacher.Id == studentSearch.teacherId.Value);
            }
            if (studentSearch.classroomId.HasValue)
            {
                query = query.Where(student => student._classrooms.Id == studentSearch.classroomId.Value);
            }
            if (studentSearch.sortBy == "id")
            {
                query = query.OrderBy(s => s._id);
            }
            if (studentSearch.sortBy == "studentName")
            {
                query = query.OrderBy(s => s._name);
            }
            return query;
        }

        public async Task<List<StudentChartDTO>> GetStudentAgesChartAsync(int id = 1)
        {
            var query = session.Query<Students>();
            if (id != 1)
            {
                query = query.Where(s => s._classrooms.Id == id);
            }
            List<StudentChartDTO> result = await query
                .GroupBy(s => DateTime.Now.Year - s._birthday.Year)
                .Select(s => new StudentChartDTO
                {
                    Age = s.Key,
                    Count = s.Count()
                })
                .OrderBy(r => r.Age)
                .ToListAsync();
            return result;
        }

        public async Task<List<StudentChartDTO>> GetStudentCountChartAsync(int id)
        {
            var query = session.Query<Students>()
                               .Fetch(s => s._classrooms)
                               .AsQueryable();

            if (id != 0)
            {
                query = query.Where(s => s._classrooms.Id == id);
            }

            var result = await session.Query<Classrooms>()
                        .Select(c => new StudentChartDTO
                        {
                            ClassName = c.NameClassroom,
                            Count = session.Query<Students>()
                                    .Count(s => s._classrooms.Id == c.Id)
                        })
                        .OrderBy(x => x.ClassName)
                        .ToListAsync();
            return result;
        }

        public async Task<List<StudentChartDTO>> GetStudentCountOfTeacherChartAsync(int id)
        {
            var query = session.Query<Students>()
                               .Fetch(s => s._classrooms)
                               .ThenFetch(c => c.Teacher)
                               .AsQueryable();

            if (id != 0)
            {
                query = query.Where(s => s._classrooms.Teacher.Id == id);
            }

            var result = await session.Query<Teachers>()
                .Select(t => new StudentChartDTO
                {
                    TeacherName = t.NameTeacher,
                    Count = session.Query<Students>()
                            .Count(s=>s._classrooms.Teacher.Id == t.Id)
                })
                .OrderBy(r => r.TeacherName)
                .ToListAsync();

            return result;
        }

    }
}
