using SchoolAdmin.DataAccess.DbContext;
using SchoolAdmin.DataAccess.Interfaces;
using SchoolAdmin.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace SchoolAdmin.DataAccess.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
            private ISchoolDbContext _context;

            public TeacherRepository(ISchoolDbContext schoolDbContext)
            {
                this._context = schoolDbContext;
            }
            public string Add(Teacher teacher)
            {
                Teacher teacherx = _context.Teachers.Where(c =>
                    c.FirstName == teacher.FirstName)
                    .FirstOrDefault();

                if (teacherx == null)
                {
                    _context.Teachers.Add(teacher);
                    _context.SaveChanges();
                    return "success";
                }
                else
                {
                    return "duplicate";
                }
            }
            public void Update(Teacher teacher)
            {
                _context.Teachers.AddOrUpdate(teacher);
                _context.SaveChanges();
            }

            public void Remove(Teacher teacher)
            {
                _context.Teachers.Remove(teacher);
            }

            public Teacher Get(string id)
            {
                var teachers = _context.Teachers.Find(id);
                return teachers;
            }

            public List<Teacher> GetAll()
            {
                var Teachers = _context.Teachers.ToList();
                return Teachers;
            }

            public void AddAsync(Teacher teachers)
            {
                throw new NotImplementedException();
            }
        }
    }

