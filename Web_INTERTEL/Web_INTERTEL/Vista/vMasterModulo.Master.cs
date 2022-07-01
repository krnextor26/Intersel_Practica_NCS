using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web_INTERTEL.Vista
{
    public partial class vMasterModulo : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session.IsNewSession)
                {
                    Response.Redirect("~/Login.aspx");
                }
                else
                {
                    lbNameUser.Text = Session["NombreCompleto"].ToString();
                }

            }
        }

        protected void imgSalir_Click(object sender, ImageClickEventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/login.aspx");
        }

        protected void btnCarga_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Vista/vCarga.aspx");
        }

        protected void btnConsulta_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Vista/vConsulta.aspx");
        }

        protected void btnUusarios_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Vista/vUsuarios.aspx");
        }
    }
}