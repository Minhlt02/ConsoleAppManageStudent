using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using ManageStudentConsole.Mapping;
using NHibernate;

namespace ManageStudentConsole.DBHelper
{
    class NHibernateHelper
    {
        private static readonly ISessionFactory _sessionFactory;
        static NHibernateHelper()
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
        public static ISession GetCurrentSession()
        {
            return _sessionFactory.OpenSession();
        }
        public static ISession OpenSession()
        {
            var session = _sessionFactory.OpenSession();
            if (session == null)
            {
                throw new InvalidOperationException("Could not open NHibernate session.");
            }
            return session;
        }
        public static void CloseSession(ISession session)
        {
            session?.Dispose();
        }
        public static void CloseSessionFactory()
        {
            if (_sessionFactory != null)
            {
                _sessionFactory.Close();
            }
        }
    }
}
