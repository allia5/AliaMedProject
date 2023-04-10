using DTO;
using Server.Models.Doctor.Exceptions;
using Server.Models.Exceptions;

namespace Server.Services.Foundation.DoctorService
{
    public partial class DoctorService
    {
        private delegate Task<List<DoctorInformationDto>> returningDoctorsFunction();

        private async ValueTask<List<DoctorInformationDto>> TryCatch(returningDoctorsFunction returningDoctorsFunction)
        {
            try
            {
                return await returningDoctorsFunction();
            }
            catch (NullException Ex)
            {
                throw new ServiceException(Ex);
            }
            catch (NotFoundException Ex)
            {
                throw new StorageValidationException(Ex);
            }
            catch (NullDataStorageException Ex)
            {
                throw new ValidationException(Ex);
            }
        }
    }
}
