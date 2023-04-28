using DTO;
using Org.BouncyCastle.Asn1.Crmf;

namespace Server.Services.Foundation.OrdreMedicalService
{
    public interface IOrdreMedicalService
    {
        public Task<OrdreMedicalDto> AddOrdreMedicalDto(string Email, OrderMedicalToAddDro orderMedicalToAdd);
        public Task<List<InformationOrderMedicalSecritary>> SelectAllMedicalOrdreSecritary(string Email,KeysAppoimentInformationSecretary keysAppoimentInformationSecritary);
        public Task UpdateStatusOrdreMedicalService(string Email, UpdateOrdreMedicalDto updateOrdreMedicalDto);
        public Task<MedicalFileArchiveDto> GetListOrdreFileMedical(string Email, string AppointmentId, string FileId);


    }
}
