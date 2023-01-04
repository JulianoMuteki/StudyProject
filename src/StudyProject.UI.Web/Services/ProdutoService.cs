using CategoriasMvc.Models;
using StudyProject.Application.ViewModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CategoriasMvc.Services;

public class ProdutoService : IProdutoService
{
    private readonly IHttpClientFactory _clientFactory;
    private const string apiEndpoint = "/api/Product/";
    private readonly JsonSerializerOptions _options;
    private ProductVM produtoVM;
    private IEnumerable<ProductVM> produtosVM;

    public ProdutoService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
        _options = new JsonSerializerOptions {  PropertyNameCaseInsensitive = true };
    }

    public async Task<IEnumerable<ProductVM>> GetProdutos(string token)
    {
        var client = _clientFactory.CreateClient("ProdutosApi");
        PutTokenInHeaderAuthorization(token, client);
        using (var response = await client.GetAsync(apiEndpoint))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                produtosVM = await JsonSerializer
                               .DeserializeAsync<IEnumerable<ProductVM>>
                               (apiResponse, _options);
            }
            else
            {
                return null;
            }
        }
        return produtosVM;
    }
    public async Task<ProductVM> GetProdutoPorId(int id, string token)
    {
        var client = _clientFactory.CreateClient("ProdutosApi");
        PutTokenInHeaderAuthorization(token, client);
        using (var response = await client.GetAsync(apiEndpoint + id))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                produtoVM = await JsonSerializer
                              .DeserializeAsync<ProductVM>
                              (apiResponse, _options);

            }
            else
            {
                return null;
            }
        }
        return produtoVM;
    }

    public async  Task<ProductVM> CriaProduto(ProductVM produtoVM, string token)
    {
        var client = _clientFactory.CreateClient("ProdutosApi");
        PutTokenInHeaderAuthorization(token, client);

        var produto = JsonSerializer.Serialize(produtoVM);
        StringContent content = new StringContent(produto, Encoding.UTF8, "application/json");

        using (var response = await client.PostAsync(apiEndpoint, content))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                produtoVM = await JsonSerializer
                             .DeserializeAsync<ProductVM>
                             (apiResponse, _options);
            }
            else
            {
                return null;
            }
        }
        return produtoVM;
    }
    public async  Task<bool> AtualizaProduto(int id, ProductVM produtoVM, string token)
    {
        var client = _clientFactory.CreateClient("ProdutosApi");
        PutTokenInHeaderAuthorization(token, client);

        using (var response = await client.PutAsJsonAsync(apiEndpoint + id, produtoVM))
        {
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    public async Task<bool> DeletaProduto(int id, string token)
    {
        var client = _clientFactory.CreateClient("ProdutosApi");
        PutTokenInHeaderAuthorization(token, client);

        using (var response = await client.DeleteAsync(apiEndpoint + id))
        {
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
        }
        return false;
    }

    private static void PutTokenInHeaderAuthorization(string token, HttpClient client)
    {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }
}
