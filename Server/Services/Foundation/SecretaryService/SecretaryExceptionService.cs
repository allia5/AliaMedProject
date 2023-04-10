using DTO;
using Server.Models.Doctor.Exceptions;
using Server.Models.Exceptions;

namespace Server.Services.Foundation.SecretaryService
{
    public partial class SecretaryService
    {
        public delegate Task<SecritaryDto> AddSecretaryReturningFunction();
        public delegate Task<List<SecritaryDto>> GetSecretaryReturningFunction();
        public delegate Task UpdateSecretaryReturningFunction();
        public delegate Task<List<SecretaryCabinetInformationDto>> GetCabinetInformationReturningFunction();



        public async Task<List<SecretaryCabinetInformationDto>> TrycCatchFour(GetCabinetInformationReturningFunction getCabinetInformation)
        {
            try
            {
                return await getCabinetInformation();
            }
            catch (NullException Ex)
            {
                throw new ValidationException(Ex.InnerException);
            }

        }

        public async Task<SecritaryDto> TryCatch(AddSecretaryReturningFunction addSecretaryReturningFunction)
        {
            try
            {
                return await addSecretaryReturningFunction();
            }
            catch (NullException Ex)
            {
                throw new ValidationException(Ex);
            }
            catch (InvalidException Ex)
            {
                throw new ServiceException(Ex);
            }
            catch (NullDataStorageException Ex)
            {
                throw new StorageValidationException(Ex);

            }
            catch (OccuredDataException Ex)
            {
                throw new ConflictException(Ex);
            }
        }
        public async Task<List<SecritaryDto>> TryCatch_(GetSecretaryReturningFunction getSecretaryReturningFunction)
        {
            try
            {
                return await getSecretaryReturningFunction();
            }
            catch (NullException Ex)
            {
                throw new ValidationException(Ex);
            }

        }
        public async Task _TryCatch(UpdateSecretaryReturningFunction updateSecretaryReturningFunction)
        {
            try
            {
                await updateSecretaryReturningFunction();
            }
            catch (NullException Ex)
            {
                throw new ValidationException(Ex);
            }

        }
    }
}
