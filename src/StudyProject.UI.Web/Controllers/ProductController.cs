using CategoriasMvc.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudyProject.Application.Identity.AccountViewModels;
using StudyProject.Application.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudyProject.UI.WebCore.Controllers
{
    // [AuthorizeEnum(RoleAuthorize.Admin, RoleAuthorize.Manager)]
    public class ProductController : Controller
    {
        private readonly IProdutoService _produtoService;
        private readonly IAutenticacao _autenticacaoService;
        private string token = string.Empty;
        public ProductController(IProdutoService produtoService, IAutenticacao autenticacaoService)
        {
            _autenticacaoService = autenticacaoService;
            _produtoService = produtoService;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            //verifica se o modelo é válido
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Login Inválido....");
                return View(model);
            }
            //verifica as credenciais do usuário e retorna um valor
            var result = await _autenticacaoService.AutenticaUsuario(model);


            if (result is null)
            {
                ModelState.AddModelError(string.Empty, "Login Inválido....");
                return View(model);
            }

            HttpContext.Response.Cookies.Append("X-Access-Token", result.Token, new CookieOptions()
            {
                Secure = true,
                HttpOnly = true,
                SameSite = SameSiteMode.Strict,
                IsEssential = true
            });
            return Redirect("Home/Index");

        }

        //[AuthorizePolicyEnum(PERMISSIONS.Index)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductVM>>> Index()
        {
            //extrai o token do cookie
            var result = await _produtoService.GetProdutos(ObtemTokenJwt());

            if (result is null)
                return View("Error");

            return View(result);
        }

        private string ObtemTokenJwt()
        {
            if (HttpContext.Request.Cookies.ContainsKey("X-Access-Token"))
                token = HttpContext.Request.Cookies["X-Access-Token"].ToString();

            return token;
        }
    }
}