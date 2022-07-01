using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web_INTERTEL.Controlador;
using Web_INTERTEL.Modelo;

namespace Web_INTERTEL.Vista
{
    public partial class vConsulta : System.Web.UI.Page
    {
        cConsulta clase = new cConsulta();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session.IsNewSession)
                {
                    Response.Redirect("~/login.aspx");
                }

                cargarListaLineasCelulares();
            }
        }

        protected void gvInforme_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (gvInforme.Rows.Count > 0)
            {
                gvInforme.PageIndex = e.NewPageIndex;
                cargarListaLineasCelulares();
            }
        }

        protected void gvDetalles_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (gvDetalles.Rows.Count > 0)
            {
                gvDetalles.PageIndex = e.NewPageIndex;
                cargarDetalleLineasCelulares(HfMobileLine.Value);
            }
        }

        protected void gvInforme_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Ver")
            {
                HfMobileLine.Value = e.CommandArgument.ToString();

                cargarDetalleLineasCelulares(HfMobileLine.Value);
            }
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            HfMobileLine.Value = "0";
            cargarListaLineasCelulares();
        }

        private void cargarListaLineasCelulares()
        {
            List<mLineasCelulares> lista = null;
            lista = clase.ListaLineasCelulares();
            gvInforme.DataSource = lista;
            gvInforme.DataBind();
            dvConsultar.Visible = true;
            dvDetalle.Visible = false;
        }

        private void cargarDetalleLineasCelulares(string MobileLine)
        {
            List<mDetallesLlamadas> lista = null;
            lista = clase.DetalleLineasCelulares(MobileLine);
            gvDetalles.DataSource = lista;
            gvDetalles.DataBind();
            dvConsultar.Visible = false;
            dvDetalle.Visible = true;
        }        
    }
}