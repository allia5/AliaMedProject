using Server.Models.Doctor.Exceptions;
using Server.Models.Exceptions;

namespace Server.Services.Foundation.ResultRadioService
{
    public partial class ResultRadioService
    {
        public delegate Task OnAddResultRadio();
        public async Task TryCatch(OnAddResultRadio onAddResultRadio)
        {
            try
            {
                await onAddResultRadio();
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
                throw new StorageValidationException(Ex);
            }
           
        }
    }
}
