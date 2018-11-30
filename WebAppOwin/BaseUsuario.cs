using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppOwin.Models;

namespace WebAppOwin
{
    public static class BaseUsuarios
    {
        public static IEnumerable<usuario> usuarios()
        {

            string configuracao = "Server=tpdadbd.database.windows.net; Database=tpdadbd; Uid=tpdadbd; Pwd=tp@123456;";

            SqlConnection conexao = new SqlConnection(configuracao);

            String select = "SELECT * FROM usuario";

            SqlCommand consulta = new SqlCommand(select, conexao);

            conexao.Open();

            SqlDataReader resultadoConsulta = consulta.ExecuteReader();

            List<usuario> usuarios = new List<usuario>();

            while (resultadoConsulta.Read())
            {
                int id = Convert.ToInt32(resultadoConsulta["id"].ToString());
                String email = resultadoConsulta["email"].ToString();
                String senha = resultadoConsulta["senha"].ToString();

                usuario aux = new usuario {id = id, nome = email, senha = senha };

                usuarios.Add(aux);
            }

            conexao.Close();

            return usuarios;
        }
    }
}