namespace Core.Application.DTOs
{
    public class EnrollmentRequest
    {
        //public string UserId { get; set; } = string.Empty;
        public List<Guid> SubjectIds { get; set; } = new();
    }
}
