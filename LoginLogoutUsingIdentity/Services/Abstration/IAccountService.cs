using LoginLogoutUsingIdentity.ViewModels;
using LoginLogoutUsingIdentity.ViewModels.DTO;


namespace LoginLogoutUsingIdentity.Services.Abstration
{
    public interface IAccountService
    {
        Task<Status> LoginAsync(LoginVM loginVM);
        Task<Status> RegistrationAsync(RegistrationVM registerVM);
        Task<Status> LogoutAsync();


    }
}
