using SchoolAdmin.Domain;
using System.Collections.Generic;
using System.Linq;
using System;
using SchoolAdmin.DataAccess.DbContext;
using SchoolAdmin.DataAccess.Interfaces;

namespace SchoolAdmin.DataAccess.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private ISchoolDbContext _context;

        public StudentRepository(ISchoolDbContext schoolDbContext)
        {
            this._context = schoolDbContext;
        }
        public string Add (Student student)
        {
            Student studentx = _context.Students.Where(s => 
                s.FirstName == student.FirstName && 
                s.LastName == student.LastName &&
                s.MiddleName == student.MiddleName)
                .FirstOrDefault();

            if (studentx == null)
            {
                _context.Students.Add(student);
                _context.SaveChanges();
                return "success";
            }
            else
            {
                return "duplicate";
            }
        }
        public void Update (Student student) { }
        public Student Remove (Student student)
        {
            var res = _context.Students.Remove(student);
            return res;
        }

        public Student Get (string id)
        {
            var student =_context.Students.Find(id);
            return student;
        }

        public List<Student> GetAll()
        {
            var students = _context.Students.ToList();
            return students;
        }

        public void AddAsync(Student student)
        {
            throw new NotImplementedException();
        }
    }
}
