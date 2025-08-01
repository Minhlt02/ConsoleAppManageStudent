﻿using NHibernate.Linq;
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
        Task<List<Students>> GetStudentsByIdAsync(List<int> ids);
        Task AddStudentAsync(Students student);
        Task UpdateStudentAsync(Students student);
        Task DeleteStudentAsync(Students student);
        Task DeleteManyStudentAsync(List<Students> students);
        Task<int> CountAsync();
        Task<PageViewDTO<Students>> GetPaginationAsync(SearchStudentDTO searchStudent);
        Task<List<StudentChartDTO>> GetStudentAgesChartAsync(int id);
        Task<List<StudentChartDTO>> GetStudentCountChartAsync(int id);
        Task<List<StudentChartDTO>> GetStudentCountOfTeacherChartAsync(int id);


    }
}
