namespace Core.Application.DTOs
{
    public class SubjectDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Credits { get; set; }
        public TeacherDto? Teacher { get; set; }
    }
}
