using DesafioPastelariaSMN.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using NuGet.Protocol.Plugins;
using System.Net.Mail;
using System.Net;
using Humanizer;
using Microsoft.EntityFrameworkCore;

namespace DesafioPastelariaSMN.Pages
{
    public class TarefasFunc
    {
        private string titulo { get; set; }
        private string msg { get; set; }
        private string func { get; set; }
        private string estado { get; set; }
        private DateTime prazo { get; set; }
        private int id { get; set; }

        public string meuTitulo
        {
            get { return titulo; }
            set { titulo = value; }
        }
        public string msgTarefa
        {
            get { return msg; }
            set { msg = value; }
        }
        public string funcResp
        {
            get { return func; }
            set { func = value; }
        }
        public string estadoTarefa
        {
            get { return estado; }
            set { estado = value; }
        }
        public DateTime prazoTarefa
        {
            get { return prazo; }
            set { prazo = value; }
        }
        public int idTarefa
        {
            get { return id; }
            set { id = value; }
        }

        [BindProperty]
        public ModeloUser Usuario { get; set; }
        public List<TarefasFunc> TarefasFuncionarioGeral { get; set; }

        public bool EstadoTipo { get; set; }

        public string Message { get; set; }
    }

}
