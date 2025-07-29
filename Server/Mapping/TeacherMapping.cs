using FluentNHibernate.Mapping;
using Server.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Mapping
{
    public class TeacherMapping : ClassMap<Teachers>
    {
        public TeacherMapping()
        {
            Id(x => x.Id, "id").GeneratedBy.Identity();
            Map(x => x.NameTeacher, "teacher_name");
            Map(x => x.BirthdayTeacher, "teacher_birthday");
        }
    }
}
