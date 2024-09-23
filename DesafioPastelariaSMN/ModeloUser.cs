using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Mysqlx.Session;
using System;
using System.Drawing;

namespace DesafioPastelariaSMN
{
    public class ModeloUser
    {
        private string nome { get; set; }
        private DateTime nasec { get; set; }
        private string email { get; set; }
        private string senha { get; set; }
        private int telefone { get; set; }
        private int celular { get; set; }
        private string rua { get; set; }
        private int numero { get; set; }
        public IFormFile foto { get; set; }
        public string nomeUser
        {
            get { return nome; }
            set { nome = value; }
        }
        public DateTime nasecUser
        {
            get { return nasec; }
            set { nasec = value; }
        }
        public string emailUser
        {
            get { return email; }
            set { email = value; }
        }
        public string senhaUser
        {
            get { return senha; }
            set { senha = value; }
        }
        public int telUser
        {
            get { return telefone; }
            set { telefone = value; }
        }
        public int celUser
        {
            get { return celular; }
            set { celular = value; }
        }
        public string ruaUser
        {
            get { return rua; }
            set { rua = value; }
        }
        public int numUser
        {
            get { return numero; }
            set { numero = value; }
        }

    }
}
