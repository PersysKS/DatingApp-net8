﻿using API.Data;
using API.Interface;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationService(this IServiceCollection service,
       IConfiguration config)
    {
        service.AddControllers();
        service.AddDbContext<DataContext>(opt =>
        {
            opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
        });
        service.AddCors();
        service.AddScoped<ITokenService, TokenService>();
        service.AddScoped<IUserRepository, UserRepository>();
        service.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        return service;
    }

}
