using Inventory.Domain.DTO;
using Inventory.Domain.Model;
using Inventory.Domain.Model.Account;
using Inventory.Services.Interfaces.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Inventory.API.Controllers.v1.Account
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        //private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        public AccountController(IConfiguration configuration, IAccountService accountService)
        {
            _configuration = configuration;
            _accountService = accountService;
            //_emailService = emailService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserModel user)
        {
            try
            {
                var result = await _accountService.login(user);

                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Database error: {ex}");

                if (ex.InnerException is Microsoft.Data.SqlClient.SqlException)
                {
                    return StatusCode(503, new
                    {
                        success = false,
                        message = "Unable to connect to the database. Please try again later."
                    });
                }

                return StatusCode(500, new
                {
                    success = false,
                    message = "Something went wrong. Please try again later."
                });
            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
            {
                Console.WriteLine($"SQL error: {ex}");

                return StatusCode(503, new
                {
                    success = false,
                    message = "Unable to connect to the database. Please try again later."
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Login API error: {ex}");

                return StatusCode(500, new
                {
                    success = false,
                    message = "Something went wrong. Please try again later."
                });
            }
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDTO user)
        {
            try
            {
                var checkUser = await _accountService.GetUsers();
                var existUser = checkUser.Where(x => x.User_Name.ToLower() == user.User_Name.ToLower() && x.Email.ToLower() == user.Email.ToLower()).FirstOrDefault();
                if(existUser != null)
                    return Ok(ApiResponse<string>.ErrorResponse("", "User already exist, Please check with you administrator or try different one"));

                var token = await _accountService.register(user);
                return Ok(new AuthenticationResponse { Token = token });
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("users")]
        [Authorize]
        public async Task<IActionResult> Users()
        {
            try
            {
                var users = await _accountService.GetUsers();
                return Ok(users);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPut("users")]
        [Authorize]
        public async Task<IActionResult> Users([FromBody] UserDTO user)
        {
            try
            {
                var users = await _accountService.updateUser(user);
                return Ok(users);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("users/{id}")]
        [Authorize]
        public async Task<IActionResult> Users(Guid id)
        {
            try
            {
                var user = await _accountService.GetUser(id);
                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpGet("roles")]
        [Authorize]
        public async Task<IActionResult> Roles()
        {
            try
            {
                var roles = await _accountService.GetRoles();
                return Ok(ApiResponse<object>.SuccessResponse(roles, "Role fetched successfully."));
            }
            catch (Exception e)
            {
                return BadRequest(ApiResponse<string>.SuccessResponse(e.Message, "\"Unable to fetch Roles, Please contact your administrator."));
            }
        }
        [HttpGet("roles/{role_Id}")]
        [Authorize]
        public async Task<IActionResult> Roles(Guid role_Id)
        {
            try
            {
                var roles = await _accountService.GetRoleName(role_Id);
                return Ok(ApiResponse<string>.SuccessResponse(roles, "Role name fetched."));
            }
            catch (Exception e)
            {
                return BadRequest(ApiResponse<string>.SuccessResponse(e.Message, "Unable to fetch Role name, Please contact your administrator."));
            }
        }

        [HttpPost("roles")]
        [Authorize]
        public async Task<IActionResult> Roles([FromBody] Roles role)
        {
            try
            {
                await _accountService.CreateOrUpdateRole(role);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpGet("UserPermissions/{user_Id}")]
        [Authorize]
        public async Task<IActionResult> UserPermissions(Guid user_Id)
        {
            try
            {
                var result = await _accountService.GetUserPermissionsAsync(user_Id);
                return Ok(ApiResponse<object>.SuccessResponse(result, "User Permission record fetched successfully."));
            }
            catch (Exception e)
            {
                return BadRequest(ApiResponse<string>.ErrorResponse(e.Message, "User Permission record are empty."));
            }
        }
        [HttpGet("UserPermissions/roles/{role_id}")]
        [Authorize]
        public async Task<IActionResult> UserPermissionsByRoleId(Guid role_id)
        {
            try
            {
                var result = await _accountService.GetUserPermissionsByRoleAsync(role_id);
                return Ok(ApiResponse<object>.SuccessResponse(result, "User Permission record fetched successfully."));
            }
            catch (Exception e)
            {
                return BadRequest(ApiResponse<string>.ErrorResponse(e.Message, "User Permission record are empty."));
            }
        }


        [HttpPost("UserPermissions/{user_Id}")]
        [Authorize]
        public async Task<IActionResult> UserPermissions(Guid user_Id, [FromBody]List<UserPermissions> permissions)
        {
            try
            {
                var result = await _accountService.saveUserPermissionsAsync(permissions, user_Id);
                return Ok(ApiResponse<object>.SuccessResponse(result, "User Permission saved successfully."));
            }
            catch (Exception e)
            {
                return BadRequest(ApiResponse<string>.ErrorResponse(e.Message, "User Permission saved failed."));
            }
        }

        [HttpPost("UserPermissions/roles/{role_Id}")]
        [Authorize]
        public async Task<IActionResult> UserPermissionsByRole(Guid role_Id, [FromBody] List<UserPermissions> permissions)
        {
            try
            {
                var result = await _accountService.saveUserPermissionsByRoleAsync(permissions, role_Id);
                return Ok(ApiResponse<object>.SuccessResponse(result, "User Permission saved successfully."));
            }
            catch (Exception e)
            {
                return BadRequest(ApiResponse<string>.ErrorResponse(e.Message, "User Permission saved failed."));
            }
        }

        [HttpGet("sendverification/{id}")]
        [Authorize]
        public async Task<IActionResult> SendVerification(Guid id)
        {
            try
            {
                await _accountService.SendVerificationCode(id);
                return Ok(
                    ApiResponse<string>.SuccessResponse("", "Verification code sened succussfully.")
                    );
            }
            catch (Exception)
            {
                return BadRequest(ApiResponse<string>.SuccessResponse("", "Verification code sened faild, Please contact your administrator"));
            }
        }
        [HttpPost("verifycode/{id}")]
        [Authorize]
        public async Task<IActionResult> VerifyCode([FromBody] VerifyCode verify)
        {
            try
            {
                var result = await _accountService.VerifyCode(verify);
                if (result)
                    return Ok(
                        ApiResponse<string>.SuccessResponse("", "Verification code sended succussfully.")
                        );
                else
                    return BadRequest(ApiResponse<string>.SuccessResponse("", "Verification failed, Please re-send code and re-verify you account."));
            }
            catch (Exception)
            {
                return BadRequest(ApiResponse<string>.SuccessResponse("", "Verification failed, Please contact your administrator"));
            }
        }
    }
}
