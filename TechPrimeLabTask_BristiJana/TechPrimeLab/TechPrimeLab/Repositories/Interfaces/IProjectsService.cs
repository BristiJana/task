using TechPrimeLab.Models;
using TechPrimeLab.ViewModel;

namespace TechPrimeLab.Repositories.Interfaces
{
    public interface IProjectsService
    {
        Task<ResponseVM> CreateProject(ProjectDetails projectDetails);
        Task<ResponseVM> GetProjectsByUserId(Guid UserId);
        Task<ResponseVM> GetAllStatusCountsByUserId(Guid UserId);
        Task<ResponseVM> ChangeProjectStatus(ChangeProjectStatusVM request);
        Task<ResponseVM> DepartmentWiseTotalVsClosed(Guid UserId);
    }
}
