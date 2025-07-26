using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Server.Mapping;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DBHelper
{
    public class NHibernateHelper
    {
        private readonly ISessionFactory _sessionFactory;
        public NHibernateHelper()
        {
            string connectionString = "Data Source=MINHLT02\\SQLEXPRESS;Initial Catalog=ManageStudent;Integrated Security=True;TrustServerCertificate=True";

            _sessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(connectionString))
                .Mappings(m => m.FluentMappings
                    .AddFromAssemblyOf<TeacherMapping>()
                    .AddFromAssemblyOf<StudentMapping>()
                    .AddFromAssemblyOf<ClassroomMapping>())

                .BuildSessionFactory();
        }

        public ISession OpenSession()
        {
            var session = _sessionFactory.OpenSession();
            if (session == null)
            {
                throw new InvalidOperationException("Could not open NHibernate session.");
            }
            return session;
        }
    }
}
