using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace DesafioPastelariaSMN.Pages
{
    public class TarefasModel : PageModel
    {
        private readonly DBConexao _dbConexao;
        private string connectionString = "Server=localhost;Database=desafio;User ID=root;Password=SenhaDoSeuBanco;";

        public TarefasModel(DBConexao dbConexao)
        {
            _dbConexao = dbConexao;
        }

        [BindProperty]
        public ModeloUser Usuario { get; set; }
        [BindProperty]
        public List<TarefasFunc> TarefasFuncionarioAll { get; set; }

        [BindProperty]
        public string Message { get; set; }

        public MySqlConnection Connect()
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            return connection;
        }

        public void visTarefa(string emailFuncionarioLogado)
        {
            Message = "Clique no botão verde pra concluir tarefas.";
            TarefasFuncionarioAll = new List<TarefasFunc>();

            try
            {
                using (var connection = Connect())
                {
                    string query = "SELECT id, titulo, mensagem, funcResp, estado, prazo FROM tarefas";
                    using (var cmd = new MySqlCommand(query, connection))
                    {

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

                                TarefasFuncionarioAll.Add(tarefa);
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

        public void OnGet()
        {
            string emailUsuarioLogado = User.Identity.Name;

            try
            {
                bool isAdmin = _dbConexao.TipoUsuario(emailUsuarioLogado);
                ViewData["UserTipo"] = isAdmin ? 0 : 1;

                // Buscar as tarefas do funcionário logado
                visTarefa(emailUsuarioLogado);
            }
            catch (Exception ex)
            {
                ViewData["UserTipo"] = 1;
            }
        }
        [HttpPost]
        public IActionResult OnPost(int tarefaId, string action)
        {
            try
            {
                if (action == "Concluir")
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
                    Message = "Tarefa marcada como concluída.";
                    return Page();

                }


                else if (action == "Excluir")
                {

                    using (var connection = Connect())
                    {
                        string query = "DELETE FROM tarefas WHERE id = @tarefaId";
                        using (var cmd = new MySqlCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@tarefaId", tarefaId);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    Message = "Tarefa Excluida";
                    return Page();
                }
            }
            catch (Exception ex)
            {
                Message = "Erro ao excluir tarefa: " + ex.Message;
                return Page();
            }
            return Page();
        }
    } 
}
