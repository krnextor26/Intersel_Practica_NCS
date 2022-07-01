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
    public partial class vUsuarios : System.Web.UI.Page
    {
        UsuariosData clase = new UsuariosData();
        UsuariosResponse usuario = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session.IsNewSession)
                {
                    Response.Redirect("~/login.aspx");
                }

                cargarListaUsuarios();
            }
        }

        protected void gvUsuario_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (gvUsuario.Rows.Count > 0)
            {
                gvUsuario.PageIndex = e.NewPageIndex;
                cargarListaUsuarios();
            }
        }

        protected void gvUsuario_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Actualizar")
            {
                int UsuarioID = Convert.ToInt32(e.CommandArgument.ToString());

                usuario = new UsuariosResponse();

                usuario.UsuarioID = UsuarioID;
                usuario = clase.ConsultarUsuario(usuario);

                cargarListas();

                HfIdUsuario.Value = usuario.UsuarioID.ToString();
                txtUsuario.Text = usuario.Usuario;
                txtNombre.Text = usuario.NombreCompleto;
                txtContra.Text = "";
                txtConfirma.Text = "";
                txtTelefono.Text = usuario.Telefono;
                ddlRol.SelectedValue = usuario.IdRol.ToString();
                ddlTipoUsuario.SelectedValue = usuario.IdTipoUsuario.ToString();
                
                dvUsuarios.Visible = false;
                dvInsertar.Visible = true;

            }
            else if (e.CommandName == "Eliminar")
            {
                int UsuarioID = Convert.ToInt32(e.CommandArgument.ToString());

                bool resultado = false;
                usuario = new UsuariosResponse();
                usuario.UsuarioID = UsuarioID;

                resultado = clase.EliminarUsuario(usuario);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "sweetAlert('Exito','Registro Eliminado', 'success');", true);

                cargarListaUsuarios();

            }
        }

        protected void btnInsertar_Click(object sender, EventArgs e)
        {
            dvInsertar.Visible = true;
            dvUsuarios.Visible = false;
            cargarListas();
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            cargarListaUsuarios();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            usuario = new UsuariosResponse();

            usuario.UsuarioID = Convert.ToInt32(HfIdUsuario.Value);
            usuario.Usuario = txtUsuario.Text;
            usuario.Contrasenia = txtContra.Text;
            usuario.NombreCompleto = txtNombre.Text;
            usuario.Telefono = txtTelefono.Text;
            usuario.IdRol = Convert.ToInt32(ddlRol.SelectedValue);
            usuario.IdTipoUsuario = Convert.ToInt32(ddlTipoUsuario.SelectedValue);
            bool resultado = false;


            if (HfIdUsuario.Value == "0")
            {
                resultado = clase.RegistrarUsuario(usuario);
                HfIdUsuario.Value = "0";
            }
            else
            {
                resultado = clase.ActualizarUsuario(usuario);
                HfIdUsuario.Value = "0";
            }

            limpiarCampos();
            cargarListaUsuarios();

            if (resultado)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "sweetAlert('Exito','Registro Guardado Correctamente', 'success');", true);
            }
            
        }

        private void cargarListaUsuarios()
        {
            List<UsuariosResponse> lista = null;
            lista = clase.ListaUsuarios();
            gvUsuario.DataSource = lista;
            gvUsuario.DataBind();
            dvUsuarios.Visible = true;
            dvInsertar.Visible = false;
        }

        private void cargarListas()
        {
            List<mLista> listaRoles = new List<mLista>();
            listaRoles = clase.TraerLista("Rol");
            ddlRol.DataSource = listaRoles;
            ddlRol.DataTextField = "DESCRIPCION";
            ddlRol.DataValueField = "ID";
            ddlRol.SelectedValue = "0";
            ddlRol.DataBind();

            List<mLista> listaTipoUsuario = new List<mLista>();
            listaTipoUsuario = clase.TraerLista("TipoUsuario");
            ddlTipoUsuario.DataSource = listaTipoUsuario;
            ddlTipoUsuario.DataTextField = "DESCRIPCION";
            ddlTipoUsuario.DataValueField = "ID";
            ddlTipoUsuario.SelectedValue = "0";
            ddlTipoUsuario.DataBind();
        }

        private void limpiarCampos()
        {
            txtUsuario.Text = "";
            txtContra.Text = "";
            txtNombre.Text = "";
            txtTelefono.Text = "";
            ddlRol.SelectedValue = "0";
            ddlTipoUsuario.SelectedValue= "0";
        }
    }
}