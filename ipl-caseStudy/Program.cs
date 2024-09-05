
using IPL.DataAccessLayer;
using Npgsql;

namespace IPL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("IplDatabase");

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IIplDAO, IplDAO>();
            builder.Services.AddScoped(
                (provider) => new NpgsqlConnection(connectionString)
            );
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
                options.AddPolicy("FrontEnd", builder =>
                {
                    builder.WithOrigins("http://localhost:3000")
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });


            var app = builder.Build();

            app.UseCors();

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

/*
 * Create action methods to retrieve the specified data:
    1. Create an action method to inserts a new Player in the database.
    2. Retrieve detailed statistics for each match, including team names, venue, match date, and
    the total number of fan engagements for each match.
    3. Retrieve the top 5 players based on the number of matches played who have participated in
    matches with the highest fan engagements.
    4. Retrieve all matches that were played within a specific date range. This endpoint retrieves a
    list of matches that took place between two specified dates, including match details such as
    date, venue, and teams involved.
*/