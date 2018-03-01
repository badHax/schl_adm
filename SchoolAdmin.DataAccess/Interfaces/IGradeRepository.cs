using SchoolAdmin.Domain;

namespace SchoolAdmin.DataAccess.Interfaces
{
    public interface IGradeRepository
    {
        void Add(Grade grade);
        Grade Get(string id);
        void Remove(Grade grade);
        void Update(Grade grade);
    }
}