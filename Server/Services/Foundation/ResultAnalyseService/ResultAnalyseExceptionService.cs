using DTO;
using Server.Models.Doctor.Exceptions;
using Server.Models.Exceptions;

namespace Server.Services.Foundation.ResultAnalyseService
{
    public partial class ResultAnalyseService
    {
        public delegate Task OnPostResultAnalyse();
        public delegate Task<FileResultDto> OnGetFileResultAnalyse();

        public async Task<FileResultDto> TryCatch_(OnGetFileResultAnalyse onGetFileResultAnalyse)
        {
            try
            {
                return await onGetFileResultAnalyse();
            }
            catch ()
            {

            }
            catch ()
            {

            }
            catch ()
            {

            }
        }
        public async Task TryCatch(OnPostResultAnalyse onPostResultAnalyse )
        {
            try
            {
                 await onPostResultAnalyse();
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
