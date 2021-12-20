using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SchoolC1.Models;
using MySql.Data.MySqlClient;
using System.Diagnostics;



namespace SchoolC1.Controllers
{
    public class TeacherDataController : ApiController
    {
        // Database context class to access MySQL Database.
        private SchoolDbContext School = new SchoolDbContext();

        //Controller to access the teachers table of the School database.
        /// <summary>
        /// Returns a list of teachers in the system
        /// </summary>
        /// <example>GET api/TeacherData/ListTeachers</example>
        /// <returns>
        /// A list of Teachers (first names and last names)
        /// </returns>
        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{SearchKey?}")]
        public IEnumerable<Teacher> ListTeachers(string SearchKey = null)
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for the database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from Teachers  where lower(teacherfname) like lower(@key) or lower(teacherlname) like lower(@key) or lower(concat(teacherfname, ' ', teacherlname)) like lower(@key)";

            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
            cmd.Prepare();

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Teachers
            List<Teacher> Teachers = new List<Teacher> { };

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {

                Teacher NewTeacher = new Teacher();
                NewTeacher.TeacherId = (int)ResultSet["teacherid"];
                NewTeacher.TeacherFname = (string)ResultSet["teacherfname"];
                NewTeacher.TeacherLname = (string)ResultSet["teacherlname"];
                if (ResultSet.IsDBNull(ResultSet.GetOrdinal("salary")))
                {
                    NewTeacher.TeacherSalary = null;
                }
                else
                {
                    NewTeacher.TeacherSalary = ResultSet.GetDecimal("salary");
                }
                if (ResultSet.IsDBNull(ResultSet.GetOrdinal("hiredate")))
                {
                    NewTeacher.TeacherHireDate = null;
                }
                else
                {
                    NewTeacher.TeacherHireDate = ResultSet.GetDateTime("hiredate").ToString(string.Format("dd/MM/yyyy"));
                }
                if (ResultSet.IsDBNull(ResultSet.GetOrdinal("employeenumber")))
                {
                    NewTeacher.TeacherEmployeeNumber = null;
                }
                else
                {
                    NewTeacher.TeacherEmployeeNumber = ResultSet.GetString("employeenumber");
                }



                //Add the Teacher Name to the List
                Teachers.Add(NewTeacher);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of Teacher names
            return Teachers;
        }

        [HttpGet]
        public Teacher FindTeacher(int id)
        {
            Teacher NewTeacher = new Teacher();

            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            string query = "Select * from Teachers where teacherid=@id";
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();


            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                NewTeacher.TeacherId = (int)ResultSet["teacherid"];
                NewTeacher.TeacherFname = (string)ResultSet["teacherfname"];
                NewTeacher.TeacherLname = (string)ResultSet["teacherlname"];
                if (ResultSet.IsDBNull(ResultSet.GetOrdinal("salary")))
                {
                    NewTeacher.TeacherSalary = null;
                }
                else
                {
                    NewTeacher.TeacherSalary = ResultSet.GetDecimal("salary");
                }
                if (ResultSet.IsDBNull(ResultSet.GetOrdinal("hiredate")))
                {
                    NewTeacher.TeacherHireDate = "";
                }
                else
                {
                    NewTeacher.TeacherHireDate = ResultSet.GetDateTime("hiredate").ToString(string.Format("yyyy/MM/dd"));
                }
                if (ResultSet.IsDBNull(ResultSet.GetOrdinal("employeenumber")))
                {
                    NewTeacher.TeacherEmployeeNumber = null;
                }
                else
                {
                    NewTeacher.TeacherEmployeeNumber = ResultSet.GetString("employeenumber");
                }
            }


            return NewTeacher;
        }

        /// <summary>
        /// Adds a teacher into the system 
        /// </summary>
        /// <param name="NewTeacher">Teacher object</param>
        public void AddTeacher(string TeacherFname, string TeacherLname,
            decimal? TeacherSalary, string TeacherEmployeeNumber)
        {
            inputValidation(TeacherFname, TeacherLname,
            TeacherSalary, TeacherEmployeeNumber);


            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();
            string query = "insert into teachers (teacherfname, teacherlname, salary, hiredate, employeenumber)" +
                " values (@fname,@lname,@salary,@hiredate,@employeenumber)";

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@fname", TeacherFname);
            cmd.Parameters.AddWithValue("@lname", TeacherLname);
            cmd.Parameters.AddWithValue("@salary", TeacherSalary);
            cmd.Parameters.AddWithValue("@hiredate", DateTime.Now.ToString(string.Format("yyyy/MM/dd")));
            cmd.Parameters.AddWithValue("@employeenumber", TeacherEmployeeNumber);

            cmd.ExecuteNonQuery();

            Conn.Close();

        }
        private void inputValidation(string TeacherFname, string TeacherLname,
            decimal? TeacherSalary, string TeacherEmployeeNumber)
        {
            if (String.IsNullOrEmpty(TeacherFname))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            if (String.IsNullOrEmpty(TeacherLname))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            if (TeacherSalary == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            if (String.IsNullOrEmpty(TeacherEmployeeNumber))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }
        /// <summary>
        /// Deletes a teacher in the system by it's primary key 
        /// </summary>
        /// <param name="id">the primary key of the teacher</param>
        public void DeleteTeacher(int id)
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            string query = "delete from teachers where teacherid=@id";

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();

            Conn.Close();

        }

        public void Update(int id, Teacher TeacherInfo)
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();
            string query = "update teachers " +
                "set teacherfname=@fname," +
                "teacherlname=@lname," +
                "salary=@salary," +
                "hiredate=@hiredate," +
                "employeenumber=@employeenumber " +
                "where teacherid=@TeacherId";


            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@fname", TeacherInfo.TeacherFname);
            cmd.Parameters.AddWithValue("@lname", TeacherInfo.TeacherLname);
            cmd.Parameters.AddWithValue("@salary", TeacherInfo.TeacherSalary);
            cmd.Parameters.AddWithValue("@hiredate", DateTime.Parse(TeacherInfo.TeacherHireDate).ToString(string.Format("yyyy/MM/dd")));
            cmd.Parameters.AddWithValue("@employeenumber", TeacherInfo.TeacherEmployeeNumber);
            cmd.Parameters.AddWithValue("@TeacherId", id);

            cmd.ExecuteNonQuery();

            Conn.Close();
        }
    }

}

