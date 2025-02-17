using GruppAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GruppAPI.Data
{
    public class TeamServices
    {
        private readonly TeamDBContext _db;


        public TeamServices(TeamDBContext dbContext)
        {
            _db = dbContext;
        }

        public async Task AddTeam(Team team)
        {
            await _db.Teams.AddAsync(team);
            await _db.SaveChangesAsync();
        }

        public async Task<List<Team>> GetTeams()
        {
            return await _db.Teams.ToListAsync();
        }

        public async Task<Team> UpdateTeam(int id, Team updatedTeam)
        {
            var team = await _db.Teams.FirstOrDefaultAsync(x => x.Id == id);
            if (team == null) return null;
            team.FirstName = updatedTeam.FirstName;
            team.LastName = updatedTeam.LastName;
            team.Age = updatedTeam.Age;
            await _db.SaveChangesAsync();
            return team;

        }

        public async Task<Team> DeleteTeam(int id)
        {
            var deleteTeam = await _db.Teams.FirstOrDefaultAsync(x => x.Id == id);
            _db.Teams.Remove(deleteTeam);
            await _db.SaveChangesAsync();
            return deleteTeam;
        }
    }
}
