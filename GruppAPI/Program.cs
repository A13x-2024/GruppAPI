
using GruppAPI.Data;
using GruppAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GruppAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddDbContext<TeamDBContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<TeamServices>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapPost("/team", async (Team team, TeamServices service) =>
            {
                await service.AddTeam(team);
                return Results.Ok();
            });

            app.MapGet("/teams", async (TeamServices service) =>
            {
                var getAll = await service.GetTeams();
                return Results.Ok(getAll);
            });

            app.MapPut("/team/{id}", async (int id, Team team, TeamServices service) =>
            {
                var updatedTeam = await service.UpdateTeam(id, team);
                if (updatedTeam == null)
                    return Results.NotFound("Team not found");

                return Results.Ok(updatedTeam);
            });

            app.MapDelete("/team/{id}", async(int id, TeamServices service) =>
            { 
                var isDeleted = await service.DeleteTeam(id);
                if (isDeleted == null)
                {
                    return Results.NotFound("Team member not found");
                }
                return Results.Ok(isDeleted);
            });

            app.Run();
        }
    }
}
