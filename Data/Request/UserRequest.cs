namespace DevManager.Data.Request;

public class UserRequest
{
    public int UserID { get; set; }
    public string? Password { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string UserRole { get; set; } = null!; // Nuevo campo para el rol del usuario
}
