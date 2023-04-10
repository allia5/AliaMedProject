using DTO;
using OtripleS.Web.Api.Models.Users.Exceptions;
using Server.Models.Doctor.Exceptions;
using Server.Models.Exceptions;

namespace Server.Services.Foundation.PlanningAppoimentService
{
    public partial class PlanningAppoimentService
    {
        public delegate Task<List<AppointmentInformationDto>> AppointmentInformationFunction();
        public delegate Task UpdateAppoimentFunction();

        public async Task<List<AppointmentInformationDto>> TryCatch(AppointmentInformationFunction appointmentInformationFunction)
        {
            try
            {
                return await appointmentInformationFunction();
            }
            catch (NullException Ex)
            {
                throw new ValidationException(Ex);
            }
            catch (StatusValidationException Ex)
            {
                throw new FailedUserServiceException(Ex);
            }
            catch (NotFoundException Ex)
            {
                throw new ServiceException(Ex);
            }
            catch (OccuredDataException Ex)
            {
                throw new StorageValidationException(Ex);
            }
        }
        public async Task TryCatch_(UpdateAppoimentFunction updateAppoimentFunction)
        {
            try
            {
                await updateAppoimentFunction();
            }
            catch (InvalidException Ex)
            {
                throw new ValidationException(Ex);

            }
            catch (NullException Ex)
            {
                throw new ServiceException(Ex);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
