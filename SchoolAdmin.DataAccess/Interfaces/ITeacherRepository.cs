using System.Collections.Generic;
using SchoolAdmin.Domain;

namespace SchoolAdmin.DataAccess.Interfaces
{
    public interface ITeacherRepository
    {
        string Add(Teacher teacher);
        void AddAsync(Teacher teachers);
        Teacher Get(string id);
        List<Teacher> GetAll();
        void Remove(Teacher teacher);
        void Update(Teacher teacher);
    }
}