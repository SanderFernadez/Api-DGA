

namespace Api_DGA.Core.Entities
{
    public class Client
    {

        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        // Relación con ventas
        public ICollection<Sale> Sales { get; set; } = new List<Sale>();

    }
}
