using Server.Models.RadioMedical;

namespace Server.Managers.Storages.RadioManager
{
    public interface IRadioManager
    {
        public Task<Radio> InsertRadioAsync(Radio radio);
    }
}
