using DTO;
using OtripleS.Web.Api.Models.Users.Exceptions;
using Server.Models.Doctor.Exceptions;
using Server.Models.Exceptions;
using Microsoft.Data.SqlClient;

namespace Server.Services.Foundation.JwtService
{
    public partial class JwtService
    {
        private delegate ValueTask<JwtDto> ReturningUserFunction();
        private async ValueTask<JwtDto> TryCatch(ReturningUserFunction returningUserFunction)
        {
            try
            {
                return await returningUserFunction();
            }
            catch (NullException nullUserException)
            {
                throw new ValidationException(nullUserException);
            }

            catch (InvalidException InvalidException)
            {
                throw new ServiceException(InvalidException);
            }
            catch (SqlException SqlException)
            {
                throw new FailedUserServiceException(SqlException);
            }

            catch (IdentityTokenException identityTokenException)
            {
                throw new IdentityException(identityTokenException);

            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);//ServiceException(exception);


            }
        }
    }
}
