
using SchoolAdmin.DataAccess.DbContext;
using SchoolAdmin.DataAccess.Interfaces;
using SchoolAdmin.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace SchoolAdmin.DataAccess.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private ISchoolDbContext _context;

        public CourseRepository(ISchoolDbContext schoolDbContext)
        {
            this._context = schoolDbContext;
        }
        public string Add(Course course)
        {
            Course coursex = _context.Courses.Where(c =>
                c.CourseName == course.CourseName)
                .FirstOrDefault();

            if (coursex == null)
            {
                _context.Courses.Add(course);
                _context.SaveChanges();
                return "success";
            }
            else
            {
                return "duplicate";
            }
        }
        public void Update(Course course)
        {
            _context.Courses.AddOrUpdate(course);
            _context.SaveChanges();
        }

        public void Remove(Course course)
        {
            _context.Courses.Remove(course);
        }

        public Course Get(string id)
        {
            var courses = _context.Courses.Find(id);
            return courses;
        }

        public List<Course> GetAll()
        {
            var Courses = _context.Courses.ToList();
            return Courses;
        }

        public void AddAsync(Course courses)
        {
            throw new NotImplementedException();
        }
    }
}
