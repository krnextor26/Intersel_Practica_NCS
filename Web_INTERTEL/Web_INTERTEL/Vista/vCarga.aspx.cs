using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web_INTERTEL.Controlador;

namespace Web_INTERTEL.Vista
{
    public partial class vCarga : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session.IsNewSession)
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
        }

        protected void btnCargaArchivo_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(ddlTipoArchivo.SelectedValue) <= 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "sweetAlert('Error','Seleccione un tipo de archivo a cargar', 'error');", true);
            }
            else if (fupCargaArchivo.HasFile)
            {
                String sRutaDestino = null;
                DataTable resultado = null;
                DataRow row = null;
                string[] res = null;
                cCarga controlador = new cCarga();

                // Crea el directorio en el servidor.
                Directory.CreateDirectory(Server.MapPath("~/Files"));
                // Valida si la ruta del archivo Excel fue especificada o no.  
                string extension = Path.GetExtension(fupCargaArchivo.FileName);

                if (extension != ".csv")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "sweetAlert('Error','Seleccione un tipo de archivo a cargar', 'error');", true);
                    return;
                }

                sRutaDestino = Server.MapPath("~/Files/") +
                                           Path.GetFileName(fupCargaArchivo.PostedFile.FileName);

                // Salvamos el archivo en el servidor, una vez obtenida la ruta destino.
                fupCargaArchivo.PostedFile.SaveAs(sRutaDestino);

                if (Convert.ToInt32(ddlTipoArchivo.SelectedValue) == 1)
                {
                    resultado = controlador.procesaArchivo(sRutaDestino, Convert.ToInt32(Session["ID"]), fupCargaArchivo.FileName, Session["NombreCompleto"].ToString().Trim());
                    ddlTipoArchivo.SelectedValue = "0";
                }
                else
                {
                    resultado = controlador.procesaArchivoDetalle(sRutaDestino, Convert.ToInt32(Session["ID"]), fupCargaArchivo.FileName, Session["NombreCompleto"].ToString().Trim());
                    ddlTipoArchivo.SelectedValue = "0";
                }
                
                row = resultado.Rows[0];
                res = row["cerrar"].ToString().Split('|');
                resultado.Rows.RemoveAt(0);

                switch (res[0])
                {
                    case "0":
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "sweetAlert('Exito','" + res[1] + "', 'success');", true);
                        break;
                    case "2":
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "sweetAlert('Error','" + res[1] + "', 'error');", true);
                        break;
                    case "4":
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "sweetAlert('Error','" + res[1] + "', 'error');", true);
                        break;
                    case "5":
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "sweetAlert('Error','" + res[1] + "', 'error');", true);
                        break;
                }

                

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "sweetAlert('Error','Debe seleccionar un archivo de excel para cargar', 'error');", true);
            }
        }
    }
}