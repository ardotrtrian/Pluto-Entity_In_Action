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

        //using Fluent API to change method calls 
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>()
                .Property(t => t.Description)
                .IsRequired();

            modelBuilder.Entity<Course>()
                .Property(t => t.Title)
                .IsRequired();
            
            //to configure primary key, use .HasKey()
            //to configure table name use ToTable()
            //to configure ColumnName use .HasColumnName()
            //to configure type of column use .HasColumnType()
            //to change length of strinf use .HasMaxLength()

            /* One-To-Many relationship */

                //Author// ---- //Cuorse//    (One author can have many courses, but one course has one author
                                                //modelBuilder.Entity<Author>()
                                                //            .HasMany(author=>author.Courses)
                                                //            .WithRequired(course=>course.Author)
                                                //            .HasForeignKey(course=>course.AuthorId);

            /* Many-To-Many Relationship */

               //Course// ---- //Tag//      (One course has many tags, one tag has many courses)
                                                //modelBuilder.Entity<Course>()
                                                //            .HasMany(course=>course.Tags)
                                                //            .WithMany(tag=>tag.Courses)
               
            /* One-To-One relationship */
               //Course// ---- //Cover//    (One course has one cover and one cover has obne course)
                                                //modelBuilder.Entity<Course>()
                                                //            .HasRequired(course=>course.cover)
                                                //            .WithRequiredPrincipal(cover=>cover.Course)
                                                
                                                //OR

                                                //modelBuilder.Entity<Cover>()
                                                //.HasRequired(cover=>cover.Course)
                                                //.WithRequiredDependant(course=>course.Cover)
        }
    }
}
