using DTO;

namespace Server.Services.Foundation.AnalyseMedicalService
{
    public partial class AnalyseMedicalService
    {
        public delegate Task<InformationAnalyseResultDto> OnGetInformationAnalyse();
        public async Task<InformationAnalyseResultDto> TryCatch(OnGetInformationAnalyse onGetInformationAnalyse)
        {
            try
            {
                return await onGetInformationAnalyse();
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
    }
}
