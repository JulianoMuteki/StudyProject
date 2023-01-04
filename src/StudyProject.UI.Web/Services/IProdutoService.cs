using CategoriasMvc.Models;
using StudyProject.Application.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CategoriasMvc.Services;

public interface IProdutoService
{
    Task<IEnumerable<ProductVM>> GetProdutos(string token);
    Task<ProductVM> GetProdutoPorId(int id, string token);
    Task<ProductVM> CriaProduto(ProductVM produtoVM, string token);
    Task<bool> AtualizaProduto(int id, ProductVM produtoVM, string token);
    Task<bool> DeletaProduto(int id, string token);
}
