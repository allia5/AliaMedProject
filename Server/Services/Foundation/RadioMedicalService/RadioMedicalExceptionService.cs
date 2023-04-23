using DTO;
using Server.Models.Doctor.Exceptions;
using Server.Models.Exceptions;

namespace Server.Services.Foundation.RadioMedicalService
{
    public partial class RadioMedicalService
    {
        public delegate Task<InformationRadioResultDto> GetInformationRadioResultDtoDelegate();

        public async Task<InformationRadioResultDto> TryCatch(GetInformationRadioResultDtoDelegate informationRadioResultDto)
        {
            try
            {
                return await informationRadioResultDto();
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
                throw new  StorageValidationException(Ex);
            }
        }
    }
}
