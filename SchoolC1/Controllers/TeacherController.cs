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
            Teacher SelectedTeacher = Controller.FindTeacher(id);

            return View(SelectedTeacher);
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
        //GET:/Teacher/Update/{id}
        /// <summary>
        /// Routes to a dynamically generated "Teacher Update" Page. Gathers information from the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A dynamic "Update Teacher" page which provides the current information of the author
        /// and asks the user new info as part of a form</returns>
        /// <example>GET:/Teacher/Update/{id}</example>
        public ActionResult Update(int id)
        {
            TeacherDataController Controller = new TeacherDataController();
            Teacher SelectedTeacher = Controller.FindTeacher(id);

            return View(SelectedTeacher);
        }
        /// <summary>
        /// Receives a Post request containing information of existing teacher
        /// in the database, with new values.Takes the info to the API, and redirects to the 
        /// "Teacher Show" page of my updated Teacher
        /// </summary>
        /// <param name="id"></param>
        /// <param name="TeacherFname"></param>
        /// <param name="TeacherLname"></param>
        /// <param name="TeacherSalary"></param>
        /// <param name="TeacherEmployeeNumber"></param>
        /// <returns> A Dynamic page which provides the current info of the Teacher</returns>
        ///<example>POST :/Teacher/Update/{id}</example>
        [HttpPost]
        public ActionResult Update(int id, string TeacherFname, string TeacherLname,
            decimal? TeacherSalary, string TeacherEmployeeNumber, string TeacherHireDate)
        {

            Teacher TeacherInfo = new Teacher();

            TeacherInfo.TeacherId = id;
            TeacherInfo.TeacherFname = TeacherFname;
            TeacherInfo.TeacherLname = TeacherLname;
            TeacherInfo.TeacherSalary = TeacherSalary;
            TeacherInfo.TeacherHireDate = TeacherHireDate;
            TeacherInfo.TeacherEmployeeNumber = TeacherEmployeeNumber;

            //Add the Teacher Name to the List
            TeacherDataController Controller = new TeacherDataController();
            Controller.Update(id, TeacherInfo);

            return RedirectToAction("Show/" + id);
        }
    }
}