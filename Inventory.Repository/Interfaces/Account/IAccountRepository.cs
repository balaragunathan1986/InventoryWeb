using Inventory.Domain.DTO;
using Inventory.Domain.Model.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Repository.Interfaces.Account
{
    public interface IAccountRepository
    {
        Task<object> login(UserModel usermodel);
        Task<string> register(UserDTO usermodel);
        Task<UserDTO> UpdateUser(UserDTO user);
        Task<List<UserDTO>> GetUsers();
        Task<UserDTO> GetUser(Guid id);
        Task CreateOrUpdateRole(Roles role);
        Task<List<Roles>> GetRoles();
        Task<Guid> GetRoleId(string roleName);
        Task<string> GetRoleName(Guid roleId);
        Task<string> GetUserEmail(Guid userId);
        Task<List<UserPermissions>> GetUserPermissionsAsync(Guid user_Id);
        Task<List<UserPermissions>> GetUserPermissionsByRoleAsync(Guid role_id);
        Task<bool> saveUserPermissionsAsync(List<UserPermissions> userPermissions, Guid user_Id);
        Task<bool> saveUserPermissionsByRoleAsync(List<UserPermissions> userPermissions, Guid role_Id);
        Task<bool> InsertVerifyCode(VerifyCode verify);
        Task<bool> VerifyCode(VerifyCode verify);
    }
}
