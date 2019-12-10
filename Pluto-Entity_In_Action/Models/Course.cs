using Pluto_Entity_In_Action.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pluto_Entity_In_Action
{
    public class Course
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Description { get; set; }

        public CourseLevel Level { get; set; }
        public float FullPrice { get; set; }

        [ForeignKey("Author")]
        public int AuthorId { get; set; }
        public Author Author { get; set; }

        public IList<Tag> Tags { get; set; }
        public Category Category { get; set; }
        public DateTime? DatePublished { get; set; }
    }
}
