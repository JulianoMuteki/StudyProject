using CategoriasMvc.Models;
using StudyProject.Application.Identity.AccountViewModels;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CategoriasMvc.Services;

public class Autenticacao : IAutenticacao
{
    private readonly IHttpClientFactory _clientFactory;
    const string apiEndpointAutentica = "/api/Account/Login/";
    private readonly JsonSerializerOptions _options;
    private TokenViewModel tokenUsuario;
    public Autenticacao(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<TokenViewModel> AutenticaUsuario(LoginViewModel usuarioVM)
    {
        var client = _clientFactory.CreateClient("AutenticaApi");
        var usuario = JsonSerializer.Serialize(usuarioVM);
        StringContent content = new StringContent(usuario, Encoding.UTF8, "application/json");

        using (var response = await client.PostAsync(apiEndpointAutentica, content))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                tokenUsuario = await JsonSerializer
                              .DeserializeAsync<TokenViewModel>
                              (apiResponse, _options);
            }
            else
            {
                return null;
            }
        }
        return tokenUsuario;
    }
}

