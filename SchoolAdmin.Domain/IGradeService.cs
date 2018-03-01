namespace SchoolAdmin.Domain
{
    public interface IGradeService
    {
        Grade GetGrade(string teacherId, string studentId, string courseId);
        bool IsCourseAdministrator(string id);
    }
}