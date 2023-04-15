using DTO;
using Server.Models.Doctor.Exceptions;
using Server.Models.Exceptions;

namespace Server.Services.Foundation.FileMedicalService
{
    public partial class FileMedicalService
    {
        public delegate Task<FileMedicalPatientDto> ReturningFileMedicalPatientAsync();
        public async Task<FileMedicalPatientDto> TryCatch(ReturningFileMedicalPatientAsync returningFileMedicalPatient)
        {
            try
            {
                return await returningFileMedicalPatient();
            }catch (NullException ex)
            {
                throw new ValidationException(ex.InnerException);
            }
            catch (CompatibilityError Ex)
            {
                throw new ServiceException(Ex.InnerException);
            }catch(StatusValidationException ex)
            {
                throw new ServiceException(ex.InnerException);
            }
        }
    }
}
