using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using PortalPexIM.Models;
using PortalPexIM.Model;

namespace PortalPexIM.Controllers
{
   
    public class UsuarioController : Controller
    {
        peximContext db = new peximContext();
        public IActionResult Index()
        {
            return View();
        }


        private async void Login(Usuario usuario)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.Login),
                new Claim(ClaimTypes.Role, "Usuario_Comum"),
                new Claim(ClaimTypes.NameIdentifier, usuario.SiglaEstado)
            };

            var identidadeDeUsuario = new ClaimsIdentity(claims, "Login")  ;
            ClaimsPrincipal claimPrincipal = new ClaimsPrincipal(identidadeDeUsuario);

            var propriedadesDeAutenticacao = new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = DateTime.Now.ToLocalTime().AddHours(2),
                IsPersistent = true
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPrincipal, propriedadesDeAutenticacao);
        }

        [Authorize]
        public IActionResult UserPage()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("LoginPage");
        }

        [HttpPost]
        public IActionResult LoginPage(Usuario usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //aqui poderia ter alguma requisição para base de dados, estou usando
                    //dados estáticos para não complicar

                    var usuarioBd = db.Usuarios.Where(x => x.Login == usuario.Login && x.Senha == usuario.Senha).FirstOrDefault();
                    if (usuarioBd!= null)
                    {
                        usuario.SiglaEstado = usuarioBd.SiglaEstado;

                        Login(usuario);
                        return RedirectToAction("Index", "Home");

                    }
                    else
                    {

                        ViewBag.Erro = "Usuário e / ou senha incorretos!";
                    }
                }
            }
            catch (Exception)
            {
                ViewBag.Erro = "Ocorreu algum erro ao tentar se logar, tente novamente!";
            }
            return View();
        }

        public IActionResult LoginPage()
        {
            if (User.Identity.IsAuthenticated)
            {
                //return RedirectToAction("UserPage");
            }

            return View();
        }
    }
}
