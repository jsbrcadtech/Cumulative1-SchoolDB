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
            TeacherBusiness Controller = new TeacherBusiness();
            IEnumerable<Teacher> Teachers = Controller.ListTeachers(SearchKey);
            return View(Teachers);
        }

        //GET : /Teacher/Show/{id}
        public ActionResult Show(int id)
        {
            TeacherBusiness Controller = new TeacherBusiness();
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

            TeacherBusiness Controller = new TeacherBusiness();


            Controller.AddTeacher(TeacherFname, TeacherLname,
            TeacherSalary, TeacherEmployeeNumber);


            // go back to the list6 of teachers 
            return RedirectToAction("List");

        }

        //GET : DeleteConfirm/{id}
        [HttpGet]
        [Route("Teacher/Create")]
        public ActionResult DeleteConfirm(int id)
        {

            // get info about Teacher to confirm delete
            TeacherBusiness Controller = new TeacherBusiness();
            Teacher NewTeacher = Controller.FindTeacher(id);

            return View(NewTeacher);
        }

        //POST : Teacher/Delete{id}
        public ActionResult Delete(int id)
        {
            TeacherBusiness Controller = new TeacherBusiness();
            Controller.DeleteTeacher(id);


            return RedirectToAction("List");
        }

    }
}