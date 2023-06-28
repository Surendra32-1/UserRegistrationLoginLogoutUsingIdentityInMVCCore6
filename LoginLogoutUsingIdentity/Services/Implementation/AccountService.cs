using LoginLogoutUsingIdentity.Data;
using LoginLogoutUsingIdentity.Models;
using LoginLogoutUsingIdentity.Services.Abstration;
using LoginLogoutUsingIdentity.ViewModels;
using LoginLogoutUsingIdentity.ViewModels.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Security.Claims;
using System.Security.Cryptography;

namespace LoginLogoutUsingIdentity.Services.Implementation
{
    public class AccountService:IAccountService,IDisposable
    {
        //private readonly DataContext _context;
        //public AccountService(DataContext context)
        //{
        //    _context = context;
        //}
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<Result> RegistrationAsync(RegistrationVM registerVM)
        {
            var result = new Result();
            //throw new NotImplementedException();
           var existUserData=await _userManager.FindByNameAsync(registerVM.UserName);
            if (existUserData != null)
            {
                result.status = Status.Failure;
                result.Message = "User Already Exists!";
                return result; 
            }
            //Taking Data from form and putting in new ApplicationUser
            ApplicationUser user = new ApplicationUser()
            {
                SecurityStamp=Guid.NewGuid().ToString(),
                Name = registerVM.Name,
                UserName = registerVM.UserName,
                Email = registerVM.Email,
                EmailConfirmed=true
            };
            //Create User
            var userData =await _userManager.CreateAsync(user,registerVM.Password);
            if(!(userData.Succeeded))
            {
                result.status = Status.Failure;
                result.Message = "Unable to create user!";
                return result;
            }
            //For Role
            //RoleExistsAsync return true or false
            if (!(await _roleManager.RoleExistsAsync(registerVM.Role)))
            {
                //Assign userRole ie:Name is  roleName in IdentityRole
               await _roleManager.CreateAsync(new IdentityRole(registerVM.Role));
                
            }
            if ((await _roleManager.RoleExistsAsync(registerVM.Role)))
            {
                //Adding role in user Manager
                await _userManager.AddToRoleAsync(user,registerVM.Role);
            }
            result.status = Status.Success;
            result.Message = "User is Successfully Registered, Thank You!";
            return result;

        }

        public void Dispose()
        {
           // throw new NotImplementedException();
        }

        public async  Task<Result> LoginAsync(LoginVM loginVM)
        {
           var result=new Result();
           var userData=await _userManager.FindByEmailAsync(loginVM.Email);
            if(userData==null)
            {
                result.status = Status.Failure;
                result.Message = "Invalid Email.";
            }
            //It will check by taking userData and it convert itself 
           var  check = await _userManager.CheckPasswordAsync(userData, loginVM.Password);
            if (!check)
            {
                result.status = Status.Failure;
                result.Message = "Invalid Password.";
            }
            var signInData = await _signInManager.PasswordSignInAsync(userData, loginVM.Password, true, false);
            if (signInData.Succeeded)
            {
                //Getting all Roles from userManager
                var userRoles = await _userManager.GetRolesAsync(userData);
                //Adding Cliams
                var authClaim = new List<Claim>
                { 
                    new Claim(ClaimTypes.Name,userData.UserName)
                };
                //adding all Role in Claims
                foreach(var userRole in userRoles)
                {
                    authClaim.Add(new Claim(ClaimTypes.Role,userRole));
                }

                result.status = Status.Success;
                result.Message = "Successfully Login!";
                return result;
            }
            else if(signInData.IsLockedOut)
            {/*When a user exceeds the maximum number of allowed failed login attempts within a specified period, 
              * the user's account can be locked out as a security measure.The IsLockedOut property provides information 
              * about the current lockout status of a user account.*/

                result.status = Status.Failure;
                result.Message = "User Is Locked Out.";
                return result;
            }
            else
            {
                result.status = Status.Failure;
                result.Message = "Error ,Unable to Login .";
                return result;
            }
        }

        public async Task LogoutAsync()
        {
            //sign out the user
           await _signInManager.SignOutAsync();

        }

        
    }
}
