using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolC1.Models
{
    public class Teacher
    {

        // The following fields define a Teacher 
        public int TeacherId;
        public string TeacherFname;
        public string TeacherLname;
        public List<Courses> CoursesList;

    }

}