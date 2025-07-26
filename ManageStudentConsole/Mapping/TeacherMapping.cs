using FluentNHibernate.Mapping;
using ManageStudentConsole.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageStudentConsole.Mapping
{
    public class TeacherMapping : ClassMap<Teachers>
    {
        public TeacherMapping()
        {
            Id(x => x._id, "id").GeneratedBy.Identity();
            Map(x => x._idTeacher, "teacher_id");
            Map(x => x._nameTeacher, "teacher_name");
            Map(x => x._birthdayTeacher, "teacher_birthday");
        }
    }
}
