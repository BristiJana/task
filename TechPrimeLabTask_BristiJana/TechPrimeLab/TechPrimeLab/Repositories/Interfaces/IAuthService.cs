using TechPrimeLab.ViewModel;

namespace TechPrimeLab.Repositories.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseVM> RegisterUser(string user_name, UserVM request);
        Task<ResponseVM> Login(UserVM request);
        Task<ResponseVM> ForgotPassword(UserVM request);
    }
}
