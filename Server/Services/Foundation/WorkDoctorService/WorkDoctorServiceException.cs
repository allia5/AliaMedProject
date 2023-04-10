using DTO;
using Microsoft.Data.SqlClient;
using Server.Models.Doctor.Exceptions;
using Server.Models.Exceptions;

namespace Server.Services.Foundation.WorkDoctorService
{
    public partial class WorkDoctorService
    {
        private delegate Task returningFunction();
        private delegate Task<List<InvitationsDoctorDto>> ReturningInvitationFunction();
        private delegate Task<List<JobsDoctorDto>> ReturningFunctionJobsDoctor();
        private delegate Task<JobSettingDto> ReturningJobSetting();
        private delegate Task<List<DoctorCabinetDto>> ReturningDoctorsCabinet();

        private async Task TryCatch(returningFunction returningFunction)
        {
            try
            {
                await returningFunction();
            }
            catch (NullException Ex)
            {
                throw new ValidationException(Ex);
            }
            catch (NullDataStorageException Ex)
            {
                throw new ServiceException(Ex);
            }


        }
        private async Task<List<InvitationsDoctorDto>> _TryCatch(ReturningInvitationFunction returningInvitationFunction)
        {
            try
            {
                return await returningInvitationFunction();
            }
            catch (NullException Ex)
            {
                throw new ValidationException(Ex);
            }
            catch (NullDataStorageException Ex)
            {
                throw new ServiceException(Ex);
            }


        }
        private async Task<List<JobsDoctorDto>> TryCatch_(ReturningFunctionJobsDoctor returningFunctionJobsDoctor)
        {
            try
            {
                return await returningFunctionJobsDoctor();
            }
            catch (NullException Ex)
            {
                throw new ValidationException(Ex);
            }
            catch (NullDataStorageException Ex)
            {
                throw new ValidationException(Ex);
            }

        }
        private async Task<JobSettingDto> _TryCatch_(ReturningJobSetting returningJobSetting)
        {
            try
            {
                return await returningJobSetting();
            }
            catch (NullException Ex)
            {
                throw new ValidationException(Ex);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        private async Task<List<DoctorCabinetDto>> _TryCatch_1(ReturningDoctorsCabinet returningDoctorsCabinet)
        {
            try
            {
                return await returningDoctorsCabinet();
            }
            catch (NullException Ex)
            {
                throw new ValidationException(Ex);
            }
            catch (NullDataStorageException Ex)
            {
                throw new ValidationException(Ex);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
