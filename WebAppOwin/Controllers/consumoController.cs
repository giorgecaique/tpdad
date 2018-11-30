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
    public class consumoController : ApiController
    {
        SqlConnection _conexao = Contexto.conexaoBD();

        #region Gets

        [Authorize]
        [HttpGet]
        [Route("api/consumo")]
        public object Get()
        {
            List<object> data = new List<object>();

            var conexao = Contexto.conexaoBD();

            var consulta = new SqlCommand("SELECT * FROM consumo", conexao);

            conexao.Open();

            SqlDataReader resultadoConsulta = consulta.ExecuteReader();

            while (resultadoConsulta.Read())
            {
                int id = (int)resultadoConsulta["id"];
                int id_usuario = (int)resultadoConsulta["id_usuario"];
                int qtd_pessoas = (int)resultadoConsulta["qtd_pessoas"];
                DateTime tempo_permanencia = (DateTime)resultadoConsulta["id"];
                int litros = (int)resultadoConsulta["litros"];
                DateTime referencia = (DateTime)resultadoConsulta["referencia"];
                string ano_mes = resultadoConsulta["ano_mes"].ToString();
                double consumo_metros_cubicos = (double)resultadoConsulta["consumo_metros_cubicos"];
                double leitura = (double)resultadoConsulta["leitura"];
                double valor = (double)resultadoConsulta["valor"];

                object aux = new
                {
                    id,
                    id_usuario,
                    qtd_pessoas,
                    tempo_permanencia,
                    litros,
                    referencia,
                    ano_mes,
                    consumo_metros_cubicos,
                    leitura,
                    valor
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
        [Route("api/consumo/{id}")]
        public object Get(int uid)
        {
            var conexao = Contexto.conexaoBD();

            var consulta = new SqlCommand(string.Format("SELECT * FROM consumo where consumo_id = {0}", uid), conexao);

            conexao.Open();

            SqlDataReader resultadoConsulta = consulta.ExecuteReader();

            while (resultadoConsulta.Read())
            {
                int id = (int)resultadoConsulta["id"];
                int id_usuario = (int)resultadoConsulta["id_usuario"];
                int qtd_pessoas = (int)resultadoConsulta["qtd_pessoas"];
                TimeSpan tempo_permanencia = (TimeSpan)resultadoConsulta["id"];
                int litros = (int)resultadoConsulta["litros"];
                DateTime referencia = (DateTime)resultadoConsulta["referencia"];
                string ano_mes = resultadoConsulta["ano_mes"].ToString();
                double consumo_metros_cubicos = (double)resultadoConsulta["consumo_metros_cubicos"];
                double leitura = (double)resultadoConsulta["leitura"];
                double valor = (double)resultadoConsulta["valor"];


                consumo data = new consumo()
                {
                    id = id,
                    id_usuario = id_usuario,
                    referencia = referencia,
                    ano_mes = ano_mes,
                    consumo_metros_cubicos = consumo_metros_cubicos,
                    leitura = leitura,
                    valor = valor
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
        [Route("api/consumoPorUsuario/{usuario_id}")]
        public object GetByUserId(int usuario_id)
        {
            List<consumo> data = new List<consumo>();

            var consulta = new SqlCommand(string.Format("SELECT * FROM consumo where usuario_id = {0}", usuario_id), _conexao);

            _conexao.Open();

            SqlDataReader resultadoConsulta = consulta.ExecuteReader();

            while (resultadoConsulta.Read())
            {
                int id = (int)resultadoConsulta["id"];
                int id_usuario = (int)resultadoConsulta["id_usuario"];
                int qtd_pessoas = (int)resultadoConsulta["qtd_pessoas"];
                TimeSpan tempo_permanencia = (TimeSpan)resultadoConsulta["id"];
                int litros = (int)resultadoConsulta["litros"];
                DateTime referencia = (DateTime)resultadoConsulta["referencia"];
                string ano_mes = resultadoConsulta["ano_mes"].ToString();
                double consumo_metros_cubicos = (double)resultadoConsulta["consumo_metros_cubicos"];
                double leitura = (double)resultadoConsulta["leitura"];
                double valor = (double)resultadoConsulta["valor"];


                consumo aux = new consumo()
                {
                    id = id,
                    id_usuario = id_usuario,
                    referencia = referencia,
                    ano_mes = ano_mes,
                    consumo_metros_cubicos = consumo_metros_cubicos,
                    leitura = leitura,
                    valor = valor
                };

                data.Add(aux);
            }

            _conexao.Close();

            string status = HttpStatusCode.OK.ToString();
            string message = string.Empty;
            object response = new { message, status, data };

            return response;
        }

        [Authorize]
        [HttpGet]
        [Route("api/consumoAnual")]
        public object GetConsumoMensal(int usuario_id, int ano)
        {
            List<object> data = new List<object>();

            var consulta = new SqlCommand(string.Format("select ano_mes, sum(valor) valor, (sum(valor) / DAY(EOMONTH(referencia))) media_diaria from consumo where id_usuario = {0} and year(referencia) = {1} group by ano_mes,DAY(EOMONTH(referencia))", usuario_id, ano), _conexao);

            _conexao.Open();

            SqlDataReader resultadoConsulta = consulta.ExecuteReader();

            while (resultadoConsulta.Read())
            {
                string ano_mes = resultadoConsulta["ano_mes"].ToString();
                double valor = (double)resultadoConsulta["valor"];
                double media_diaria = (double)resultadoConsulta["media_diaria"];

                object aux = new 
                {
                    ano_mes,
                    valor,
                    media_diaria
                };
                data.Add(aux);
            }

            _conexao.Close();

            var responseResult = new HttpResponseMessage(HttpStatusCode.OK);
            string status = HttpStatusCode.OK.ToString();
            string message = string.Empty;
            object response = new { message, status, data };

            return response;
        }

        [Authorize]
        [HttpGet]
        [Route("api/consumoMensal")]
        public object GetConsumoAnual(int usuario_id, string mes_ano)
        {
            List<object> data = new List<object>();

            var consulta = new SqlCommand(string.Format("select ano_mes, sum(valor) valor, (sum(valor) / DAY(EOMONTH(referencia))) media_diaria from consumo where id_usuario = {0} and ano_mes = {1} group by ano_mes,DAY(EOMONTH(referencia))", usuario_id, mes_ano), _conexao);

            _conexao.Open();

            SqlDataReader resultadoConsulta = consulta.ExecuteReader();

            while (resultadoConsulta.Read())
            {
                string ano_mes = resultadoConsulta["ano_mes"].ToString();
                double valor = (double)resultadoConsulta["valor"];
                double media_diaria = (double)resultadoConsulta["media_diaria"];

                object aux = new
                {
                    ano_mes,
                    valor,
                    media_diaria
                };
                data.Add(aux);
            }

            _conexao.Close();

            var responseResult = new HttpResponseMessage(HttpStatusCode.OK);
            string status = HttpStatusCode.OK.ToString();
            string message = string.Empty;
            object response = new { message, status, data };

            return response;
        }

        #endregion

        #region Posts

        [Authorize]
        [HttpPost]
        [Route("api/consumo")]
        public object Post([FromUri] consumo item)
        {
            try
            {
                var data = calcularConsumo(item);
                InsertUpdate(data);
                string status = HttpStatusCode.OK.ToString();
                string message = string.Empty;
                object response = new { message, status, data };
                return response;
            }
            catch (Exception ex)
            {
                string status = HttpStatusCode.InternalServerError.ToString();
                string message = string.Empty;
                object response = new { message, status };
                return response;
            }
        }

        public consumo GetUltimaLeitura(consumo c)
        {
            string referencia = c.ano_mes + "-01";
            _conexao.Open();
            string query = string.Format("select * from consumo where id_usuario = {0} and ano_mes = cast(year(DATEADD(month, -1, '{1}')) as varchar(4)) + '-' + case when month(DATEADD(month, -1, '{1}')) < 10 then '0' + cast(month(DATEADD(month, -1, {1})) as varchar(1)) else cast(month(DATEADD(month, -1, '{1}')) as varchar(2)) end", c.id_usuario, referencia);
            var consulta = new SqlCommand(query, _conexao);

            SqlDataReader resultadoConsulta = consulta.ExecuteReader();

            while (resultadoConsulta.Read())
            {
                consumo aux = new consumo()
                {
                    id = (int)resultadoConsulta["id"],
                    id_usuario = (int)resultadoConsulta["id_usuario"],
                    referencia = (DateTime)resultadoConsulta["referencia"],
                    ano_mes = resultadoConsulta["ano_mes"].ToString(),
                    leitura = (double)resultadoConsulta["leitura"]
                };
                _conexao.Close();
                return aux;
            }
            return null;
        }

        #endregion

        #region Métodos

        public consumo calcularConsumo(consumo c)
        {
            double valor = 0;
            double consumo_atual = 0;

            consumo ultimo_consumo = GetUltimaLeitura(c);

            if (ultimo_consumo == null)
                consumo_atual = c.leitura;
            else
                consumo_atual = (double)c.leitura - (double)ultimo_consumo.leitura;

            int dezenas = (int)Math.Floor(consumo_atual / 10);

            if (dezenas-1 >= 0)
                valor += 16.53;
            if (dezenas-1 >= 1)
                valor += 10 * 1.27;
            if (dezenas-1 >= 2)
                valor += 10 * 1.45;
            if (dezenas-1 >= 3)
                valor += 10 * 2;
            if (dezenas-1 >= 4)
                valor += 10 * 3.45;

            double x = 0;
            switch (dezenas)
            {
                case 1:
                    x = 1.27;
                    break;
                case 2:
                    x = 1.45;
                    break;
                case 3:
                    x = 1.27;
                    break;
                case 4:
                    x = 2;
                    break;
                case 5:
                    x = 3.45;
                    break;
                default:
                    x = 4.3;
                    break;
            }

            valor += (consumo_atual - dezenas * 10) * x;

            c.valor = valor;
            c.consumo_metros_cubicos = consumo_atual;

            return c;
        }

        public void InsertUpdate(consumo csm)
        {
            using (tpdadbdEntities context = new tpdadbdEntities())
            {
                var result = context.consumo.Where(x => x.id_usuario == csm.id_usuario && x.ano_mes == csm.ano_mes);
                if (result == null)
                    context.consumo.Add(csm);
                else
                {
                    foreach(consumo item in result)
                    {
                        item.leitura = csm.leitura;
                        item.consumo_metros_cubicos = csm.consumo_metros_cubicos;
                        item.valor = csm.valor;
                    }
                }
                context.SaveChanges();
            }
        }

        #endregion
    }
}