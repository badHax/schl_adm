////////////////////////////////////////////////

using log4net;
using SchoolAdmin.DataAccess.ViewModels;
using SchoolAdmin.Domain;
using SchoolAdmin.MVC.UxHelpers;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

////////////////////////////////////////////////

namespace SchoolAdmin.MVC.Controllers
{

    [Authorize(Roles = "admin, teacher")]
    [RoutePrefix("Student")]
    public class StudentController : Controller
    {
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private readonly IGradeService _gradeService;
        private readonly IStudentService _studentService;
        private readonly ICourseService _courseService;
        private ILog _logger;

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public StudentController(IStudentService studentService, ICourseService courseService ,ILog logger)
        {
            _courseService = courseService;
            _studentService = studentService;
            _logger = logger;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // GET: list
        [Route("/List")]
        [HttpPost]
        public ActionResult Index()
        {
            List<Student> students = _studentService.GetAllStudents();
            return View("ListStudents",students);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //Get: Student/Details
        [HttpPost]
        [Route("/Details")]
        public ActionResult Details(string id)
        {
            try
            {
                var student = _studentService.GetStudentDetails(id);
                ViewBag.GPA = _studentService.GetStudentGPA(student);
                return View("StudentDetails",student);
            }
            catch (NotFoundException)
            {
                return RedirectToAction("Index");
            }  
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //Get : Student/Add
        [Route("/Add")]
        [HttpGet]
        public ActionResult AddStudent()
        {
            return View("AddStudent");
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //Get : Student/Add
        [HttpPost]
        public ActionResult AddStudent(StudentViewModel studentModel)
        {
            if (ModelState.IsValid)
            {
                var student = new Student()
                {
                    FirstName = studentModel.FirstName,
                    LastName = studentModel.LastName,
                    Id = new Random(10).GetHashCode().ToString()
                };

                try
                {
                    _studentService.AddNew(student);
                    _logger.Info(String.Format("Added new student {0} {1} with id {2}", student.FirstName, student.LastName, student.Id));
                    return View().Success("added student");
                }
                catch (UpdateException e)
                {
                    _logger.Error(e.Message);
                    return View().Error(e.Message);
                }
            }
            return View().Warning("Undefined Error");
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //Get: Student
        [Route("/Enroll")]
        [HttpGet]
        public ActionResult EnrollStudentToCourse()
        {
            return View();
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPost]
        public ActionResult EnrollStudentToCourse(string studentId, string courseId)
        {
            try
            {
                _courseService.RegisterStudentToCourse(studentId, courseId);
                return View().Success("Added.");
            }
            catch (UpdateException e)
            {
                _logger.Error(e);
                return View().Error(e.Message);
            }
            catch(NotFoundException e)
            {
                _logger.Error(e);
                return View().Error(e.Message);
            }
            catch (Exception e)
            {
                _logger.Error(e);
                return View();
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}