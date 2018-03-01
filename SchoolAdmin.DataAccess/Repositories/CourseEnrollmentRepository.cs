using SchoolAdmin.DataAccess.DbContext;
using SchoolAdmin.DataAccess.Interfaces;
using SchoolAdmin.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace SchoolAdmin.DataAccess.Repositories
{
    public class CourseEnrollmentRepository : ICourseEnrollmentRepository
    {
        private ISchoolDbContext _context;

        public CourseEnrollmentRepository(ISchoolDbContext schoolDbContext)
        {
            this._context = schoolDbContext;
        }
        public string Add(CourseEnrollment enrollment)
        {
            CourseEnrollment enrollmentx = _context.Enrollment.Where(e =>
                e.Id == enrollment.Id &&
                e.Id == enrollment.Id
                )
                .FirstOrDefault();

            if (enrollmentx == null)
            {
                _context.Enrollment.Add(enrollment);
                _context.SaveChanges();
                return "success";
            }
            else
            {
                return "duplicate";
            }
        }
        public void Update(CourseEnrollment enrollment)
        {
            _context.Enrollment.AddOrUpdate(enrollment);
            _context.SaveChanges();
        }

        public void Remove(CourseEnrollment enrollment)
        {
            _context.Enrollment.Remove(enrollment);
        }

        public CourseEnrollment Get(string id, string courseId)
        {
            var enrollments = _context.Enrollment.Where(e => e.Id == id && e.CourseId == courseId).FirstOrDefault();
            return enrollments;
        }

        public List<CourseEnrollment> GetAll()
        {
            var Enrollment = _context.Enrollment.ToList();
            return Enrollment;
        }

        public void AddAsync(CourseEnrollment enrollments)
        {
            throw new NotImplementedException();
        }
    }
}
