using DTO;
using Server.Models.Cities;

namespace Server.Services.Foundation.cityService
{
    public partial class CityService
    {
        public delegate Task<List<CityDto>> ListCityReturnFunction();
        public async Task<List<CityDto>> TryCatch(ListCityReturnFunction listCityReturnFunction)
        {
            try
            {
                return await listCityReturnFunction();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
