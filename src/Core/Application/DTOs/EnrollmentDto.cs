namespace Core.Application.DTOs
{
    public class EnrollmentDto
    {
        public Guid EnrollmentId { get; set; }
        public Guid SubjectId { get; set; }
        public string SubjectName { get; set; } = string.Empty;
        public int SubjectCredits { get; set; }
        public string TeacherName { get; set; } = string.Empty;
    }
}
