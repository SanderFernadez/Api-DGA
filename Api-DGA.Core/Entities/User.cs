using Api_DGA.Core.Common;
using System.ComponentModel.DataAnnotations;

namespace Api_DGA.Core.Entities
{
    public class User : CommontFields
    {
        public new int Id { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        public string PasswordHash { get; set; } = string.Empty;
        
        public string? RefreshToken { get; set; }
        
        public DateTime? RefreshTokenExpiryTime { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime? LastLoginAt { get; set; }
        
        // Relación con roles
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
