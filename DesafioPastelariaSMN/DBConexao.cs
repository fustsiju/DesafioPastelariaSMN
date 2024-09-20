using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System.Runtime.Serialization;

namespace DesafioPastelariaSMN
{
    public class DBConexao
    {
        DateTimeFormat prazo = new DateTimeFormat("YYYY-MM-DD");

        private string connectionString = "Server=localhost;Database=desafio;User ID=root;Password=201023;";

        public MySqlConnection Connect()
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            return connection;
        }

        public bool TipoUsuario(string email)
        {
            using (var connection = Connect())
            {
                string queryTipoUsuario = "SELECT tipo FROM usuario where email = @email";
                MySqlCommand cmdTipoUsuario = new MySqlCommand(queryTipoUsuario, connection);
                cmdTipoUsuario.Parameters.AddWithValue("@email", email);
                var tipo = cmdTipoUsuario.ExecuteScalar();
                if (tipo != null)
                {
                    return Convert.ToBoolean(tipo);
                }
                else
                {
                    throw new Exception("Usuário não encontrado.");
                }

            }

        }

        public void addUsuario(string nome, DateTime nasec, int telefone, int celular, string email, string senha, string rua, int num, bool tipo)
        {
            using (var connection = Connect())
            {

                string query = "INSERT INTO usuario (nome, nasec, telefone, celular, email, senha, rua, num, imagem, tipo) VALUES (@nome, @nasec, @tel, @cel, @email, @senha@, @rua, @num, @img, @tipo);";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@nome", nome);
                cmd.Parameters.AddWithValue("@nasec", nasec);
                cmd.Parameters.AddWithValue("@tel", telefone);
                cmd.Parameters.AddWithValue("@cel", celular);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@senha", senha);
                cmd.Parameters.AddWithValue("@rua", rua);
                cmd.Parameters.AddWithValue("@num,", num);
                cmd.Parameters.AddWithValue("@tipo,", tipo);

                cmd.ExecuteNonQuery();
            }
        }
        public void mandarTarefas(string msg,  int func,  int estado, DateTimeFormat prazo)
        {
            string query = "INSERT INTO tarefas (mensagem, funcResp, estado, prazo) VALUES (@msg, @func, @estado, @prazo)";
            MySqlCommand cmdMandarTarefas = new MySqlCommand(query, Connect());
            cmdMandarTarefas.Parameters.AddWithValue("@msg",  msg);
            cmdMandarTarefas.Parameters.AddWithValue("@func", func);
            cmdMandarTarefas.Parameters.AddWithValue("@estado", estado);
            cmdMandarTarefas.Parameters.AddWithValue("@prazo", prazo);

            cmdMandarTarefas.ExecuteNonQuery();
        }

        public void verTarefas()
        {

            string query = "SELECT mensagem, prazo, estado FROM tarefas WHERE id = @id";

        }


    }
}