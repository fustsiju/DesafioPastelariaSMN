using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using System;
using System.IO;
using System.Linq.Expressions;
using static System.Net.Mime.MediaTypeNames;

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
        public string Message { get; set; }
        public bool MessageType { get; set; }


        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost() 
        {

            try
            {

                if (string.IsNullOrEmpty(Usuario.nomeUser) || string.IsNullOrEmpty(Usuario.emailUser) ||
                    string.IsNullOrEmpty(Usuario.ruaUser) || int.IsNegative(Usuario.numUser) || int.IsNegative(Usuario.telUser) || int.IsNegative(Usuario.celUser))
                {
                    Message = "Preencha todos os dados!.";
                    return Page();
                }
                else if (Usuario.nasecUser >= DateTime.UtcNow)
                {
                    Message = "A data de nascimento não é válida.";
                    return Page();
                }

                using (var connection = _dbConexao.Connect())
                {


                    string query = "INSERT INTO usuario (nome, nasec, telefone, celular, email, senha, rua, num, tipo, foto) VALUES (@nome, @nasec, @tel, @cel, @email, @senha, @rua, @num,1, @foto);";

                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@nome", Usuario.nomeUser);
                    cmd.Parameters.AddWithValue("@nasec", Usuario.nasecUser);
                    cmd.Parameters.AddWithValue("@tel", Usuario.telUser);
                    cmd.Parameters.AddWithValue("@cel", Usuario.celUser);
                    cmd.Parameters.AddWithValue("@email", Usuario.emailUser);
                    cmd.Parameters.AddWithValue("@senha", Usuario.senhaUser);
                    cmd.Parameters.AddWithValue("@rua", Usuario.ruaUser);
                    cmd.Parameters.AddWithValue("@num", Usuario.numUser);
                    cmd.Parameters.AddWithValue("@foto", Usuario.foto);


                    int result = cmd.ExecuteNonQuery();
                    if (result != 0)
                    {
                        MessageType = true;
                        Message = "Cadastrado!";
                        return Page();
                    }
                    else
                    {
                        Message = "Não cadastrado, verifique os campos!";
                        return Page();
                    }

                    return Page();
                }
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1062)
                {
                    MessageType = false;
                    Message = "Usuario já cadastrado, use outro email.";
                    return Page();
                }
                else
                {
                    Message = "Erro: " + ex.Message + Usuario.foto;
                }
            }
            return Page();
        }
    }
}

