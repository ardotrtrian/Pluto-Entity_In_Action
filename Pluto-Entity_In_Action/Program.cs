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

                var query2ext = db.Courses.Where(c => c.Level == CourseLevel.Intermediate);

                //Ordering
                var query3 = from c in db.Courses
                             orderby c.FullPrice descending, c.Title
                             //Projection
                             select new
                             {
                                 Name = c.Title,
                                 Instructor = c.Author
                             };

                var quer3ext = db.Courses.
                               OrderByDescending(c => c.FullPrice).
                               ThenBy(c => c.Title).
                               Select(c => new
                               {
                                   Name = c.Title,
                                   Instructor = c.Author,
                                   Price = c.FullPrice
                               });


                Console.WriteLine();

                //Set operations
                var set = db.Courses.
                          Where(c => c.Level == CourseLevel.Advanced).
                          OrderByDescending(c => c.FullPrice).
                          ThenBy(c => c.Title).
                          SelectMany(c => c.Tags).
                          Distinct();
                foreach (var item in set)
                {
                    Console.WriteLine(item.Name);
                }
                Console.WriteLine();

                //Grouping
                //breaking down the courses into groups by level
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

                Console.WriteLine("Group the courses by level using extension method");
                var groupingByLevelext = db.Courses.GroupBy(c => c.Level);

                foreach (var group in groupingByLevelext)
                {
                    Console.WriteLine($"{group.Key} : ");

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

                var coursesAndAuthorsext = db.Courses.Join(
                                   db.Authors,
                                   c => c.AuthorId,
                                   a => a.Id, (course, author) => new
                                   {
                                       CourseName = course.Title,
                                       authorName = author.Name
                                   });

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

                var authorAndTheirCoursesext =
                                        db.Authors.GroupJoin(db.Courses,
                                        a => a.Id,
                                        c => c.AuthorId,
                                        (author, courses) => new
                                        {
                                            authorName = author.Name,
                                            courseCount = courses.Count()
                                        });

                //cross join
                //show a combination of all authors and their courses
                var allCoursesAndAllAuthors = from a in db.Authors
                                              from c in db.Courses
                                              select new
                                              {
                                                  AuthorName = a.Name,
                                                  CourseName = c.Title
                                              };

                var allCoursesAndAllAuthorsext = db.Courses.SelectMany(a => db.Courses, (author, course) => new
                {
                    authorName = author.Author.Name,
                    courseName = course.Title
                });

                //Partitioning
                var page2 = db.Courses.Skip(10).Take(10);

                //Element Operators
                db.Courses.OrderBy(c => c.Level).First();

                //order the course list by level, then return the first course which price is greater than 200
                db.Courses.OrderBy(c => c.Level).FirstOrDefault(c => c.FullPrice > 200);

                //Give me a course with id=1
                db.Courses.SingleOrDefault(c => c.Id == 1);

                //Are all courses fullprice more than 10 , return true if correct
                var boolean = db.Courses.All(c => c.FullPrice > 10);

                //are there any courses with level 1?
                var LevelOneCourseExist = db.Courses.Any(c => c.Level == CourseLevel.beginner);

                //Aggregate functions:
                var countOfCourses = db.Courses.Count();
                var max = db.Courses.Max(c => c.FullPrice);  //also there are min and average
                var countCoursesInLevelOne = db.Courses.Count(c => c.Level == CourseLevel.beginner);

                //IQueryable VS IEnumerable

                //IQueryable makes the queries extendable without executing them.
                //the expressions we extend are stored in an expression object , and once we iterate or call a method, 
                //the expression object is executed in the database.
                IQueryable<Course> coursesQ = db.Courses;
                var filteredCourses = coursesQ.Where(c => c.FullPrice < 100);
                //once we start itereating, the query that goes to db is select * where fullprice<100 (query with filter)
                foreach (var item in filteredCourses)
                {
                    Console.WriteLine(item.Title);
                }

                //IEnumerable 
                IEnumerable<Course> coursesEnumerable = db.Courses; 
                //here is where query goes to db and brings all records then filters it..
                //..here
                var filteredCoursesEnumerable = coursesQ.Where(c => c.FullPrice < 100);
                foreach (var item in filteredCoursesEnumerable)
                {
                    Console.WriteLine(item.Title);
                }

            }
        }
    }
}
