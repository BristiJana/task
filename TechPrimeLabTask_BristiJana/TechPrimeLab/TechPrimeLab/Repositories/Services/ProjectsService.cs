using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using TechPrimeLab.Data;
using TechPrimeLab.Helpers.Constants;
using TechPrimeLab.Models;
using TechPrimeLab.Repositories.Interfaces;
using TechPrimeLab.ViewModel;

namespace TechPrimeLab.Repositories.Services
{
    public class ProjectsService : IProjectsService
    {
        private readonly TechPrimeDBContext _techPrimeLabDBContext;

        public ProjectsService(TechPrimeDBContext techPrimeLabDBContext)
        {
            _techPrimeLabDBContext = techPrimeLabDBContext;
        }

        #region ChangeProjectStatus
        public async Task<ResponseVM> ChangeProjectStatus(ChangeProjectStatusVM request)
        {
            ProjectDetails projectDetails = await _techPrimeLabDBContext.project_details
                .FirstOrDefaultAsync(pd => pd.project_user_id == request.project_user_id && pd.project_id == request.project_id);

            if(projectDetails is null)
                return new ResponseVM(true, StatusCodes.Status400BadRequest, "Project Not Found!", string.Empty);

            projectDetails.project_status = request.project_status;
            await _techPrimeLabDBContext.SaveChangesAsync();

            return new ResponseVM(true, StatusCodes.Status200OK, "Status Updated", JsonSerializer.Serialize(projectDetails));
        } 
        #endregion

        #region CreateProject
        public async Task<ResponseVM> CreateProject(ProjectDetails projectDetails)
        {
            var projectId = Guid.NewGuid();
            ProjectDetails pd = new ProjectDetails
            {
                project_id = projectId,
                project_name = projectDetails.project_name,
                project_user_id = projectDetails.project_user_id,
                project_department = projectDetails.project_department,
                project_category = projectDetails.project_category,
                project_division = projectDetails.project_division,
                project_location = projectDetails.project_location,
                project_priority = projectDetails.project_priority,
                project_reason = projectDetails.project_reason,
                project_type = projectDetails.project_type,
                project_theme = projectDetails.project_theme,
                project_status = projectDetails.project_status,
                start_date = projectDetails.start_date,
                end_date = projectDetails.end_date,
            };

            _techPrimeLabDBContext.project_details.Add(pd);
            await _techPrimeLabDBContext.SaveChangesAsync();

            return new ResponseVM(true, StatusCodes.Status200OK, "Project created!", JsonSerializer.Serialize(pd));
        } 
        #endregion

        #region GetAllStatusCountsByUserId
        public async Task<ResponseVM> GetAllStatusCountsByUserId(Guid UserId)
        {
            var projects = await GetProjectMethod(UserId);
            if(projects is null)
                return new ResponseVM(true, StatusCodes.Status400BadRequest, "This user don't have projects.", new object[] { });

            DashboardStatusVM dashboardStatus = new DashboardStatusVM
            {
                total_projects = projects.Count(),
                closed_projects = projects.Where(x => x.project_status == "Closed").Count(),
                closure_delay_projects = projects.Where(x => x.end_date < DateTime.UtcNow.Date && x.project_status == "Running").Count(),
                canceled_projects = projects.Where(x => x.project_status == "Cancelled").Count(),
                running_projects = projects.Where(x => x.project_status == "Running").Count()
            };

            return new ResponseVM(true, StatusCodes.Status200OK, ResponseMessage.Success, JsonSerializer.Serialize(dashboardStatus));
        } 
        #endregion

        #region GetProjectsByuserId
        public async Task<ResponseVM> GetProjectsByUserId(Guid UserId)
        {
            var projects = await GetProjectMethod(UserId);
            
            if (projects is null)
                return new ResponseVM(true, StatusCodes.Status400BadRequest, "This user don't have projects.", new object[] { });

            return new ResponseVM(true, StatusCodes.Status200OK, ResponseMessage.Success, JsonSerializer.Serialize(projects));
        } 
        #endregion

        #region Get Projects By User Id Private Method
        private async Task<List<ProjectDetails>> GetProjectMethod(Guid UserId)
        {
            List<ProjectDetails> projects = await _techPrimeLabDBContext.project_details.Where(x => x.project_user_id == UserId).ToListAsync();
            return projects;
        }
        #endregion

        #region DepartmentWiseTotalVsClosed
        public async Task<ResponseVM> DepartmentWiseTotalVsClosed(Guid UserId)
        {
            var projectCounts = from pd in _techPrimeLabDBContext.project_details
                                where pd.project_user_id == UserId
                                group pd by pd.project_department into departmentGroup
                                select new
                                {
                                    DepartmentName = departmentGroup.Key,
                                    TotalProjects = departmentGroup.Count(),
                                    ClosedProjects = departmentGroup.Count(p => p.project_status == "Closed")
                                };

            return new ResponseVM(true, StatusCodes.Status200OK, ResponseMessage.Success, JsonSerializer.Serialize(projectCounts));
        } 
        #endregion
    }
}
