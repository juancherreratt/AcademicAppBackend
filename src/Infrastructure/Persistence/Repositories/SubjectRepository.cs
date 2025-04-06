using Core.Domain.Entities;
using Core.Domain.Interfaces;
using Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class SubjectRepository : Repository<Subject>, ISubjectRepository
    {
        private readonly AppDbContext _context;

        public SubjectRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Subject>> GetAllWithTeacherAsync()
        {
            return await _context.Subjects.Include(s => s.Teacher).ToListAsync();
        }
    }
}
