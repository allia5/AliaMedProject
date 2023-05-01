using DTO;
using Server.Models.Doctor.Exceptions;
using Server.Models.Exceptions;

namespace Server.Services.Foundation.PrescriptionService
{
    public partial class PrescriptionService
    {
        public delegate Task<InformationPrescriptionResultDto> GetInformationPrescriptionResultDtoDelegate();
        public delegate Task<byte[]> ReturningByteFileMedical();
        public async Task<InformationPrescriptionResultDto> TryCatch(GetInformationPrescriptionResultDtoDelegate getInformationPrescriptionResultDtoDelegate)
        {
            try
            {
                return await getInformationPrescriptionResultDtoDelegate();
            }
            catch (ArgumentNullException Ex)
            {
                throw new ValidationException(Ex);
            }
            catch (NullException Ex)
            {
                throw new ServiceException(Ex);
            }
            catch (NullDataStorageException Ex)
            {
                throw new StorageValidationException(Ex);
            }
            catch (FormatException Ex)
            {
                throw new ValidationException(Ex);
            }
        }
        public async Task<byte[]> TryCatch(ReturningByteFileMedical returningByteFileMedical)
        {
            try
            {
                return await returningByteFileMedical();
            }
            catch (ArgumentNullException ex)
            {
                throw new ValidationException(ex);
            }
            catch (NullException ex)
            {
                throw new ServiceException(ex);
            }
        }
    }
}
