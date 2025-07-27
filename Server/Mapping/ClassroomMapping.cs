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
            Id(x => x._id, "id").GeneratedBy.Identity();
            Map(x => x._idClassroom, "classroom_id");
            Map(x => x._nameClassroom, "classroom_name");
            Map(x => x._nameSubject, "classroom_subject");
            References(x => x._teacher, "teacher_id").Cascade.None();
        }
    }
}
