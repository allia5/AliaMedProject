using DTO;

namespace Server.Services.Foundation.ResultRadioService
{
    public interface  IResultRadioService
    {
        public Task AddRadioResultService(string Email, RadioResultToAddDto RadioResultToAddDto);
    }
}
