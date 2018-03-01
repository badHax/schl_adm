using System.Collections.Generic;
using SchoolAdmin.Domain;

namespace SchoolAdmin.DataAccess.Interfaces
{
    public interface IStudentRepository
    {
        string Add(Student student);
        void AddAsync(Student student);
        Student Get(string id);
        List<Student> GetAll();
        Student Remove(Student student);
        void Update(Student student);
    }
}