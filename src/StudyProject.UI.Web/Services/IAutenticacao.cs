using CategoriasMvc.Models;
using StudyProject.Application.Identity.AccountViewModels;
using System.Threading.Tasks;

namespace CategoriasMvc.Services;

public interface IAutenticacao
{
    Task<TokenViewModel> AutenticaUsuario(LoginViewModel usuarioVM);
}
