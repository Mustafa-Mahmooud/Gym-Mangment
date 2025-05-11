
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Presentation.Interfaces;
using Repo;
using Repo.Data;
using Repo.Data.Generic;

namespace Presentation
{
    public class Program
    {
        public static  async Task Main(string[] args)//s
        {
            var builder = WebApplication.CreateBuilder(args);


            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();



            //DI
            builder.Services.AddDbContext<GymContext>(options =>
                 options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped(typeof(IGeneric<>), typeof(GenericRepo<>));
            builder.Services.AddAutoMapper(typeof(Program));



            var app = builder.Build();

            #region Database Migration and Seeding
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var _dbContext = services.GetRequiredService<GymContext>();
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();

            try
            {
               await _dbContext.Database.MigrateAsync();

                // Seed the database with initial data if needed
                await GymContextSeed.SeedAsync(_dbContext);

            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An error occurred during migration or seeding.");
            }
            #endregion

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
