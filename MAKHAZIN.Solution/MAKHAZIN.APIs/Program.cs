
using MAKHAZIN.APIs.Extensions;
using MAKHAZIN.APIs.Middlewares;
using MAKHAZIN.Repository;
using Microsoft.EntityFrameworkCore;

namespace MAKHAZIN.APIs
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var webApplicationBuilder = WebApplication.CreateBuilder(args);

            #region Configure Services

            webApplicationBuilder.Services.AddControllers();
            webApplicationBuilder.Services.AddEndpointsApiExplorer();
            webApplicationBuilder.Services.AddSwaggerGen();

            webApplicationBuilder.Services.AddDbContext<MAKHAZINDbContext>(options =>
            {
                options.UseSqlServer(webApplicationBuilder.Configuration.GetConnectionString("DefaultConnection"));
            });
            webApplicationBuilder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining<MAKHAZINDbContext>();
            });
            webApplicationBuilder.Services.AddApplicationServices();
            #endregion

            var app = webApplicationBuilder.Build();

            #region Configure Middlewares
            app.UseMiddleware<ExceptionMiddleware>();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers(); 
            #endregion

            app.Run();
        }
    }
}
