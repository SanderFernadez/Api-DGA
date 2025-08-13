using Api_DGA.Core.Common;

namespace Api_DGA.Core.Entities
{
    public class Role : CommontFields
    {
        public new int Id { get; set; }
        
        public string Description { get; set; } = string.Empty;
        
        public bool IsActive { get; set; } = true;
        
        // Relación con usuarios
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
