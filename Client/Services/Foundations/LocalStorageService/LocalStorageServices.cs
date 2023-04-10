using Blazored.LocalStorage;
using DTO;

namespace Client.Services.Foundations.LocalStorageService
{
    public class LocalStorageServices : ILocalStorageServices
    {

        public ILocalStorageService localStorageService { get; set; }
        public LocalStorageServices(ILocalStorageService localStorageService)
        {
            this.localStorageService = localStorageService;

        }
        public async Task ClearAsync()
        {
            await localStorageService.ClearAsync();
        }

        public async Task<T> GetItemAsync<T>(string key)
        {
            return await localStorageService.GetItemAsync<T>(key);
        }

        /* public async ValueTask<string> KeyAsync(int index)
         {
             try
             {
                 return await localStorageService.KeyAsync(index);
             }
             catch (Exception e)
             {
                 throw new Exception(e.Message);
             }

         }*/

        public async Task RemoveItemAsync(string key)
        {
            await localStorageService.RemoveItemAsync(key);
        }

        public async Task SetItemAsync<T>(string key, T value)
        {
            await localStorageService.SetItemAsync(key, value);
        }
    }
}
