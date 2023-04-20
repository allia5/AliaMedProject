using DTO;
using Server.Models.Doctor.Exceptions;
using Server.Models.Exceptions;

namespace Server.Services.Foundation.OrdreMedicalService
{
    public partial class OrdreMedicalService
    {
        public delegate Task<OrdreMedicalDto> ReturningFunctionOnAddOrdreMedcial();
        public delegate Task<List<InformationOrderMedicalSecritary>> ReturningFunctionMedicalOrderSecritary();
        public delegate Task ReturningFunctionOnUpdateStatusOrdreMedical();


        public async Task TryCatch(ReturningFunctionOnUpdateStatusOrdreMedical returningFunctionOnUpdateStatusOrdreMedical)
        {
            try
            {
                await returningFunctionOnUpdateStatusOrdreMedical();
            }
            catch (NullException ex)
            {
                throw new ServiceException(ex);
            }
            catch (ArgumentNullException ex)
            {
                throw new ValidationException(ex);
            }
            catch (StatusValidationException ex)
            {
                throw new ValidationException(ex);
            }
            
           
        } 


        public async Task<List<InformationOrderMedicalSecritary>> TryCatch(ReturningFunctionMedicalOrderSecritary returningFunctionMedicalOrderSecritary)
        {
            try
            {
                return await returningFunctionMedicalOrderSecritary();
            }
            catch (NullException ex)
            {
                throw new ServiceException(ex);
            }
            catch (ArgumentNullException ex)
            {
                throw new ValidationException(ex);
            }
            
            catch (StatusValidationException ex)
            {
                throw new ServiceException(ex);
            }
        }
        public async Task<OrdreMedicalDto> TryCatch(ReturningFunctionOnAddOrdreMedcial returningFunctionOnAddOrdreMedcial)
        {
            try
            {
                return await returningFunctionOnAddOrdreMedcial();
            }
            catch (NullException ex)
            {
                throw new ServiceException(ex);
            }catch (ArgumentNullException ex)
            {
                throw new ValidationException(ex);
            }catch(CompatibilityError  ex)
            {
                throw new ValidationException(ex);
            }catch(StatusValidationException ex)
            {
                throw new ValidationException(ex);
            }
        }
    }
}
