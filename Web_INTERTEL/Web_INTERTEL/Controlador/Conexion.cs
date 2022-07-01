using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Web_INTERTEL.Controlador
{
    public class Conexion
    {
        public static string cadenaConexion = ConfigurationManager.ConnectionStrings["Cadena_Conexion"].ConnectionString;
    }
}