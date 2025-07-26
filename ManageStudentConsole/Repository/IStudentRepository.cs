using ManageStudentConsole.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageStudentConsole.Repository
{
    public interface IStudentRepository
    {
        //int GenerateID();
        void Add(Students students);
        List<Students> GetAll();
        void Update(Students students);
        Students FindById(int id);
        void Delete(int id);
        List<Students> SortByName();

    }
}
