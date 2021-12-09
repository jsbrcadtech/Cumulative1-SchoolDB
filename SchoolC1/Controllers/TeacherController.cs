using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolC1.Models;
using System.Diagnostics;

namespace SchoolC1.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult index()
        {
            return View();
        }
        //GET : /Teacher/List
        public ActionResult List(string SearchKey = null)
        {
            TeacherDataController Controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = Controller.ListTeachers(SearchKey);
            return View(Teachers);
        }

        //GET : /Teacher/Show/{id}
        public ActionResult Show(int id)
        {
            TeacherDataController Controller = new TeacherDataController();
            Teacher NewTeacher = Controller.FindTeacher(id);

            return View(NewTeacher);
        }
        //GET: Teacher/New
        [HttpGet]
        [Route("Teacher/NewTeacher")]

        public ActionResult NewTeacher()
        {
            return View();

        }

        //POST : Teacher/Create
        [HttpPost]
        [Route("Teacher/Create")]
        public ActionResult Create(string TeacherFname, string TeacherLname,
            decimal? TeacherSalary, string TeacherEmployeeNumber)
        {

            TeacherDataController Controller = new TeacherDataController();


            Controller.AddTeacher(TeacherFname, TeacherLname,
            TeacherSalary, TeacherEmployeeNumber);


            // Return to the list of teachers 
            return RedirectToAction("List");

        }

        //GET : DeleteConfirm/{id}
        [HttpGet]
        [Route("Teacher/Create")]
        public ActionResult DeleteConfirm(int id)
        {

            // get info about Teacher to confirm delete
            TeacherDataController Controller = new TeacherDataController();
            Teacher NewTeacher = Controller.FindTeacher(id);

            return View(NewTeacher);
        }

        //POST : Teacher/Delete{id}
        public ActionResult Delete(int id)
        {
            TeacherDataController Controller = new TeacherDataController();
            Controller.DeleteTeacher(id);


            return RedirectToAction("List");
        }

    }
}