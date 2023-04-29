using Server.Models.ResultsRadio;

namespace Server.Managers.Storages.RadioResultManager
{
    public interface IRadioResultManager
    {
        public Task<ResultRadio> InserRadioResult(ResultRadio resultRadio);
        public Task<ResultRadio> SelectRadioResultByIdLineRadio(Guid IdLine);
    }
}
