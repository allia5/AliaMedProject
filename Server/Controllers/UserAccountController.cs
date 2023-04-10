using DTO;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using OtripleS.Web.Api.Models.Users.Exceptions;
using Server.Models.Doctor.Exceptions;
using Server.Models.Exceptions;
using Server.Services.Foundation.JwtService;
using Server.Services.UserService;
using System.Transactions;
using static Server.Utility.Utility;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountController : ControllerBase
    {
        public readonly IUserService userService;
        public readonly IJwtService jwtService;
        public UserAccountController(IUserService userService, IJwtService jwtService)
        {
            this.userService = userService;
            this.jwtService = jwtService;
        }
        [HttpPost("CreateUserAccount")]
        public async Task<ActionResult<MessageResultDto>> CreateUserAccount([FromBody] RegistreAccountDto registreAccountDto)
        {
            TransactionScope transaction = CreateAsyncTransactionScope(IsolationLevel.ReadCommitted);
            try
            {
                var result = await this.userService.RegistreAccountAsync(registreAccountDto);
                transaction.Complete();
                return result;
            }
            catch (ValidationException validateException)
            {
                var messageException = GetInnerMessage(validateException);
                return BadRequest(messageException);
            }
            catch (ServiceException serviceException)
            {
                var messageException = GetInnerMessage(serviceException);
                return Conflict(messageException);
            }
            catch (FailedUserServiceException failedUserServiceException)
            {
                var messageException = GetInnerMessage(failedUserServiceException);
                return Problem(messageException);
            }
            finally
            {
                transaction.Dispose();
            }
        }
        [HttpGet("ValidatAccount")]
        public async Task<ActionResult<MessageResultDto>> ValidateEmailAdress(string uid, string token)
        {
            TransactionScope transaction = CreateAsyncTransactionScope(IsolationLevel.ReadCommitted);
            try
            {
                token = token.Replace(' ', '+');
                var result = await this.userService.ValidateAccountUserAsync(uid, token);
                transaction.Complete();
                return result;

            }
            catch (ValidationException validateException)
            {
                var messageException = GetInnerMessage(validateException);
                return BadRequest(messageException);
            }
            catch (IdentityException serviceException)
            {
                var messageException = GetInnerMessage(serviceException);
                return Unauthorized(messageException);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
            finally
            {
                transaction.Dispose();
            }


        }

        [HttpPost("LoginAccount")]
        public async Task<ActionResult<JwtDto>> Authentification(LoginAccountDto loginAccountDto)
        {
            try
            {
                return await this.userService.AuthenticationAccountAsync(loginAccountDto);
            }
            catch (ValidationException Ex)
            {
                return BadRequest(Ex.InnerException);
            }
            catch (StorageValidationException Ex)
            {
                return NotFound(Ex.InnerException);
            }
            catch (IdentityException Ex)
            {
                return Unauthorized(Ex.InnerException);
            }
            catch (FailedUserServiceException Ex)
            {
                return BadRequest(Ex.InnerException);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

        }

        private static string GetInnerMessage(Exception exception) =>
          exception.InnerException.Message;


        [HttpGet("AuthenticatedState")]

        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
        public IActionResult GetAuthenticatedState()
        {
            return Ok();
        }

        [HttpPost("TokenAlive")]
        public async Task<ActionResult<JwtDto>> PostNewRefreshToken(JwtDto jwtDto)
        {
            try
            {
                return await this.jwtService.UpdateRefreshToken(jwtDto);
            }
            catch (ValidationException Ex)
            {
                return BadRequest(Ex.InnerException);
            }
            catch (ServiceException Ex)
            {
                return BadRequest(Ex.InnerException);
            }
            catch (IdentityException Ex)
            {
                return Unauthorized(Ex.InnerException);
            }
            catch (FailedUserServiceException Ex)
            {
                return Problem(Ex.Message);
            }
            catch (Exception Ex)
            {
                return Problem(Ex.Message);
            }

        }




    }
}
