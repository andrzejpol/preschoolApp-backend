using PreschoolApp.Models;

namespace PreschoolApp.Services.Interfaces;

public interface IGroupService
{
    Task<List<Group>> GetAllGroupsAsync();
    Task<Group> GetGroupByIdAsync(int id);
    Task<Group> CreateGroupAsync(Group group);
    Task<bool> UpdateGroupAsync(int id, Group updatedGroup);
    Task<bool> DeleteGroupAsync(int id);
}