using Core.Domain.Interfaces;

namespace Core.Domain.Entities
{
    public class Student
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public IUser? User { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
