using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeApi.Application.Interfaces.Repositories;
using YoutubeApi.Application.Interfaces.UnitOfWorks;
using YoutubeApi.Persistence.Context;
using YoutubeApi.Persistence.Repositories;
using YoutubeApi.Persistence.UnitOfWorks;

namespace YoutubeApi.Persistence
{
    public static class Registration
    {
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(opt =>
            opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))); //veritabanını bağladık.

            services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>)); // IReadRepository'yi ReadRepository ile ilişkilendirdik.
            services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));//IWriteRepository'yi WriteRepository ile ilişkilendirdik.
            services.AddScoped<IUnitOfWork, UnitOfWork>(); //IUnitOfWork'yi UnitOfWork ile ilişkilendirdik.
        }

    }
}
