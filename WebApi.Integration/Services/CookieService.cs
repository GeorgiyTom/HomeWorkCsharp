using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;

namespace WebApi.Integration.Services;

public class CookieService
{
    private CookieAuthApiClient _cookieAuthApiClient;

    public CookieService()
    {
        _cookieAuthApiClient = new CookieAuthApiClient();
    }
    
    public async Task<string> GetAdminCookieAsync()
    {
        return await GetCookieAsync("admin", "admin");
    }
    
    public async Task<string> GetCookieAsync(string name, string password)
    {
        var response = await GetCookieInternalAsync(name, password);
        if (response.StatusCode != HttpStatusCode.BadRequest)
            return response.Headers.FirstOrDefault(h=> h.Key == "Set-Cookie").Value.ToList().First();
        return null;
    }
    
    public async Task<HttpResponseMessage> GetCookieInternalAsync(string name, string password)
    {
        if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(password))
            return await _cookieAuthApiClient.GetAuthCookieAsync(name, password);
        return new HttpResponseMessage(HttpStatusCode.BadRequest);
    }
}