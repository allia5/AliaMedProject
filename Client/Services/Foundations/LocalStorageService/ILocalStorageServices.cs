namespace Client.Services.Foundations.LocalStorageService
{
    public interface ILocalStorageServices
    {
        public Task SetItemAsync<T>(string key, T value);
        public Task<T> GetItemAsync<T>(string key);
        public Task RemoveItemAsync(string key);
        public Task ClearAsync();
        //public ValueTask<string> KeyAsync(int index);

    }
}
