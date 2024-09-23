using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System.Net.Mail;
using System.Net;
using System.Runtime.Serialization;
using NuGet.Protocol.Plugins;

namespace DesafioPastelariaSMN
{
    public class DBConexao
    {
        DateTimeFormat prazo = new DateTimeFormat("YYYY-MM-DD");

        public string con()
        {
            string connectionString = "Server=localhost;Database=desafio;User ID=root;Password=SenhaDoSeuBanco;";
            return connectionString;
        }

        public MySqlConnection Connect()
        {
            MySqlConnection connection = new MySqlConnection(con());
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
    }
}