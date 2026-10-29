using Inventory.Domain.DTO;
using Inventory.Domain.Model.Account;
using Inventory.Repository.Interfaces.Account;
using Inventory.Services.Interfaces.Account;
using Inventory.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Services.Implementation.Account
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        //private readonly IEmailService _emailService;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
            //_emailService = emailService;
        }
        public async Task<object> login(UserModel userModel)
        {
            return await _accountRepository.login(userModel);

        }
        public async Task<string> register(UserDTO userModel)
        {
            return await _accountRepository.register(userModel);
        }
        public async Task<UserDTO> updateUser(UserDTO user)
        {
            return await _accountRepository.UpdateUser(user);
        }
        public async Task CreateOrUpdateRole(Roles role)
        {
            await _accountRepository.CreateOrUpdateRole(role);
        }

        public async Task<List<Roles>> GetRoles()
        {
            return await _accountRepository.GetRoles();
        }
        public async Task<string> GetRoleName(Guid roleId)
        {
            return await _accountRepository.GetRoleName(roleId);
        }
        public async Task<Guid> GetRoleId(string roleName)
        {
            return await _accountRepository.GetRoleId(roleName);
        }

        public async Task<List<UserPermissions>> GetUserPermissionsAsync(Guid user_Id)
        { 
            return await _accountRepository.GetUserPermissionsAsync(user_Id);
        }

        public async Task<List<UserPermissions>> GetUserPermissionsByRoleAsync(Guid role_id)
        {
            return await _accountRepository.GetUserPermissionsByRoleAsync(role_id);
        }
        public async Task<bool> saveUserPermissionsAsync(List<UserPermissions> userPermissions, Guid user_Id)
        { 
            return await _accountRepository.saveUserPermissionsAsync(userPermissions, user_Id);
        }
        public async Task<bool> saveUserPermissionsByRoleAsync(List<UserPermissions> userPermissions, Guid role_Id)
        {
            return await _accountRepository.saveUserPermissionsByRoleAsync(userPermissions, role_Id);
        }
        public async Task SendVerificationCode(Guid userId)
        {
            var email = await _accountRepository.GetUserEmail(userId);
            Int32 code = CommonFunctions.GenerateVerificationCode();

            await _accountRepository.InsertVerifyCode(new VerifyCode() { user_Id = userId, code = code.ToString(), isExpired = false });

            string subject = $"BillSoft - One-Time Verification code";
            string body = CommonFunctions.GetVerificationCodeTemplate("BillSoft", code.ToString());
            //await _emailService.SendEmailAsync(email, subject, body);
        }

        public async Task<bool> VerifyCode(VerifyCode verify)
        {
            return await _accountRepository.VerifyCode(verify);
        }

        public async Task<List<UserDTO>> GetUsers()
        {
            return await _accountRepository.GetUsers();
        }

        public async Task<UserDTO> GetUser(Guid id)
        {
            return await _accountRepository.GetUser(id);
        }
    }
}
