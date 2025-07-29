using FluentNHibernate.Mapping;
using ManageStudentConsole.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageStudentConsole.Mapping
{
    public class StudentMapping : ClassMap<Students>
    {
        public StudentMapping()
        {
            Id(x => x._id, "id").GeneratedBy.Identity();
            Map(x => x._name, "student_name");
            Map(x => x._birthday, "student_birthday");
            Map(x => x._address, "student_address");
            References(x => x._classrooms, "classroom_id").Cascade.None();
        }
    }
}
