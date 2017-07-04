using System.Threading.Tasks;

namespace HiveHome.Client
{
    public interface IHiveHomeClient
    {
        Task LoginAsync(string username, string password);

        Task LogoutAsync();
    }
}