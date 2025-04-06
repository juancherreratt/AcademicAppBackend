using Core.Application.DTOs;
using Core.Application.Interfaces;
using Core.Domain.Entities;
using Core.Domain.Interfaces;

namespace Core.Application.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IRepository<Subject> _subjectRepository;
        private readonly IRepository<Student> _studentRepository;
        private readonly IRepository<Teacher> _teacherRepository;

        public EnrollmentService(
            IEnrollmentRepository enrollmentRepository,
            IRepository<Subject> subjectRepository,
            IRepository<Student> studentRepository,
            IRepository<Teacher> teacherRepository)
        {
            _enrollmentRepository = enrollmentRepository;
            _subjectRepository = subjectRepository;
            _studentRepository = studentRepository;
            _teacherRepository = teacherRepository;
        }

        public async Task<GenericResponse<List<EnrollmentDto>>> GetMyEnrollmentsAsync(string userId)
        {
            var student = await GetStudentByUserId(userId);
            if (student is null)
                return new(false, "Student not found", new());

            var enrollments = (await _enrollmentRepository.GetAllAsync())
                .Where(e => e.StudentId == student.Id)
                .ToList();

            var subjects = await _subjectRepository.GetAllAsync();
            var teachers = await _teacherRepository.GetAllAsync();

            var result = enrollments.Select(e =>
            {
                var subject = subjects.FirstOrDefault(s => s.Id == e.SubjectId);
                var teacher = teachers.FirstOrDefault(t => t.Id == subject!.TeacherId);

                return new EnrollmentDto
                {
                    EnrollmentId = e.Id,
                    SubjectId = subject!.Id,
                    SubjectName = subject.Name,
                    SubjectCredits = subject.Credits,
                    TeacherName = teacher?.Name ?? "N/A"
                };
            }).ToList();

            return new(true, "Enrollments retrieved", result);
        }

        public async Task<List<StudentDto>> GetStudentsBySubjectAsync(Guid subjectId)
        {
            var students = await _enrollmentRepository.GetStudentsBySubjectAsync(subjectId);

            return students.Select(s => new StudentDto
            {
                Id = s.Id,
                Name = s.Name
            }).ToList();
        }
        public async Task<GenericResponse<string>> EnrollSubjectsAsync(EnrollmentRequest request, string userId)
        {
            var student = await GetStudentByUserId(userId);
            if (student is null)
                return new(false, "Student not found", null);

            var subjectValidation = await ValidateSubjectsAsync(request.SubjectIds);
            if (!subjectValidation.Success)
                return new(false, subjectValidation.Message, null);

            await InsertEnrollmentsAsync(subjectValidation.Data!, student.Id);

            return new(true, "Enrollment successful", null);
        }

        public async Task<GenericResponse<string>> UpdateEnrollSubjectsAsync(EnrollmentRequest request, string userId)
        {
            var student = await GetStudentByUserId(userId);
            if (student is null)
                return new(false, "Student not found", null);

            var subjectValidation = await ValidateSubjectsAsync(request.SubjectIds);
            if (!subjectValidation.Success)
                return new(false, subjectValidation.Message, null);

            var existingEnrollments = await _enrollmentRepository
                .FindAllByStudentIdAsync(student.Id);

            foreach (var enrollment in existingEnrollments)
            {
                await _enrollmentRepository.DeleteAsync(enrollment.Id);
            }

            await InsertEnrollmentsAsync(subjectValidation.Data!, student.Id);

            return new(true, "Enrollment updated successfully", null);
        }

        public async Task<GenericResponse<string>> CancelEnrollmentAsync(string userId)
        {
            var student = await GetStudentByUserId(userId);
            if (student is null)
                return new(false, "Student not found", null);

            var enrollments = await _enrollmentRepository
                .FindAllByStudentIdAsync(student.Id);

            if (!enrollments.Any())
                return new(false, "No enrollments found to delete", null);

            foreach (var enrollment in enrollments)
            {
                await _enrollmentRepository.DeleteAsync(enrollment.Id);
            }

            return new(true, "Enrollment cancelled successfully", null);
        }

        private async Task<Student?> GetStudentByUserId(string userId)
        {
            var students = await _studentRepository.GetAllAsync();
            return students.FirstOrDefault(s => s.UserId == userId);
        }

        private async Task<GenericResponse<List<Subject>>> ValidateSubjectsAsync(List<Guid> subjectIds)
        {
            if (subjectIds.Count != 3)
                return new(false, "You must select exactly 3 subjects (9 credits)", null);

            var subjects = (await _subjectRepository.GetAllAsync())
                .Where(s => subjectIds.Contains(s.Id)).ToList();

            if (subjects.Count != 3)
                return new(false, "Some subjects do not exist", null);

            var teacherIds = subjects.Select(s => s.TeacherId).Distinct();
            if (teacherIds.Count() != 3)
                return new(false, "You must select subjects from different teachers", null);

            return new(true, "Valid subjects", subjects);
        }

        private async Task InsertEnrollmentsAsync(List<Subject> subjects, Guid studentId)
        {
            var enrollments = subjects.Select(s => new Enrollment
            {
                Id = Guid.NewGuid(),
                StudentId = studentId,
                SubjectId = s.Id
            }).ToList();

            foreach (var enrollment in enrollments)
            {
                await _enrollmentRepository.AddAsync(enrollment);
            }
        }
    }
}
