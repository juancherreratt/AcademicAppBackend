using Core.Application.DTOs;

namespace Core.Application.Interfaces
{
    public interface ISubjectService
    {
        Task<List<SubjectDto>> GetAllSubjectsAsync();
    }
}
