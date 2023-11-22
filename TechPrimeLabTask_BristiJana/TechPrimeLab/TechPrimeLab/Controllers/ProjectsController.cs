using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechPrimeLab.Models;
using TechPrimeLab.Repositories.Interfaces;
using TechPrimeLab.ViewModel;

namespace TechPrimeLab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectsService _projectsService;

        public ProjectsController(IProjectsService projectsService)
        {
            _projectsService = projectsService;
        }

        [HttpPost("CreateProject")]
        public async Task<ActionResult> CreateProject(ProjectDetails projectDetails)
        {
            var result = await _projectsService.CreateProject(projectDetails);
            return Ok(result);
        }

        [HttpGet("GetProjectsByUserId/{UserId}")]
        public async Task<IActionResult> GetProjectsByUserId(Guid UserId)
        {
            ResponseVM result = await _projectsService.GetProjectsByUserId(UserId);
            return Ok(result);
        }

        [HttpGet("GetAllStatusCountsByUserId/{UserId}")]
        public async Task<IActionResult> GetAllStatusCounts(Guid UserId)
        {
            ResponseVM result = await _projectsService.GetAllStatusCountsByUserId(UserId);
            return Ok(result);
        }

        [HttpPut("ChangeProjectStatus")]
        public async Task<IActionResult> ChangeProjectStatus(ChangeProjectStatusVM request)
        {
            ResponseVM result = await _projectsService.ChangeProjectStatus(request);
            return Ok(result);
        }

        [HttpGet("DepartmentWiseTotalVsClosed/{UserId}")]
        public async Task<IActionResult> DepartmentWiseTotalVsClosed(Guid UserId)
        {
            ResponseVM result = await _projectsService.DepartmentWiseTotalVsClosed(UserId);
            return Ok(result);
        }
    }
}
