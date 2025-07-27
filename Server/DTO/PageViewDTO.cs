using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DTO
{
    public class PageViewDTO<T> where T : class
    {
        public int total { get; set; }
        public List<T>? listStudents { get; set; }
    }
}
