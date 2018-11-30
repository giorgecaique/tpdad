using MySql.Data.Entity;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace WebAppOwin
{
    class Contexto
    {
        public Contexto()
        {

        }

        public static SqlConnection conexaoBD()
        {
            return new SqlConnection("Server=tpdadbd.database.windows.net; Database=tpdadbd; Uid=tpdadbd; Pwd=tp@123456;");
        }
    }
}