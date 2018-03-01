///////////////////////////////////////////

using log4net;
using Microsoft.AspNet.Identity;
using SchoolAdmin.Domain;
using SchoolAdmin.MVC.UxHelpers;
using System;
using System.Web.Mvc;

///////////////////////////////////////////

namespace SchoolAdmin.MVC.Controllers
{
    [Authorize(Roles = "teacher, admin")]
    [RoutePrefix("grade")]
    public class GradeController : Controller
    {
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private readonly IGradeService _gradeService;
        private readonly ITeacherService _teacherService;
        private readonly ICourseService _courseService;
        private readonly ILog _logger;

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public GradeController(IGradeService gradeService, ITeacherService teacherService, ICourseService courseService, ILog log)
        {
            _gradeService = gradeService;
            _teacherService = teacherService;
            _courseService = courseService;
            _logger = log;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public ActionResult Index()
        {
            return View();
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// For the current teacher viewing, they should be presented with the current courses that that they are administrating
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/MyCourses")]
        public ActionResult ListTeacherCourse()
        {
            try
            {
                var courseListing = _teacherService.ListTeacherCourses(User.Identity.GetUserId());
                return View(courseListing);
            }
            catch (NotFoundException e)
            {
                return View().Error(e.Message);
            }
            catch (Exception e)
            {
                _logger.Error(e);
                return View();
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Get Students for courses teacher administer
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("/MyCourses/MyStudents")]
        public ActionResult GetCourseEnrollment(string courseId)
        {
            try
            {
                var students = _courseService.GetRegisteredStudentsForCourse(courseId);
                return View(students);
            }
            catch (NotFoundException e)
            {
                return View().Error(e.Message);
            }
            catch(Exception e)
            {
                _logger.Error(e);
                return View();
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public ActionResult Edit()
        {
            return View();
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}