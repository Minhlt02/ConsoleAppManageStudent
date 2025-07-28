using Server.DTO;
using Server.Entity;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Repository
{
    public interface IStudentRepository
    {
        Task<List<Students>> GetAllStudentAsync();
        Task<List<Students>> GetSortStudentAsync();
        Task<Students> GetStudentByIdAsync(int id);
        Task AddStudentAsync(Students student);
        Task UpdateStudentAsync(Students student);
        Task DeleteStudentAsync(Students student);
        Task<int> CountAsync();
        Task<PageViewDTO<Students>> GetPaginationAsync(SearchStudentDTO searchStudent, int pageNumber, int pageSize);
        Task<List<StudentAgeDTO>> GetStudentAgesChartAsync(int id = -1);

    }
}
