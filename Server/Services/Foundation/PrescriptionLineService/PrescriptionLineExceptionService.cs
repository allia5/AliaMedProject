using Server.Models.Doctor.Exceptions;
using Server.Models.Exceptions;

namespace Server.Services.Foundation.PrescriptionLineService
{
    public partial class PrescriptionLineService
    {
        public delegate Task OnUpdateStatusPrescriptionLine();
        public async Task TryCatch(OnUpdateStatusPrescriptionLine onUpdateStatusPrescriptionLine)
        {
            try
            {
                await onUpdateStatusPrescriptionLine();
            }
             catch (NullException Ex)
            {
                throw new ServiceException(Ex);
            }
            catch (ArgumentNullException Ex)
            {
                throw new ValidationException(Ex);
            }
            catch (NullDataStorageException Ex)
            {
                throw new ValidationException(Ex);
            }catch(StatusValidationException Ex)
            {
                throw new StorageValidationException(Ex);
            }
        }
    }
}
