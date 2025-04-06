using Core.Domain.Entities;
using Core.Domain.Interfaces;
using Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class EnrollmentRepository : Repository<Enrollment>, IEnrollmentRepository
    {
        private readonly AppDbContext _context;

        public EnrollmentRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Student?>> GetStudentsBySubjectAsync(Guid subjectId)
        {
            return await _context.Enrollments
                .Where(e => e.SubjectId == subjectId)
                .Include(e => e.Student)
                .Select(e => e.Student)
                .ToListAsync();
        }

        public async Task<IEnumerable<Enrollment>> FindAllByStudentIdAsync(Guid studentId)
        {
            return await _context.Enrollments
                .Where(e => e.StudentId == studentId)
                .ToListAsync();
        }
    }
}
