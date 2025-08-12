namespace APi_DGA_Infrastructure.Seeders
{
    /// <summary>
    /// Interfaz para los seeders de datos
    /// </summary>
    public interface IDataSeeder
    {
        /// <summary>
        /// Ejecuta el seeding de datos
        /// </summary>
        /// <returns>Task</returns>
        Task SeedAsync();
    }
}
