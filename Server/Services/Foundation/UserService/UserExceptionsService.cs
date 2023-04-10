using DTO;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OtripleS.Web.Api.Models.Users.Exceptions;
using Server.Models.Doctor.Exceptions;
using Server.Models.Exceptions;
using Server.Models.UserAccount;

namespace Server.Services.UserService
{
    public partial class UserService
    {
        private delegate ValueTask<MessageResultDto> ReturningUserFunction();
        private delegate ValueTask<JwtDto> ReturningAuthenticationFunction();


        private async ValueTask<MessageResultDto> TryCatch(ReturningUserFunction returningUserFunction)
        {
            try
            {
                return await returningUserFunction();
            }
            catch (NullException nullUserException)
            {
                throw new ValidationException(nullUserException);
            }
            catch (AlreadyExistsException AlreadyExistsException)
            {
                throw new ServiceException(AlreadyExistsException);
            }
            catch (InvalidException InvalidException)
            {
                throw new ServiceException(InvalidException);
            }
            catch (SqlException SqlException)
            {
                throw new FailedUserServiceException(SqlException);
            }
            catch (FailedCreateUserException failedCreateUser)
            {
                throw new FailedUserServiceException(failedCreateUser);
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
        private async ValueTask<JwtDto> TryCatch(ReturningAuthenticationFunction returningUserFunction)
        {
            try
            {
                return await returningUserFunction();
            }
            catch (NullException nullUserException)
            {
                throw new ValidationException(nullUserException);
            }


            catch (SqlException SqlException)
            {
                throw new FailedUserServiceException(SqlException);
            }

            catch (IdentityTokenException identityTokenException)
            {
                throw new IdentityException(identityTokenException);

            }
            catch (NullDataStorageException ExStorage)
            {
                throw new StorageValidationException(ExStorage);

            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);


            }
        }

    }
}
