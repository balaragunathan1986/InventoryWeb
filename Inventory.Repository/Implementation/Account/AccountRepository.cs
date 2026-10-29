using Inventory.Domain.DTO;
using Inventory.Domain.Model.Account;
using Inventory.Repository.Data;
using Inventory.Repository.Interfaces.Account;
using Inventory.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;


namespace Inventory.Repository.Implementation.Account
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDBContext _dbcontext;
        private readonly IConfiguration _configuration;
        public AccountRepository(
            AppDBContext _context,
            IConfiguration configuration

            )
        {
            _dbcontext = _context;
            _configuration = configuration;
        }

        public async Task<object> login(UserModel usermodel)
        {
            // Find user by username OR email
            var user = await _dbcontext.Users.FirstOrDefaultAsync(x =>
                x.User_Name == usermodel.User_Name ||
                x.Email == usermodel.User_Name);

            // Don't reveal whether the username/email exists
            if (user == null)
            {
                return new
                {
                    success = false,
                    message = "Invalid username or password."
                };
            }

            // Check if account is currently locked
            if (user.IsLocked == true && user.LockoutEnd.HasValue)
            {
                if (user.LockoutEnd.Value > DateTime.UtcNow)
                {
                    var remainingMinutes =
                        (int)Math.Ceiling(
                            (user.LockoutEnd.Value - DateTime.UtcNow).TotalMinutes
                        );

                    return new
                    {
                        success = false,
                        message = $"Account is locked. Try again in {remainingMinutes} minute(s)."
                    };
                }

                // Lockout period has expired
                user.IsLocked = false;
                user.FailedLoginAttempts = 0;
                user.LockoutEnd = null;

                await _dbcontext.SaveChangesAsync();
            }

            // Validate password
            var encryptedPassword = Security.Encrypt(usermodel.Password);

            if (user.Password != encryptedPassword)
            {
                // Increase failed login attempts
                user.FailedLoginAttempts++;

                // Lock account after 5 failed attempts
                if (user.FailedLoginAttempts >= 5)
                {
                    user.IsLocked = true;
                    user.LockoutEnd = DateTime.UtcNow.AddMinutes(15);

                    await _dbcontext.SaveChangesAsync();

                    return new
                    {
                        success = false,
                        message = "Too many failed login attempts. Account locked for 2 minutes."
                    };
                }

                await _dbcontext.SaveChangesAsync();

                var attemptsRemaining = 5 - user.FailedLoginAttempts;

                return new
                {
                    success = false,
                    message = $"Invalid username or password. {attemptsRemaining} attempt(s) remaining."
                };
            }

            // Successful login - reset lockout information
            user.FailedLoginAttempts = 0;
            user.IsLocked = false;
            user.LockoutEnd = null;
            user.Last_LoggedIn = DateTime.UtcNow;

            await _dbcontext.SaveChangesAsync();

            // Generate JWT token
            string token = GenerateJwtToken(user.User_Name);

            // Get role and permissions
            var userObject = new
            {
                user_Id = user.User_Id,
                userName = user.User_Name,
                email = user.Email,
                first_Name = user.First_Name,
                last_Name = user.Last_Name,
                last_LoggedIn = user.Last_LoggedIn,
                role_Id = user.Role_Id,
                role_Name = await GetRoleName(roleId: (Guid)user.Role_Id),
                //permissions = await GetUserPermissionsAsync(user.User_Id)
                permissions = await GetUserPermissionsByRoleAsync((Guid)user.Role_Id)
            };

            return new
            {
                success = true,
                user = userObject,
                token = token
            };
        }

        public async Task<string> register(UserDTO usermodel)
        {
            usermodel.User_Id = Guid.NewGuid();
            usermodel.Password = Security.Encrypt(usermodel.Password);
            usermodel.Role_Id = await GetRoleId("User");
            await _dbcontext.Users.AddAsync(usermodel);
            await _dbcontext.SaveChangesAsync();

            return GenerateJwtToken(usermodel.User_Name);

        }
        public async Task<UserDTO> UpdateUser(UserDTO user)
        {
            var updatedItem = await _dbcontext.Users.Where(x=>x.User_Id == user.User_Id).FirstOrDefaultAsync();
            if (updatedItem != null)
            {
                user.Password = user.Password == "" ? updatedItem.Password : Security.Encrypt(user.Password);

                updatedItem.User_Name = user.User_Name;
                updatedItem.First_Name = user.First_Name;
                updatedItem.Last_Name = user.Last_Name;
                updatedItem.Password = user.Password;
                updatedItem.Email = user.Email;
                updatedItem.IsActive = user.IsActive;
                updatedItem.IsDeleted = user.IsDeleted;
                updatedItem.Role_Id = user.Role_Id;
                _dbcontext.Users.Update(updatedItem);
            }
  
            await _dbcontext.SaveChangesAsync();
            return user;
        }
        public async Task<List<UserDTO>> GetUsers()
        {
            return await _dbcontext.Users.Where(_=> _.IsDeleted == false && _.IsActive).ToListAsync();
        }

        public async Task<UserDTO> GetUser(Guid id)
        {
            var result = await _dbcontext.Users.Where(x => x.User_Id == id).FirstOrDefaultAsync();
            if (result != null)
                return result;

            return new UserDTO() { Email = "", User_Name = "", Password = "" };
        }

        public async Task CreateOrUpdateRole(Roles role)
        {
            role.role_Id = Guid.NewGuid();
            await _dbcontext.Roles.AddAsync(role);
            await _dbcontext.SaveChangesAsync();
        }
        public async Task<List<Roles>> GetRoles()
        {
            return await _dbcontext.Roles.ToListAsync();
        }

        public async Task<Guid> GetRoleId(string roleName)
        {
            var filterrole = await _dbcontext.Roles.Where(x => x.role_Name.ToLower().Equals(roleName.ToLower())).FirstOrDefaultAsync();
            if (filterrole != null)
                return filterrole.role_Id;

            return Guid.Empty;
        }

        public async Task<string> GetRoleName(Guid roleId)
        {
            var filterrole = await _dbcontext.Roles.Where(x => x.role_Id == roleId).FirstOrDefaultAsync();
            if (filterrole != null)
                return filterrole.role_Name;

            return string.Empty;
        }

        public async Task<List<UserPermissions>> GetUserPermissionsAsync(Guid user_Id)
        {
            var user = await this.GetUser(user_Id);
            var permissions = await _dbcontext.UserPermissions.Where(permissions => permissions.Role_Id.Equals(user.Role_Id)).ToListAsync();
            if (permissions.Count > 0)
                return permissions;

            var ApplicationMenus = await _dbcontext.ApplicationMenus.ToListAsync();
            ApplicationMenus.ForEach(item =>
            {
                permissions.Add(new UserPermissions
                {
                    Role_Id = (Guid)user.Role_Id,
                    Menu_Id = item.Menu_Id,
                    Menu_Name = item.Menu_Name,
                    Description = item.Description,
                    isEnable = item.isEnable,

                });
            });
            return permissions;
        }
        public async Task<List<UserPermissions>> GetUserPermissionsByRoleAsync(Guid role_id)
        {
            var permissions = await _dbcontext.UserPermissions.Where(permissions => permissions.Role_Id.Equals(role_id)).ToListAsync();
            if (permissions.Count > 0)
                return permissions;

            var ApplicationMenus = await _dbcontext.ApplicationMenus.ToListAsync();
            ApplicationMenus.ForEach(item =>
            {
                permissions.Add(new UserPermissions
                {
                    Role_Id = role_id,
                    Menu_Id = item.Menu_Id,
                    Menu_Name = item.Menu_Name,
                    Description = item.Description,
                    isEnable = item.isEnable,

                });
            });
            return permissions;
        }
        public async Task<bool> saveUserPermissionsAsync(List<UserPermissions> userPermissions, Guid user_Id)
        {
            var user = await this.GetUser(user_Id);
            var permissions = await _dbcontext.UserPermissions.Where(permissions => permissions.Role_Id.Equals(user.Role_Id)).ToListAsync();
            if (permissions.Count > 0)
            {
                permissions.ForEach(item =>
                {
                   var userPermissionItem = userPermissions.FirstOrDefault(userItem => userItem.Id == item.Id && userItem.Menu_Id == item.Menu_Id);
                    if (userPermissionItem != null)
                    {
                        item.Role_Id = user_Id;
                        item.isEnable = userPermissionItem.isEnable;
                        item.UpdatedBy = userPermissionItem.UpdatedBy;
                        item.UpdatedAt = DateTime.Now;
                    }

                });
                _dbcontext.UserPermissions.UpdateRange(permissions);
            }
            else {
                userPermissions.ForEach(item => {
                    item.Role_Id = (Guid)user.Role_Id;
                    item.CreatedAt = DateTime.Now;
                    item.CreatedBy = "Admin";
                });
                await _dbcontext.UserPermissions.AddRangeAsync(userPermissions);
            }
            await _dbcontext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> saveUserPermissionsByRoleAsync(List<UserPermissions> userPermissions, Guid role_Id)
        {
            var permissions = await _dbcontext.UserPermissions.Where(permissions => permissions.Role_Id.Equals(role_Id)).ToListAsync();
            if (permissions.Count > 0)
            {
                permissions.ForEach(item =>
                {
                    var userPermissionItem = userPermissions.FirstOrDefault(userItem => userItem.Id == item.Id && userItem.Menu_Id == item.Menu_Id);
                    if (userPermissionItem != null)
                    {
                        item.Role_Id = role_Id;
                        item.isEnable = userPermissionItem.isEnable;
                        item.UpdatedBy = userPermissionItem.UpdatedBy;
                        item.UpdatedAt = DateTime.Now;
                    }

                });
                _dbcontext.UserPermissions.UpdateRange(permissions);
            }
            else
            {
                userPermissions.ForEach(item => {
                    item.Role_Id = role_Id;
                    item.CreatedAt = DateTime.Now;
                    item.CreatedBy = "Admin";
                });
                await _dbcontext.UserPermissions.AddRangeAsync(userPermissions);
            }
            await _dbcontext.SaveChangesAsync();
            return true;
        }
        public async Task<string> GetUserEmail(Guid userId)
        {
            var findeduser = await _dbcontext.Users.Where(x => x.User_Id == userId).FirstOrDefaultAsync();
            if (findeduser != null)
                return findeduser.Email.ToString();
            return string.Empty;
        }
        private string GenerateJwtToken(string username)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!.ToString()));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256Signature);
            var tokeOptions = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: new List<Claim> { new Claim(ClaimTypes.Name, username) },
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: signinCredentials
            );
            return new JwtSecurityTokenHandler().WriteToken(tokeOptions);
        }
        public async Task<bool> InsertVerifyCode(VerifyCode verify)
        {
            await _dbcontext.VerifyCode.AddAsync(verify);
            await _dbcontext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> VerifyCode(VerifyCode verify)
        {
            var findverifycode = await _dbcontext.VerifyCode.Where(
                x => x.user_Id == verify.user_Id &&
                     x.code == verify.code &&
                     x.isExpired == false).FirstOrDefaultAsync();
            if (findverifycode != null)
            {
                findverifycode.isExpired = true;
                _dbcontext.VerifyCode.Update(findverifycode);
                await _dbcontext.SaveChangesAsync();

                var findeduser = await _dbcontext.Users.Where(x => x.User_Id == verify.user_Id).FirstOrDefaultAsync();
                if (findeduser != null)
                {
                    findeduser.IsActive = true;
                    _dbcontext.Users.Update(findeduser);
                    await _dbcontext.SaveChangesAsync();
                }

                return true;
            }
            return false;
        }



    }
}
