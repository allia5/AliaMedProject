using Client.Services.Exceptions;
using DTO;

namespace Client.Services.Foundations.AuthentificationStatService
{
    public partial class AuthentificationStatService
    {
        public void ValidationLocalStorageIsNull(JwtDto jwtDto)
        {
            if (jwtDto == null)
            {
                throw new NullException("Local Storage Is Null");

            }
        }
    }
}
