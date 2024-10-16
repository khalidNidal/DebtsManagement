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
        }
    }
}