using Core.Entities;
using Core.Repositories;
using Infrastructure.EF;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AcmeCorpBizDbContext>(options =>
                        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IGenericRepository<Customer, int>, CustomerRepository>();
            services.AddScoped<IGenericRepository<Address, int>, AddressRepository>();
            services.AddScoped<IGenericRepository<Product, int>, ProductRepository>();
            services.AddScoped<IGenericRepository<Order, int>, OrderRepository>();
            services.AddScoped<IGenericRepository<OrderDetail, OrderDetailKey>, OrderDetailRepository>();

            services.AddScoped<IAcmeCorpBizService, AcmeCorpBizService>();

            return services;
        }
    }
}
