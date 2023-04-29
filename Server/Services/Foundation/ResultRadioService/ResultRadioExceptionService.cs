using DTO;
using Server.Models.Doctor.Exceptions;
using Server.Models.Exceptions;

namespace Server.Services.Foundation.ResultRadioService
{
    public partial class ResultRadioService
    {
        public delegate Task OnAddResultRadio();
        public delegate Task<FileResultDto> OnGetFileResultRadio();

        public async Task<FileResultDto> TryCatch_(OnGetFileResultRadio onGetFileResultRadio)
        {
            try
            {
                return await onGetFileResultRadio();
            }
            catch (StatusValidationException Ex)
            {
                throw new ValidationException(Ex);
            }
            catch (CompatibilityError Ex)
            {
                throw new ValidationException(Ex);
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
                throw new StorageValidationException(Ex);
            }
            catch (ArgumentNullException Ex)
            {
                throw new ValidationException(Ex);
            }
        }
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
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
           
        }
    }
}
