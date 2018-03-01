using SchoolAdmin.DataAccess.DbContext;
using SchoolAdmin.DataAccess.Interfaces;
using SchoolAdmin.Domain;

namespace SchoolAdmin.DataAccess.Repositories
{
    public class GradeRepository : IGradeRepository
    {
        private ISchoolDbContext _context;

        public GradeRepository(ISchoolDbContext schoolDbContext)
        {
            this._context = schoolDbContext;
        }
        public void Add(Grade grade) { }
        public void Update(Grade grade) { }
        public void Remove(Grade grade)
        {
            _context.Grades.Remove(grade);
        }

        public Grade Get(string id)
        {
            var student = _context.Grades.Find(id);
            return student;
        }
    }
}
