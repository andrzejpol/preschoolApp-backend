using Microsoft.EntityFrameworkCore;
using PreschoolApp.Data;
using PreschoolApp.Models;
using PreschoolApp.Services.Interfaces;

public class GroupService : IGroupService
{
    private readonly PreschoolDbContext _applicationDbContext;

    public GroupService(PreschoolDbContext dbContext)
    {
        _applicationDbContext = dbContext;
    }
    public async Task<List<Group>> GetAllGroupsAsync()
    {
        return await _applicationDbContext.Groups
            .Include(g => g.TeacherGroups)
            .Include(g => g.StudentGroups)
            .ToListAsync();
    }


    public Task<Group> GetGroupByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Group> CreateGroupAsync(Group group)
    {
        _applicationDbContext.Groups.Add(group);
        await _applicationDbContext.SaveChangesAsync();
        return group;
    }

    public Task<bool> UpdateGroupAsync(int id, Group updatedGroup)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteGroupAsync(int id)
    {
        var group = await _applicationDbContext.Groups.FindAsync(id);
        if (group == null)
        {
            return false;
        }
        
        _applicationDbContext.Groups.Remove(group);
        
        await _applicationDbContext.SaveChangesAsync();

        return true;
    }
}