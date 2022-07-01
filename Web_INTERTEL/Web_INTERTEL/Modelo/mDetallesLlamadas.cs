using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_INTERTEL.Modelo
{
    public class mDetallesLlamadas
    {
        public int CallDetailId { get; set; }
        public int MobileLineId { get; set; }
        public string MobileLine { get; set; }
        public string Description { get; set; }
        public string CalledPartyNumber { get; set; }
        public string CalledPartyDescription { get; set; }
        public DateTime FechaHora { get; set; }
        public int Duration { get; set; }
        public decimal TotalCost { get; set; }
        public int EstatusId { get; set; }
        public string Estatus { get; set; }
        public int UsuarioAltaId { get; set; }
        public string NombreCompleto { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime FechaUpdate { get; set; }
    }
}