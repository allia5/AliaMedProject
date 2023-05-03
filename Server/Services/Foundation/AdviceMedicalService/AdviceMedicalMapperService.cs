using DTO;
using Server.Models.AdviceMedicals;
using Server.Models.UserAccount;
using static Server.Utility.Utility;


namespace Server.Services.Foundation.AdviceMedicalService
{
    public static class AdviceMedicalMapperService
    {
        public static AdviceMedical MapperToAdviceMedical(MedicalAdviceToAddDto medicalAdviceToAddDto , string UserId ,Guid OrdreMedicalId)
        {
            return new AdviceMedical
            {
                Id = Guid.NewGuid(),
                DateSendMessage = DateTime.Now,
                Message = medicalAdviceToAddDto.Message,
                OrdreMedicalId = OrdreMedicalId,
                StatusViewReceiver = StatusViewReceiver.NotWatchIt,
                transmitterUserId = UserId,


            };
        }
        public static AdviceMedicalDto MapperToAdviceMedical(User UserAccountReceiver, AdviceMedical adviceMedical)
        {
            return new AdviceMedicalDto
            {
                Id = EncryptGuid(adviceMedical.Id),
                DateSend = adviceMedical.DateSendMessage,
                FullNameReceiver = UserAccountReceiver.Firstname +" "+ UserAccountReceiver.LastName,

                Message = adviceMedical.Message,
                StatusViewReceiver = (StatusViewReceiverDto)adviceMedical.StatusViewReceiver
            };
        }
    }
}
