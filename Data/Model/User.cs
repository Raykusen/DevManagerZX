using DevManager.Data.Request;
using DevManager.Data.Response;
using System.ComponentModel.DataAnnotations;

namespace DevManager.Data.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        [Required]
        public string? PasswordHash { get; set; }
        [Required]
        public string FirstName { get; set; } = null!;
        [Required]
        public string LastName { get; set; } = null!;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string UserRole { get; set; } = null!; // Nuevo campo para el rol del usuario

        public static User Crear(UserRequest user) => new()
        {
            PasswordHash = user.Password,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            CreatedAt = user.CreatedAt,
            UserRole = user.UserRole
        };

        public bool Modificar(UserRequest user)
        {
            var cambio = false;
            if (FirstName != user.FirstName) FirstName = user.FirstName; cambio = true;
            if (LastName != user.LastName) LastName = user.LastName; cambio = true;
            if (Email != user.Email) Email = user.Email; cambio = true;
            if (PasswordHash != user.Password) PasswordHash = user.Password; cambio = true;
            if (UserRole != user.UserRole) UserRole = user.UserRole; cambio = true;
            return cambio;
        }

        public UserResponse ToResponse() => new()
        {
            UserID = UserID,
            Password = PasswordHash,
            FirstName = FirstName,
            LastName = LastName,
            Email = Email,
            CreatedAt = CreatedAt,
            UserRole = UserRole
        };
    }
}
