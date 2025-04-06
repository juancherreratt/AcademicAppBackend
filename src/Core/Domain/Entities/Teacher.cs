using Core.Domain.Interfaces;

namespace Core.Domain.Entities
{
    public class Teacher
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public IUser? User { get; set; }

        public ICollection<Subject> Subjects { get; set; } = new List<Subject>();
    }
}
