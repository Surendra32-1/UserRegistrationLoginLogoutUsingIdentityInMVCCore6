using LoginLogoutUsingIdentity.ViewModels;
using LoginLogoutUsingIdentity.ViewModels.DTO;


namespace LoginLogoutUsingIdentity.Services.Abstration
{
    public interface IAccountService
    {
        Task<Result> LoginAsync(LoginVM loginVM);
        Task<Result> RegistrationAsync(RegistrationVM registerVM);
        Task LogoutAsync();


    }
}
