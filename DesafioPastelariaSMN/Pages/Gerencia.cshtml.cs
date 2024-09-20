using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using MySql.Data.MySqlClient;
using System.Security.Claims;
using static Azure.Core.HttpHeader;
using System.Security.Cryptography;
using System;

namespace DesafioPastelariaSMN.Pages
{
    public class GerenciaModel : PageModel
    {

        private readonly DBConexao _dbConexao;

        public GerenciaModel(DBConexao dbConexao)
        {
            _dbConexao = dbConexao;
        }

        [BindProperty]
        public ModeloUser Usuario { get; set; }

        public TarefasFunc Tarefas { get; set; }

        public string Message { get; set; }
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
            if (string.IsNullOrEmpty(Usuario.email) || string.IsNullOrEmpty(Usuario.senha) || )
            {
                Message = "Email e senha são obrigatórios.";
                return Page();
            }

            using (var connection = _dbConexao.Connect())
            {
                string query = "INSERT INTO tarefas (mensagem, funcResp, estado, prazo) VALUES (@msg, @func, @estado, @prazo)";
                MySqlCommand cmdMandarTarefas = new MySqlCommand(query, Connect());
                cmdMandarTarefas.Parameters.AddWithValue("@msg", Tarefas.msg);
                cmdMandarTarefas.Parameters.AddWithValue("@func", Tarefas.func);
                cmdMandarTarefas.Parameters.AddWithValue("@estado", Tarefas.estado);
                cmdMandarTarefas.Parameters.AddWithValue("@prazo", Tarefas.prazo);

                object result = cmdMandarTarefas.ExecuteNonQuery();

                if(result != "0")
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
