

using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Api_DGA.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
          // services.AddAutoMapper(Assembly.GetExecutingAssembly());


            #region Services
            //services.AddTransient(typeof(IGenericService<,,>), typeof(GenericService<,,>));
            //services.AddTransient<IIngredientService, IngredientService>();
            //services.AddTransient<IOrderService, OrderService>();
            //services.AddTransient<IPlateService, PlateService>();
            //services.AddTransient<ITableService, TableService>();
            //services.AddTransient<IPlateService, PlateService>();
            //services.AddTransient<IPlateIngredientService, PlateIngredientService>();
            #endregion
        }
    }
}
