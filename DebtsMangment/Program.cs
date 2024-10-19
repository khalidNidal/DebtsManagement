using DebtsManagement.API.mapping;
using DebtsMangment.Infastructure.Data;
using EC.Core.Response;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DebtsMangment
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            

            // Add services to the container.
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowLocalhost",
                    builder => builder.WithOrigins("http://127.0.0.1:5500")
                                      .AllowAnyMethod()
                                      .AllowAnyHeader());
            });

            builder.Services.AddControllers();
            builder.Services.AddDbContext<AppDbContext>(op =>
            {
                op.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            }
            );
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddScoped(typeof(ApiResponse));
            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.AddScoped(typeof(ApiValidationResponse));



            var app = builder.Build();


            app.UseExceptionHandler("/error");
            app.Map("error", (HttpContext httpContext) =>
            {
                var exceptionHandlerPathFeature = httpContext.Features.Get<IExceptionHandlerPathFeature>()?.Error;

                var StatusCode = exceptionHandlerPathFeature switch
                {
                  
                    ValidationException => StatusCodes.Status422UnprocessableEntity,
                    _ => StatusCodes.Status500InternalServerError
                };

                return Results.Problem(exceptionHandlerPathFeature?.Message, statusCode: StatusCode, instance: $"{httpContext.Request.Method} {httpContext.Request.Path}");
            });

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("AllowLocalhost");

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            

            app.Run();








        }
    }
}






/*
 
 
 
 
 using DebtsManagement.API.mapping;
using DebtsMangment.Infastructure.Data;
using EC.Core.Response;
using Microsoft.EntityFrameworkCore;

namespace DebtsMangment
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            

            // Add services to the container.
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowLocalhost",
                    builder => builder.WithOrigins("http://127.0.0.1:5500")
                                      .AllowAnyMethod()
                                      .AllowAnyHeader());
            });
            builder.Services.AddControllers();
            builder.Services.AddDbContext<AppDbContext>(op =>
            {
                op.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            }
            );
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddScoped(typeof(ApiResponse));
            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.AddScoped(typeof(ApiValidationResponse));



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
            app.UseCors("AllowLocalhost");








        }
    }
}
 
 
 
 */