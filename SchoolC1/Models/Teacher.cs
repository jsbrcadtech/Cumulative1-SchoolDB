using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolC1.Models
{
    public class Teacher
    {

        // The following fields define a Teacher 
        public int TeacherId { get; set; }
        public string TeacherFname { get; set; }
        public string TeacherLname { get; set; }
        public decimal? TeacherSalary { get; set; }
        public string TeacherHireDate { get; set; }
        public string TeacherEmployeeNumber { get; set; }

    }

}