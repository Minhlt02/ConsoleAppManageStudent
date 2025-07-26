using Server.Entity;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Repository
{
    public interface IClassroomRepository
    {
        Task<List<Classrooms>> GetAllClassroomAsync();
    }
}
