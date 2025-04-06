namespace Core.Domain.Entities
{
    public class Enrollment
    {
        public Guid Id { get; set; }

        public Guid StudentId { get; set; }
        public Student? Student { get; set; }

        public Guid SubjectId { get; set; }
        public Subject? Subject { get; set; }
    }
}
