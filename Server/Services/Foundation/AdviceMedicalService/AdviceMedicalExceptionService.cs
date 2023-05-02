using DTO;
using Server.Models.Doctor.Exceptions;

namespace Server.Services.Foundation.AdviceMedicalService
{
    public partial class AdviceMedicalService
    {
        public delegate Task<List<AdviceMedicalDto>> ReturningFunctionAdviceMedicalServiceDelegate();
        public async Task<List<AdviceMedicalDto>>  TryCatch(ReturningFunctionAdviceMedicalServiceDelegate returningFunctionAdviceMedicalService)
        {
            try
            {
                return await returningFunctionAdviceMedicalService();
            }catch(ArgumentNullException Ex)
            {
                throw new ValidationException(Ex);
            }
            catch(NullException Ex)
            {
                throw new ServiceException(Ex);
            }
           catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
