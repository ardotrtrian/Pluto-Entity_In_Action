using Pluto_Entity_In_Action.Models;
using System.Data.Entity;

namespace Pluto_Entity_In_Action
{
    class PlutoContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Category> Category { get; set; }

        public PlutoContext() 
            : base("name=DefaultConnection")
        {

        }
    }
}
