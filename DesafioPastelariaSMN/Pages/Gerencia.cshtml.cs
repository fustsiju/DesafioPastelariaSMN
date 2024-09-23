using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using MySql.Data.MySqlClient;
using System.Security.Claims;
using static Azure.Core.HttpHeader;
using System.Security.Cryptography;
using System;
using System.Linq.Expressions;
using System.Net.Mail;
using System.Net;

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

        [BindProperty]
        public TarefasFunc TarefasFunc { get; set; }

        public string Message { get; set; }

        public bool MessageType { get; set; }

        

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
            if (TarefasFunc == null)
            {
                Message = "Erro: A instância de Tarefas não foi inicializada.";
                return Page();
            }

            if (string.IsNullOrEmpty(TarefasFunc.funcResp) ||
                string.IsNullOrEmpty(TarefasFunc.meuTitulo) ||
                string.IsNullOrEmpty(TarefasFunc.msgTarefa))
            {
                Message = "Todos os campos são obrigatórios!";
                return Page();
            } else if(TarefasFunc.prazoTarefa < DateTime.UtcNow)
            {
                Message = "A data não pode ser anterior ao dia atual";
                return Page();
            }

            try
            {

                using (var connection = _dbConexao.Connect())
                {
                    string veriCad = "SELECT COUNT(*) FROM usuario WHERE email = @func";
                    MySqlCommand cmdVeriCad = new MySqlCommand(veriCad, connection);
                    cmdVeriCad.Parameters.AddWithValue("@func", TarefasFunc.funcResp);
                    var veriResult = cmdVeriCad.ExecuteScalar().ToString();

                    string queryNome = "SELECT nome FROM usuario WHERE email = @email";
                    MySqlCommand cmdQueryNome = new MySqlCommand(queryNome, connection);
                    cmdQueryNome.Parameters.AddWithValue("@email", TarefasFunc.funcResp);
                    if(veriResult == "0")
                    {
                        Message = "Email não pertence a um funcionário.";
                        return Page();
                    }
                    else
                    {
                        MessageType = true;
                        string query = "INSERT INTO tarefas (titulo, mensagem, funcResp, estado, prazo) VALUES (@titulo, @msg, @func, @estado, @prazo)";
                        MySqlCommand cmdMandarTarefas = new MySqlCommand(query, connection);
                        cmdMandarTarefas.Parameters.AddWithValue("@msg", TarefasFunc.msgTarefa);
                        cmdMandarTarefas.Parameters.AddWithValue("@func", TarefasFunc.funcResp);
                        cmdMandarTarefas.Parameters.AddWithValue("@titulo", TarefasFunc.meuTitulo);
                        cmdMandarTarefas.Parameters.AddWithValue("@prazo", TarefasFunc.prazoTarefa);
                        cmdMandarTarefas.Parameters.AddWithValue("@estado", "Pendente");

                        int result = cmdMandarTarefas.ExecuteNonQuery();

                        if (result > 0)
                        {
                            Message = "Tarefa enviada!";
                            string fromMail = "email@gmail.com";
                            string fromPassword = "senhaSMTP";
                            MailMessage message = new MailMessage();
                            message.From = new MailAddress(fromMail);
                            message.Subject = $"Nova tarefa - {DateTime.UtcNow.ToShortDateString()}";
                            message.To.Add(new MailAddress($"{TarefasFunc.funcResp}"));
                            message.Body = $"Título: {TarefasFunc.meuTitulo}\n\nDescrição: {TarefasFunc.msgTarefa} \n\nPrazo: {TarefasFunc.prazoTarefa.ToShortDateString()} \n\nAtt, \nGestor Pastelaria.";


                            var smtpClient = new SmtpClient("smtp.gmail.com")
                            {
                                Port = 587,
                                Credentials = new NetworkCredential(fromMail, fromPassword),
                                EnableSsl = true,
                            };
                            smtpClient.Send(message);
                        }
                        else
                        {
                            Message = "Erro no envio da tarefa!";
                        }
                    }
                    return Page();

                }
               
            }
            catch (Exception ex)
            {
                MessageType = false;
                Message = "Erro ao processar a tarefa: " + ex.Message;
            }
            return Page();
        }
    }
}
