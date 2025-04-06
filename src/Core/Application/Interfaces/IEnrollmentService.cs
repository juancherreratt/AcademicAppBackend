using Core.Application.DTOs;

namespace Core.Application.Interfaces
{
    public interface IEnrollmentService
    {
        Task<GenericResponse<List<EnrollmentDto>>> GetMyEnrollmentsAsync(string userId);
        Task<List<StudentDto>> GetStudentsBySubjectAsync(Guid subjectId);
        Task<GenericResponse<string>> EnrollSubjectsAsync(EnrollmentRequest request, string userId);
        Task<GenericResponse<string>> UpdateEnrollSubjectsAsync(EnrollmentRequest request, string userId);
        Task<GenericResponse<string>> CancelEnrollmentAsync(string userId);
    }
}
