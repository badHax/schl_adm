using System.Collections.Generic;
using SchoolAdmin.Domain;

namespace SchoolAdmin.DataAccess.Interfaces
{
    public interface ICourseRepository
    {
        string Add(Course course);
        void AddAsync(Course courses);
        Course Get(string id);
        List<Course> GetAll();
        void Remove(Course course);
        void Update(Course course);
    }
}