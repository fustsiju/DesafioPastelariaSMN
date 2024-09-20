using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Mysqlx.Session;

namespace DesafioPastelariaSMN
{
    public class ModeloUser
    {
        public string nome { get; set; }
        public DateTime nasec { get; set; }
        public string email { get; set; }
        public string senha { get; set; }
        public int telefone { get; set; }
        public int celular { get; set; }
        public string rua { get; set; }
        public int numero { get; set; }


    }
}
