using DTO;
using Server.Models.Doctor.Exceptions;
using Server.Models.Exceptions;

namespace Server.Services.Foundation.CabinetMedicalService
{
    public partial class CabinetMedicalService
    {
        private delegate ValueTask<CabinetMedicalDto> returningCabinetFunction();

        private async ValueTask<CabinetMedicalDto> TryCatch(returningCabinetFunction returningCabinetFunction)
        {
            try
            {
                return await returningCabinetFunction();
            }
            catch (NullException Ex)
            {
                throw new ValidationException(Ex);
            }
            catch (NullDataStorageException Ex)
            {
                throw new StorageValidationException(Ex);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
    }
}
