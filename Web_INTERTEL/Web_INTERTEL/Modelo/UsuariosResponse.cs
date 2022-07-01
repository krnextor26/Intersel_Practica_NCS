using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_INTERTEL.Modelo
{
    public class UsuariosResponse
    {
        public int UsuarioID { get; set; }
        public string Usuario { get; set; }
        public string Contrasenia { get; set; }
        public string NombreCompleto { get; set; }
        public int IdEstatus { get; set; }
        public int IdTipoUsuario { get; set; }
        public int IdRol { get; set; }
        public string Telefono { get; set; }
        public string TipoUsuario { get; set; }
        public string Rol { get; set; }

        public string Estatus { get; set; }
        public DateTime FechaAlta { get; set; }
    }
}