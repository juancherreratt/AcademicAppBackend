using Core.Domain.Entities;

namespace Core.Domain.Interfaces
{
    public interface ISubjectRepository : IRepository<Subject>
    {
        Task<List<Subject>> GetAllWithTeacherAsync();
    }
}
