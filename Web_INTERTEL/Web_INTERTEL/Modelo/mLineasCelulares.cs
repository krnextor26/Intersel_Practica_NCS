using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_INTERTEL.Modelo
{
    public class mLineasCelulares
    {
        public int MobileLineId { get; set; }
        public string MobileLine { get; set; }
        public string Description { get; set; }
        public int EstatusId { get; set; }
        public string Estatus { get; set; }
        public int UsuarioAltaId { get; set; }
        public string Usuario { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime FechaUpdate { get; set; }
    }
}