using DTO;
using Server.Models.Doctor.Exceptions;
using Server.Models.Exceptions;
using static Server.Services.Foundation.FileMedicalService.FileMedicalService;

namespace Server.Services.Foundation.AnalyseMedicalService
{
    public partial class AnalyseMedicalService
    {
        public delegate Task<InformationAnalyseResultDto> OnGetInformationAnalyse();
        public delegate Task<byte[]> ReturningByteFileMedical();
        public async Task<InformationAnalyseResultDto> TryCatch(OnGetInformationAnalyse onGetInformationAnalyse)
        {
            try
            {
                return await onGetInformationAnalyse();
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
