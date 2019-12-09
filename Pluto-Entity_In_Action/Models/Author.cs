using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pluto_Entity_In_Action
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public IList<Course> Courses { get; set; }
    }
}
