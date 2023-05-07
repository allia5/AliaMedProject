namespace Client.Services.Foundations.RadioMedicalService
{
    public interface IRadioMedicalService
    {
        public Task<Stream> SecritaryGetMedicalFileRadio(string OrdreId,string CabinetId);
        public Task<Stream> PatientGetMedicalFileRadio(string OrdreId);
    }
}
