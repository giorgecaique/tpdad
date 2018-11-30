using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAppOwin.Models;
using System.Data.SqlClient;

namespace WebAppOwin.Controllers
{
    public class usuarioController : ApiController
    {
        [Authorize]
        [HttpGet]
        [Route("api/usuario")]
        public object Get()
        {
            List<usuario> data = new List<usuario>();

            var conexao = Contexto.conexaoBD();

            var consulta = new SqlCommand("SELECT * FROM usuario", conexao);

            conexao.Open();

            SqlDataReader resultadoConsulta = consulta.ExecuteReader();

            while (resultadoConsulta.Read())
            {
                int uid = (int)resultadoConsulta["id"];
                string nome = resultadoConsulta["nome"].ToString();
                string cpf_cnpj = resultadoConsulta["cpf_cnpj"].ToString();
                string email = resultadoConsulta["email"].ToString();
                string endereco = resultadoConsulta["endereco"].ToString();
                string telefone = resultadoConsulta["telefone"].ToString();
                DateTime data_nascimento = (DateTime)resultadoConsulta["data_nascimento"];
                string tipo = resultadoConsulta["tipo"].ToString();
                string sexo = resultadoConsulta["sexo"].ToString();
                int tempo_permanencia = (int)resultadoConsulta["tempo_permanencia"];
                int quantidade_pessoas = (int)resultadoConsulta["quantidade_pessoas"];
                double meta = (double)resultadoConsulta["meta"];

                usuario aux = new usuario
                {
                    id = uid,
                    nome = nome,
                    cpf_cnpj = cpf_cnpj,
                    email = email,
                    endereco = endereco,
                    telefone = telefone,
                    data_nascimento = data_nascimento,
                    tipo = tipo,
                    sexo = sexo,
                    tempo_permanencia = tempo_permanencia,
                    quantidade_pessoas = quantidade_pessoas,
                    meta = meta
                };

                data.Add(aux);
            }

            conexao.Close();

            string status = HttpStatusCode.OK.ToString();
            string message = string.Empty;
            object response = new { message, status, data };

            return response;
        }

        [Authorize]
        [HttpGet]
        [Route("api/usuario/{id}")]
        public object Get(int id)
        {
            var conexao = Contexto.conexaoBD();

            var consulta = new SqlCommand("SELECT * FROM usuario WHERE id = " + id, conexao);

            conexao.Open();

            SqlDataReader resultadoConsulta = consulta.ExecuteReader();


            while (resultadoConsulta.Read())
            {
                int oid = (int)resultadoConsulta["id"];
                string nome = resultadoConsulta["nome"].ToString();
                string cpf_cnpj = resultadoConsulta["cpf_cnpj"].ToString();
                string email = resultadoConsulta["email"].ToString();
                string endereco = resultadoConsulta["endereco"].ToString();
                string telefone = resultadoConsulta["telefone"].ToString();
                DateTime data_nascimento = (DateTime)resultadoConsulta["data_nascimento"];
                string tipo = resultadoConsulta["tipo"].ToString();
                string sexo = resultadoConsulta["sexo"].ToString();
                int tempo_permanencia = (int)resultadoConsulta["tempo_permanencia"];
                int quantidade_pessoas = (int)resultadoConsulta["quantidade_pessoas"];
                double meta = (double)resultadoConsulta["meta"];

                usuario data = new usuario
                {
                    id = oid,
                    nome = nome,
                    cpf_cnpj = cpf_cnpj,
                    email = email,
                    endereco = endereco,
                    telefone = telefone,
                    data_nascimento = data_nascimento,
                    tipo = tipo,
                    sexo = sexo,
                    tempo_permanencia = tempo_permanencia,
                    quantidade_pessoas = quantidade_pessoas,
                    meta = meta
                };
                conexao.Close();

                string status = HttpStatusCode.OK.ToString();
                string message = string.Empty;
                object response = new { message, status, data };

                return response;
            }

            return null;
        }

        [Authorize]
        [HttpGet]
        [Route("api/usuarioPorNome/{usr_nome}")]
        public object GetByName(string usr_nome)
        {
            List<usuario> data = new List<usuario>();

            var conexao = Contexto.conexaoBD();

            var consulta = new SqlCommand("SELECT * FROM usuario WHERE nome = '" + usr_nome + "'", conexao);

            conexao.Open();

            SqlDataReader resultadoConsulta = consulta.ExecuteReader();

            while (resultadoConsulta.Read())
            {
                int oid = (int)resultadoConsulta["id"];
                string nome = resultadoConsulta["nome"].ToString();
                string cpf_cnpj = resultadoConsulta["cpf_cnpj"].ToString();
                string email = resultadoConsulta["email"].ToString();
                string endereco = resultadoConsulta["endereco"].ToString();
                string telefone = resultadoConsulta["telefone"].ToString();
                DateTime data_nascimento = (DateTime)resultadoConsulta["data_nascimento"];
                string tipo = resultadoConsulta["tipo"].ToString();
                string sexo = resultadoConsulta["sexo"].ToString();
                int tempo_permanencia = (int)resultadoConsulta["tempo_permanencia"];
                int quantidade_pessoas = (int)resultadoConsulta["quantidade_pessoas"];
                int meta = (int)resultadoConsulta["meta"];

                usuario aux = new usuario
                {
                    id = oid,
                    nome = nome,
                    cpf_cnpj = cpf_cnpj,
                    email = email,
                    endereco = endereco,
                    telefone = telefone,
                    data_nascimento = data_nascimento,
                    tipo = tipo,
                    sexo = sexo,
                    tempo_permanencia = tempo_permanencia,
                    quantidade_pessoas = quantidade_pessoas,
                    meta = meta
                };

                data.Add(aux);
            }

            conexao.Close();

            string status = HttpStatusCode.OK.ToString();
            string message = string.Empty;
            object response = new { message, status, data };

            return response;
        }

        [Authorize]
        [HttpPost]
        [Route("api/usuario")]
        public HttpResponseMessage Post([FromBody] usuario item)
        {   
            try
            {
                InsertUpdate(item);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }


        public void InsertUpdate(usuario usr)
        {
            var conexao = Contexto.conexaoBD();
            using (tpdadbdEntities context = new tpdadbdEntities())
            {
                context.usuario.Add(usr);
                context.SaveChanges();
            }
        }
    }
}