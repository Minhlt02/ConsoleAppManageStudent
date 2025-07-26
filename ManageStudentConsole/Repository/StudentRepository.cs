using ManageStudentConsole.DBHelper;
using ManageStudentConsole.Entity;
using ManageStudentConsole.HandleException;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageStudentConsole.Repository
{
    public class StudentRepository : IStudentRepository
    {
        public void Add(Students students)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (ITransaction tx = session.BeginTransaction())
                {
                    var classroom = session.Get<Classrooms>(1);

                    students._classrooms = classroom;
                    session.Save(students);
                    tx.Commit();
                }
            }
        }

        public void Delete(int id)
        {

            using (var session = NHibernateHelper.OpenSession())
            {
                using (ITransaction tx = session.BeginTransaction())
                {
                    var student = session.Get<Students>(id);
                    session.Delete(student);
                    tx.Commit();
                }
            }
        }

        public Students FindById(int id)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (ITransaction tx = session.BeginTransaction())
                {
                    return session.Query<Students>()
                        .Fetch(s => s._classrooms)
                        .ThenFetch(c => c._teacher)
                        .FirstOrDefault(s => s._idStudent == id);
                }
            }
        }

        public List<Students> GetAll()
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (ITransaction tx = session.BeginTransaction())
                {
                    return session.Query<Students>().ToList();
                }
            }
        }

        public List<Students> SortByName()
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (ITransaction tx = session.BeginTransaction())
                {
                    return session.Query<Students>().OrderBy(s => s._name).ToList();
                }
            }
        }

        public void Update(Students students)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (ITransaction tx = session.BeginTransaction())
                {
                    session.Update(students);
                    tx.Commit();
                }
            }
        }
    }
}
