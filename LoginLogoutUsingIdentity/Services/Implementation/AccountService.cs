using LoginLogoutUsingIdentity.Data;
using LoginLogoutUsingIdentity.Models;
using LoginLogoutUsingIdentity.Services.Abstration;
using LoginLogoutUsingIdentity.ViewModels;
using LoginLogoutUsingIdentity.ViewModels.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Security.Cryptography;

namespace LoginLogoutUsingIdentity.Services.Implementation
{
    public class AccountService:IAccountService,IDisposable
    {
        private readonly DataContext _context;
        public AccountService(DataContext context)
        {
            _context = context;
        }

       
        public void Dispose()
        {
           // throw new NotImplementedException();
        }

        public Task<Status> LoginAsync(LoginVM loginVM)
        {
            throw new NotImplementedException();
        }

        public Task<Status> LogoutAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Status> RegistrationAsync(RegistrationVM registerVM)
        {
            throw new NotImplementedException();
        }
    }
}
