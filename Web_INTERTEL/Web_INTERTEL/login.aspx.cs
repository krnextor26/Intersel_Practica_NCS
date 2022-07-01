using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web_INTERTEL.Controlador;
using Web_INTERTEL.Modelo;

namespace Web_INTERTEL
{
    public partial class login : System.Web.UI.Page
    {
        UsuariosData clase = new UsuariosData();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnEntrar_Click(object sender, EventArgs e)
        {
            if (User.Text == "" || Pass.Text == "")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "sweetAlert('Error','Ingrese Usuario/Contraseña', 'error');", true);
            else
            {
                UsuariosResponse Logueo = new UsuariosResponse();
                UsuarioRequest Usuario = new UsuarioRequest();
                Usuario.Usuario = User.Text;
                Usuario.Contrasenia = Pass.Text;

                Logueo = clase.ConsultarUnaPersona(Usuario);

                if (Logueo.UsuarioID > 0)
                {
                    Session.Add("ID", Logueo.UsuarioID);
                    Session.Add("Usuario", Logueo.Usuario);
                    Session.Add("NombreCompleto", Logueo.NombreCompleto);

                    Response.Redirect("~/Vista/vCarga.aspx");

                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "sweetAlert('Error','Credenciales incorrectas', 'error');", true);
            }
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            User.Text = "";
            Pass.Text = "";
        }
    }
}