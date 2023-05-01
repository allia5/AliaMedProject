namespace Client.Services.Foundations.RadioMedicalService
{
    public interface IRadioMedicalService
    {
        public Task<Stream> GetMedicalFileRadio(string OrdreId);
    }
}
