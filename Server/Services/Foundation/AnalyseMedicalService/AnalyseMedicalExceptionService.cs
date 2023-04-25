using DTO;
using Server.Models.Doctor.Exceptions;
using Server.Models.Exceptions;

namespace Server.Services.Foundation.AnalyseMedicalService
{
    public partial class AnalyseMedicalService
    {
        public delegate Task<InformationAnalyseResultDto> OnGetInformationAnalyse();
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
    }
}
