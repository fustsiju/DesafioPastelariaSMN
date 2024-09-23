using DesafioPastelariaSMN;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Net.Mail;
using System.Net;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DesafioPastelariaSMN.Pages
{
    public class FuncionarioModel : PageModel
    {
        private readonly DBConexao _dbConexao;
        private string connectionString = "Server=localhost;Database=desafio;User ID=root;Password=SenhaDoSeuBanco;";
        public FuncionarioModel(DBConexao dbConexao)
        {
            _dbConexao = dbConexao;
        }

        [BindProperty]
        public ModeloUser Usuario { get; set; }
        [BindProperty]
        public List<TarefasFunc> TarefasFuncionario { get; set; }
        [BindProperty]
        public string Message { get; set; }
        TarefasFunc tarefas = new TarefasFunc();

        public MySqlConnection Connect()
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            return connection;
        }

        public void OnGet()
        {
            string emailUsuarioLogado = User.Identity.Name;

            try
            {
                bool isAdmin = _dbConexao.TipoUsuario(emailUsuarioLogado);
                ViewData["UserTipo"] = isAdmin ? 0 : 1;

                visTarefa(emailUsuarioLogado);
            }
            catch (Exception ex)
            {
                ViewData["UserTipo"] = 1;
            }
        }

        public void visTarefa(string emailFuncionarioLogado)
        {
            Message = "Clique no botão verde pra concluir tarefas.";
            TarefasFuncionario = new List<TarefasFunc>();

            try
            {
                using (var connection = Connect())
                {
                    string query = "SELECT id, titulo, mensagem, funcResp, estado, prazo FROM tarefas WHERE funcResp = @funcEmail";
                    using (var cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@funcEmail", emailFuncionarioLogado);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                TarefasFunc tarefa = new TarefasFunc
                                {
                                    idTarefa = Convert.ToInt32(reader["id"]),
                                    meuTitulo = reader["titulo"].ToString(),
                                    msgTarefa = reader["mensagem"].ToString(),
                                    funcResp = reader["funcResp"].ToString(),
                                    estadoTarefa = reader["estado"].ToString(),
                                    prazoTarefa = Convert.ToDateTime(reader["prazo"])
                                };

                                TarefasFuncionario.Add(tarefa);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Message = "Erro ao buscar tarefas: " + ex.Message;
            }
        }

        
        public IActionResult OnPost(int tarefaId, string action)
        {
            if (action == "Concluir")
            {
                try
                {
                    using (var connection = Connect())
                    {
                        string query = "UPDATE tarefas SET estado = 'Concluída' WHERE id = @tarefaId";
                        using (var cmd = new MySqlCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@tarefaId", tarefaId);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    Message = "Tarefa concluída";
                    string fromMail = "email@gmail.com";
                    string fromPassword = "senhaSMTP";
                    MailMessage message = new MailMessage();
                    message.From = new MailAddress(fromMail);
                    message.Subject = $"Tarefa Concluida - ID: {tarefaId}";
                    message.To.Add(new MailAddress($"fulviost@gmail.com"));
                    message.Body = $"Data de conclusão: {DateTime.UtcNow.ToShortDateString()}";


                    var smtpClient = new SmtpClient("smtp.gmail.com")
                    {
                        Port = 587,
                        Credentials = new NetworkCredential(fromMail, fromPassword),
                        EnableSsl = true,
                    };
                    smtpClient.Send(message);
                    return Page();
                }


                catch (Exception ex)
                {
                    Message = "Erro ao concluir tarefa: " + ex.Message;
                    return Page();
                }
            }
            return Page();
        }
    }

}

