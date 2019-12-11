using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pluto_Entity_In_Action
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new PlutoContext())
            {
                //LINQ syntax
                var data = from course in db.Courses
                           where course.Title.Contains("C#")
                           orderby course.Title
                           select course.Title;


                //Extension methods
                var ext = db.Courses.Where(c => c.Title.Contains("C#")).OrderBy(c => c.Title);

                foreach (var item in data)
                {
                    Console.WriteLine(item);
                }

                foreach (var item in ext)
                {
                    Console.WriteLine(item.Title);
                }

                //Restriction
                var query2 = from c in db.Courses
                             where c.Level == CourseLevel.Intermediate
                             select c;

                //Ordering
                var query3 = from c in db.Courses
                             orderby c.FullPrice descending, c.Title
                             //Projection
                             select new
                             {
                                 Name = c.Title,
                                 Instructor = c.Author
                             };

                Console.WriteLine();

                //Grouping
                var groupingQuery = from course in db.Courses
                                    group course by course.Level
                                    into groupsByLevel
                                    select groupsByLevel;

                foreach (var group in groupingQuery)
                {
                    Console.WriteLine($"Group Key: {group.Key} , contains {group.Count()} courses");

                    foreach (var course in group)
                    {
                        Console.WriteLine(course.Title);
                    }
                    Console.WriteLine();
                }

                //Joining//
                //inner join
                var coursesAndAuthors = from c in db.Courses
                        join a in db.Authors
                        on c.AuthorId equals a.Id
                        select new
                        {
                            courseName = c.Title,
                            authorName = a.Name
                        };
                //group join
                //output the author name and number of courses he has
                var authorAndTheirCourses = from a in db.Authors
                                            join c in db.Courses
                                            on a.Id equals c.AuthorId
                                            into g
                                            select new 
                                            {
                                                AuthorName = a.Name,
                                                Courses = g.Count()
                                            };
                foreach (var record in authorAndTheirCourses)
                {
                    Console.WriteLine($"Author name: {record.AuthorName} , number of courses he has: {record.Courses}");
                }

                //cross join
                //show a combination of all authors and their courses
                var allCoursesAndAllAuthors = from a in db.Authors
                                              from c in db.Courses
                                              select new
                                              {
                                                  AuthorName = a.Name,
                                                  CourseName = c.Title
                                              };
            }

            
        }
    }
}
