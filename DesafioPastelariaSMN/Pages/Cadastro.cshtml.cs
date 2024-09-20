using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DesafioPastelariaSMN.Pages
{
    public class CadastroModel : PageModel
    {
        private readonly DBConexao _dbConexao;

        public CadastroModel(DBConexao dbConexao)
        {
            _dbConexao = dbConexao;
        }

        [BindProperty]
        public ModeloUser Usuario { get; set; }

        public MySqlConnection Connect()
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            return connection;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrEmpty(Usuario.email) || string.IsNullOrEmpty(Usuario.senha))
            {
                Message = "Email e senha são obrigatórios.";
                return Page();
            }

            using (var connection = _dbConexao.Connect())
            {
                string query = "INSERT INTO usuario (nome, nasec, telefone, celular, email, senha, rua, num, imagem, tipo) VALUES (@nome, @nasec, @tel, @cel, @email, @senha@, @rua, @num, @img, @tipo);";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@nome", Usuario.nome);
                cmd.Parameters.AddWithValue("@nasec", Usuario.nasec);
                cmd.Parameters.AddWithValue("@tel", Usuario.telefone);
                cmd.Parameters.AddWithValue("@cel", Usuario.celular);
                cmd.Parameters.AddWithValue("@email", Usuario.email);
                cmd.Parameters.AddWithValue("@senha", Usuario.senha);
                cmd.Parameters.AddWithValue("@rua", Usuario.rua);
                cmd.Parameters.AddWithValue("@num,", Usuario.numero);
                cmd.Parameters.AddWithValue("@tipo,", '1');

                object result = cmd.ExecuteNonQuery();
                if (result != "0")
                {
                    Message = "Cadastrado!";
                }
                else
                {
                    Message = "Não cadastrado!";
                }

            }
            return Page();
        }
    }
}
}

