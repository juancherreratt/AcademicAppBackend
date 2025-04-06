using Core.Application.DTOs;
using Core.Application.Interfaces;
using Core.Domain.Interfaces;

namespace Core.Application.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;

        public SubjectService(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        public async Task<List<SubjectDto>> GetAllSubjectsAsync()
        {
            var subjects = await _subjectRepository.GetAllWithTeacherAsync();
            return subjects.Select(s => new SubjectDto
            {
                Id = s.Id,
                Name = s.Name,
                Credits = s.Credits,
                Teacher = new TeacherDto 
                { 
                    Id = s.Teacher.Id,
                    Name = s.Teacher.Name
                }
            }).ToList();
        }
    }
}
