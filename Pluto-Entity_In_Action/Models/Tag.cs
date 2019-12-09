using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pluto_Entity_In_Action
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<Course> Courses { get; set; }
    }
}
