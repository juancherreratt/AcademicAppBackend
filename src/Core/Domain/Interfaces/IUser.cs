namespace Core.Domain.Interfaces
{
    public interface IUser
    {
        string? Id { get; set; }
        string Email { get; set; }
    }
}
