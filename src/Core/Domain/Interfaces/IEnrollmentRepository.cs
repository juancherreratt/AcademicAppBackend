using Core.Domain.Entities;

namespace Core.Domain.Interfaces
{
    public interface IEnrollmentRepository : IRepository<Enrollment>
    {
        Task<List<Student?>> GetStudentsBySubjectAsync(Guid subjectId);
        Task<IEnumerable<Enrollment>> FindAllByStudentIdAsync(Guid studentId);
    }
}
