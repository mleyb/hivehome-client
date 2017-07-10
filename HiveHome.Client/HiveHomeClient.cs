using HiveHome.Client.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace HiveHome.Client
{
    public class HiveHomeClient : IHiveHomeClient, IDisposable
    {
        // https://api.prod.bgchprod.info:8443/api/docs?docContext=v6&apiVersion=6.5

        private static readonly ILog _logger = LogProvider.For<HiveHomeClient>();

        private HttpClient _client = new HttpClient();

        public string SessionId { get; private set; }

        public HiveHomeClient()
        {
            _client.BaseAddress = new Uri("https://api-prod.bgchprod.info:443/omnia");

            _client.DefaultRequestHeaders.Add("Content-Type", "application/vnd.alertme.zoo-6.1+json");
            _client.DefaultRequestHeaders.Add("Accept", "application/vnd.alertme.zoo-6.1+json");
            _client.DefaultRequestHeaders.Add("X-Omnia-Client", "Hive Web Dashboard");
        }

        public void Dispose()
        {
            try
            {                
                LogoutAsync().Wait();
            }
            catch (Exception ex)
            {
                _logger.Warn(ex, "Exception on attempt to end session");
            }
            finally
            {
                _client.Dispose();
            }
        }

        public async Task LoginAsync(string username, string password)
        {
            if (!String.IsNullOrEmpty(SessionId))
                throw new InvalidOperationException("Session already exists");

            JObject body = JObject.FromObject(new
            {
                sessions = new
                {
                    username = username,
                    password = password,
                    caller = "WEB"
                }
            });
            
            HttpResponseMessage response = await _client.PostAsync("/auth/sessions", new StringContent(body.ToString()));

            // TODO - get sessionId from response & set SessionId
        }

        public async Task LogoutAsync()
        {
            if (!String.IsNullOrEmpty(SessionId))
                throw new InvalidOperationException("No session exists");

            await _client.DeleteAsync($"/auth/sessions/{SessionId}");

            SessionId = null;
        }
    }
}
