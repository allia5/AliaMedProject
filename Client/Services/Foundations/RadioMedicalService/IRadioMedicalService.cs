namespace Client.Services.Foundations.RadioMedicalService
{
    public interface IRadioMedicalService
    {
        public Task<Stream> SecritaryGetMedicalFileRadio(string OrdreId,string CabinetId);
    }
}
