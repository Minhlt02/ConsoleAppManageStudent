using FluentNHibernate.Mapping;
using Server.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Mapping
{
    public class ClassroomMapping : ClassMap<Classrooms>
    {
        public ClassroomMapping()
        {
            Id(x => x.Id, "id").GeneratedBy.Identity();
            Map(x => x.NameClassroom, "classroom_name");
            Map(x => x.NameSubject, "classroom_subject");
            References(x => x.Teacher, "teacher_id").Cascade.None();
        }
    }
}
