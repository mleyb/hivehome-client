using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace HiveHome.Client
{
    public class HiveHomeClient : IHiveHomeClient, IDisposable
    {
        private HttpClient _client = new HttpClient();

        public HiveHomeClient()
        {
            _client.BaseAddress = new Uri("https://api-prod.bgchprod.info:443/omnia");

            _client.DefaultRequestHeaders.Add("Content-Type", "application/vnd.alertme.zoo-6.1+json");
            _client.DefaultRequestHeaders.Add("Accept", "application/vnd.alertme.zoo-6.1+json");
            _client.DefaultRequestHeaders.Add("X-Omnia-Client", "Hive Web Dashboard");
        }

        public void Dispose()
        {
            LogoutAsync().Wait();

            _client.Dispose();
        }

        public async Task LoginAsync(string username, string password)
        {
            await Task.Yield();
        }

        public async Task LogoutAsync()
        {
            await Task.Yield();
        }
    }
}
