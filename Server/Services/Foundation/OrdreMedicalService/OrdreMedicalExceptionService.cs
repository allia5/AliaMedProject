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
        public delegate Task<MedicalFileArchiveDto> ReturningFunctionMedicalFileArchive();

        public async Task<MedicalFileArchiveDto> TryCatch_(ReturningFunctionMedicalFileArchive returningFunctionMedicalFileArchive)
        {
            try
            {
                return await returningFunctionMedicalFileArchive();
            }
            catch (ArgumentNullException Ex)
            {
                throw new ValidationException(Ex);

            }
            catch (CompatibilityError Ex)
            {
                throw new ValidationException(Ex);
            }
            catch (StatusValidationException Ex)
            {
                throw new ValidationException(Ex);
            }
            catch (NullException Ex)
            {
                throw new ServiceException(Ex);
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
           
        }

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
