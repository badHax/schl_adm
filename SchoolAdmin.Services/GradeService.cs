using SchoolAdmin.DataAccess.Repositories;
using SchoolAdmin.Domain;
using System;

namespace SchoolAdmin.Services
{
    public class GradeService : IGradeService
    {
        private readonly GradeRepository _repository;

        public GradeService(GradeRepository gradeRepository)
        {
            this._repository = gradeRepository;
        }

        public Grade GetGrade(string id)
        {
            try
            {
                var grade = _repository.Get(id);
                return grade;
            }
            catch (Exception)
            {
                return new Grade();
            }
        }

        public Grade GetGrade(string teacherId, string studentId, string courseId)
        {
            throw new NotImplementedException();
        }

        public bool IsCourseAdministrator(string id)
        {
            throw new NotImplementedException();
        }
    }
}
