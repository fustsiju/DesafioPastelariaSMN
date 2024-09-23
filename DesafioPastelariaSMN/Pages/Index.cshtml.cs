using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using System.Security.Claims;

namespace DesafioPastelariaSMN.Pages
{
    public class IndexModel : PageModel
    {

        private readonly DBConexao _dbConexao;

        public IndexModel(DBConexao dbConexao)
        {
            _dbConexao = dbConexao;
        }

        [BindProperty]
        public ModeloUser Usuario { get; set; }

        public string Message { get; set; }

        public void OnGet()
        {
            string emailUsuarioLogado = User.Identity.Name;

            try
            {
                bool isAdmin = _dbConexao.TipoUsuario(emailUsuarioLogado);

                ViewData["UserTipo"] = isAdmin ? 0 : 1;
            }
            catch (Exception ex)
            {
                ViewData["UserTipo"] = 1;
        
    }
}


public async Task<IActionResult> OnPost()
{
    if (string.IsNullOrEmpty(Usuario.emailUser) || string.IsNullOrEmpty(Usuario.senhaUser))
    {
        Message = "Email e senha são obrigatórios.";
        return Page();
    }

    using (var connection = _dbConexao.Connect())
    {
        string query = "SELECT tipo FROM usuario WHERE email = @email AND senha = @senha";
        using (var command = new MySqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@email", Usuario.emailUser);
            command.Parameters.AddWithValue("@Senha", Usuario.senhaUser);

            object tipo = command.ExecuteScalar();
            Message = $"{tipo}";
            if (tipo != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, Usuario.emailUser),
                    new Claim(ClaimTypes.Role, tipo.ToString())
                };

                var identity = new ClaimsIdentity(claims, "CookieAuth");
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("CookieAuth", principal);

                if (tipo.ToString() == "False")
                {
                    return RedirectToPage("/Gerencia");
                }
                else if (tipo.ToString() == "True")
                {
                    return RedirectToPage("/Funcionario");
                }
            }
            else
            {
                Message = "Credenciais inválidas.";
                return Page();
            }
        }
    }

    return Page();
}

        }
    }
