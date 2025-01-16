using Microsoft.AspNetCore.Mvc;
using PreschoolApp.DTO;
using PreschoolApp.Models;
using PreschoolApp.Services.Interfaces;

namespace PreschoolApp.Controllers
{
    [ApiController]
    [Route("api/groups")]
    public class GroupsController : Controller
    {
        private readonly IGroupService _groupService;

        public GroupsController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGroups()
        {
            var groups = await _groupService.GetAllGroupsAsync();
            return Ok(groups);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGroup(int id)
        {
            try
            {
                var groupItem = await _groupService.GetGroupByIdAsync(id);
                return Ok(groupItem);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateGroup([FromBody] GroupDTO groupDto)
        {
            var group = new Group
            {
                Name = groupDto.GroupName,
                CreatedAt = DateTime.UtcNow,
                TeacherGroups = new List<TeacherGroup>(),
                StudentGroups = new List<StudentGroup>()
            };
            
            var createdGroup = await _groupService.CreateGroupAsync(group);
            return Ok(createdGroup);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGroup(int id, [FromBody] Group group)
        {
            var success = await _groupService.UpdateGroupAsync(id, group);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroup(int id)
        {
            var success = await _groupService.DeleteGroupAsync(id);
            if(!success) return NotFound();
            return NoContent();
        }
    }
}