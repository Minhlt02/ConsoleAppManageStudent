using Server.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Repository
{
    public interface ITeacherRepository
    {
        Task<List<Teachers>> GetAllTeacherAsync();
        Task<Teachers?> GetTeachersByIdAsync(int id);
    }
}
