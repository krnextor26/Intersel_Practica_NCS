using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Web_INTERTEL.Modelo;

namespace Web_INTERTEL.Controlador
{
    public class UsuariosData
    {
        public UsuariosResponse ConsultarUnaPersona(UsuarioRequest Usuario)
        {
            string sQueryFill = "dbo.sp_loginUsuario";

            UsuariosResponse resultado = new UsuariosResponse();
            DataTable datos = new DataTable();

            try
            {
                using (SqlConnection cnn = new SqlConnection(Conexion.cadenaConexion))
                {
                    cnn.Open();

                    using (SqlCommand cmd = new SqlCommand(sQueryFill, cnn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandText = sQueryFill;
                        cmd.Parameters.Clear();

                        SqlParameter UsuarioParameter = new SqlParameter();
                        UsuarioParameter.SqlDbType = SqlDbType.VarChar;
                        UsuarioParameter.Size = 30;
                        UsuarioParameter.Direction = ParameterDirection.Input;
                        UsuarioParameter.ParameterName = "@Usuario";
                        UsuarioParameter.Value = Usuario.Usuario;

                        SqlParameter ContraParameter = new SqlParameter();
                        ContraParameter.SqlDbType = SqlDbType.VarChar;
                        ContraParameter.Size = 30;
                        ContraParameter.Direction = ParameterDirection.Input;
                        ContraParameter.ParameterName = "@Contrasenia";
                        ContraParameter.Value = Usuario.Contrasenia;

                        cmd.Parameters.Add(UsuarioParameter);
                        cmd.Parameters.Add(ContraParameter);

                        SqlDataAdapter SqlData = new SqlDataAdapter(cmd);
                        SqlData.Fill(datos);

                    }
                    cnn.Close();
                }

                if (datos.Rows.Count > 0)
                {
                    resultado.UsuarioID = Convert.ToInt32(datos.Rows[0]["UsuarioID"].ToString());
                    resultado.Usuario = datos.Rows[0]["Usuario"].ToString();
                    resultado.NombreCompleto = datos.Rows[0]["NombreCompleto"].ToString();
                }

            }
            catch (Exception ex)
            {
                resultado = new UsuariosResponse();
            }

            return resultado;
        }

        public List<UsuariosResponse> ListaUsuarios()
        {
            List<UsuariosResponse> Lista = new List<UsuariosResponse>();

            string sQueryFill = "dbo.sp_ListaUsuarios";
            DataTable datos = new DataTable();

            try
            {
                using (SqlConnection cnn = new SqlConnection(Conexion.cadenaConexion))
                {
                    cnn.Open();

                    using (SqlCommand cmd = new SqlCommand(sQueryFill, cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = sQueryFill;
                        cmd.Parameters.Clear();

                        SqlDataAdapter SqlData = new SqlDataAdapter(cmd);
                        SqlData.Fill(datos);
                    }

                    for (int i = 0; i < datos.Rows.Count; i++)
                    {
                        UsuariosResponse nodo = new UsuariosResponse();
                        nodo.UsuarioID = Convert.ToInt32(datos.Rows[i]["UsuarioID"].ToString());
                        nodo.Usuario = datos.Rows[i]["Usuario"].ToString();
                        nodo.NombreCompleto = datos.Rows[i]["NombreCompleto"].ToString();
                        nodo.Telefono = datos.Rows[i]["Telefono"].ToString();
                        nodo.TipoUsuario = datos.Rows[i]["TipoUsuario"].ToString();
                        nodo.Rol = datos.Rows[i]["Rol"].ToString();
                        nodo.NombreCompleto = datos.Rows[i]["NombreCompleto"].ToString();
                        nodo.Estatus = datos.Rows[i]["Estatus"].ToString();
                        nodo.FechaAlta = Convert.ToDateTime(datos.Rows[i]["FechaAlta"].ToString());

                        Lista.Add(nodo);
                    }

                    cnn.Close();
                }
            }
            catch (Exception ex) { ex.ToString(); }

            return Lista;
        }

        public List<mLista> TraerLista(string Tabla)
        {
            List<mLista> Lista = new List<mLista>();

            string sQueryFill = "dbo.sp_TraerLista";
            DataTable datos = new DataTable();

            try
            {
                using (SqlConnection cnn = new SqlConnection(Conexion.cadenaConexion))
                {
                    cnn.Open();

                    using (SqlCommand cmd = new SqlCommand(sQueryFill, cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = sQueryFill;
                        cmd.Parameters.Clear();

                        SqlParameter P_Tabla = new SqlParameter();
                        P_Tabla.Direction = ParameterDirection.Input;
                        P_Tabla.SqlDbType = SqlDbType.VarChar;
                        P_Tabla.Size = 100;
                        P_Tabla.ParameterName = "@tabla";
                        P_Tabla.Value = Tabla;

                        cmd.Parameters.Add(P_Tabla);

                        SqlDataAdapter SqlData = new SqlDataAdapter(cmd);
                        SqlData.Fill(datos);
                    }

                    for (int i = 0; i < datos.Rows.Count; i++)
                    {
                        mLista nodo = new mLista();
                        nodo.ID = Convert.ToInt32(datos.Rows[i][0].ToString());
                        nodo.DESCRIPCION = datos.Rows[i][1].ToString();

                        Lista.Add(nodo);
                    }

                    cnn.Close();
                }
            }
            catch (Exception ex) { ex.ToString(); }

            return Lista;
        }

        public UsuariosResponse ConsultarUsuario(UsuariosResponse usuario)
        {
            string sQueryFill = "dbo.sp_ConsultarUsuario";

            UsuariosResponse resultado = new UsuariosResponse();
            DataTable datos = new DataTable();

            try
            {
                using (SqlConnection cnn = new SqlConnection(Conexion.cadenaConexion))
                {
                    cnn.Open();

                    using (SqlCommand cmd = new SqlCommand(sQueryFill, cnn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandText = sQueryFill;
                        cmd.Parameters.Clear();

                        SqlParameter idPersonaParameter = new SqlParameter();
                        idPersonaParameter.SqlDbType = SqlDbType.Int;
                        idPersonaParameter.Direction = ParameterDirection.Input;
                        idPersonaParameter.ParameterName = "@UsuarioID";
                        idPersonaParameter.Value = usuario.UsuarioID;

                        cmd.Parameters.Add(idPersonaParameter);

                        SqlDataAdapter SqlData = new SqlDataAdapter(cmd);
                        SqlData.Fill(datos);

                    }
                    cnn.Close();
                }

                resultado.UsuarioID = Convert.ToInt32(datos.Rows[0]["UsuarioID"].ToString());
                resultado.Usuario = datos.Rows[0]["Usuario"].ToString();
                resultado.NombreCompleto = datos.Rows[0]["NombreCompleto"].ToString();
                resultado.Contrasenia = datos.Rows[0]["Contrasenia"].ToString();
                resultado.Telefono = datos.Rows[0]["Telefono"].ToString();
                resultado.IdRol = Convert.ToInt32(datos.Rows[0]["IdRol"].ToString());
                resultado.IdTipoUsuario = Convert.ToInt32(datos.Rows[0]["IdTipoUsuario"].ToString());

            }
            catch (Exception ex)
            {
                resultado = new UsuariosResponse();
            }

            return resultado;
        }

        public bool EliminarUsuario(UsuariosResponse usuario)
        {
            string sQueryFill = "dbo.sp_EliminarUsuario";

            bool resultado = true;

            try
            {
                using (SqlConnection cnn = new SqlConnection(Conexion.cadenaConexion))
                {
                    cnn.Open();

                    using (SqlCommand cmd = new SqlCommand(sQueryFill, cnn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandText = sQueryFill;
                        cmd.Parameters.Clear();

                        SqlParameter UsuarioID = new SqlParameter();
                        UsuarioID.SqlDbType = SqlDbType.Int;
                        UsuarioID.Direction = ParameterDirection.Input;
                        UsuarioID.ParameterName = "@UsuarioID";
                        UsuarioID.Value = usuario.UsuarioID;

                        cmd.Parameters.Add(UsuarioID);

                        cmd.ExecuteNonQuery();

                    }
                    cnn.Close();
                }

            }
            catch (Exception ex)
            {
                resultado = false;
            }

            return resultado;
        }

        public bool RegistrarUsuario(UsuariosResponse user)
        {
            string sQueryFill = "dbo.sp_InsertarUsuario";

            bool resultado = true;

            try
            {
                using (SqlConnection cnn = new SqlConnection(Conexion.cadenaConexion))
                {
                    cnn.Open();

                    using (SqlCommand cmd = new SqlCommand(sQueryFill, cnn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandText = sQueryFill;
                        cmd.Parameters.Clear();

                        SqlParameter Usuario = new SqlParameter();
                        Usuario.SqlDbType = SqlDbType.VarChar;
                        Usuario.Direction = ParameterDirection.Input;
                        Usuario.Size = 30;
                        Usuario.ParameterName = "@Usuario";
                        Usuario.Value = user.Usuario;

                        SqlParameter Contrasenia = new SqlParameter();
                        Contrasenia.SqlDbType = SqlDbType.VarChar;
                        Contrasenia.Direction = ParameterDirection.Input;
                        Contrasenia.Size = 30;
                        Contrasenia.ParameterName = "@Contrasenia";
                        Contrasenia.Value = user.Contrasenia;

                        SqlParameter NombreCompleto = new SqlParameter();
                        NombreCompleto.SqlDbType = SqlDbType.VarChar;
                        NombreCompleto.Direction = ParameterDirection.Input;
                        NombreCompleto.Size = 50;
                        NombreCompleto.ParameterName = "@NombreCompleto";
                        NombreCompleto.Value = user.NombreCompleto;

                        SqlParameter Telefono = new SqlParameter();
                        Telefono.SqlDbType = SqlDbType.VarChar;
                        Telefono.Direction = ParameterDirection.Input;
                        Telefono.Size = 13;
                        Telefono.ParameterName = "@Telefono";
                        Telefono.Value = user.Telefono;

                        SqlParameter IdTipoUsuario = new SqlParameter();
                        IdTipoUsuario.SqlDbType = SqlDbType.Int;
                        IdTipoUsuario.Direction = ParameterDirection.Input;
                        IdTipoUsuario.ParameterName = "@IdTipoUsuario";
                        IdTipoUsuario.Value = user.IdTipoUsuario;

                        SqlParameter IdRol = new SqlParameter();
                        IdRol.SqlDbType = SqlDbType.Int;
                        IdRol.Direction = ParameterDirection.Input;
                        IdRol.ParameterName = "@IdRol";
                        IdRol.Value = user.IdRol;

                        cmd.Parameters.Add(Usuario);
                        cmd.Parameters.Add(Contrasenia);
                        cmd.Parameters.Add(NombreCompleto);
                        cmd.Parameters.Add(IdTipoUsuario);
                        cmd.Parameters.Add(IdRol);
                        cmd.Parameters.Add(Telefono);

                        cmd.ExecuteNonQuery();

                    }
                    cnn.Close();
                }

            }
            catch (Exception ex)
            {
                resultado = false;
            }

            return resultado;
        }

        public bool ActualizarUsuario(UsuariosResponse user)
        {
            string sQueryFill = "dbo.sp_ActualizarUsuario";

            bool resultado = true;

            try
            {
                using (SqlConnection cnn = new SqlConnection(Conexion.cadenaConexion))
                {
                    cnn.Open();

                    using (SqlCommand cmd = new SqlCommand(sQueryFill, cnn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandText = sQueryFill;
                        cmd.Parameters.Clear();

                        SqlParameter UsuarioID = new SqlParameter();
                        UsuarioID.SqlDbType = SqlDbType.Int;
                        UsuarioID.Direction = ParameterDirection.Input;
                        UsuarioID.ParameterName = "@UsuarioID";
                        UsuarioID.Value = user.UsuarioID;

                        SqlParameter Usuario = new SqlParameter();
                        Usuario.SqlDbType = SqlDbType.VarChar;
                        Usuario.Direction = ParameterDirection.Input;
                        Usuario.Size = 30;
                        Usuario.ParameterName = "@Usuario";
                        Usuario.Value = user.Usuario;

                        SqlParameter Contrasenia = new SqlParameter();
                        Contrasenia.SqlDbType = SqlDbType.VarChar;
                        Contrasenia.Direction = ParameterDirection.Input;
                        Contrasenia.Size = 30;
                        Contrasenia.ParameterName = "@Contrasenia";
                        Contrasenia.Value = user.Contrasenia;

                        SqlParameter NombreCompleto = new SqlParameter();
                        NombreCompleto.SqlDbType = SqlDbType.VarChar;
                        NombreCompleto.Direction = ParameterDirection.Input;
                        NombreCompleto.Size = 50;
                        NombreCompleto.ParameterName = "@NombreCompleto";
                        NombreCompleto.Value = user.NombreCompleto;

                        SqlParameter Telefono = new SqlParameter();
                        Telefono.SqlDbType = SqlDbType.VarChar;
                        Telefono.Direction = ParameterDirection.Input;
                        Telefono.Size = 13;
                        Telefono.ParameterName = "@Telefono";
                        Telefono.Value = user.Telefono;

                        SqlParameter IdTipoUsuario = new SqlParameter();
                        IdTipoUsuario.SqlDbType = SqlDbType.Int;
                        IdTipoUsuario.Direction = ParameterDirection.Input;
                        IdTipoUsuario.ParameterName = "@IdTipoUsuario";
                        IdTipoUsuario.Value = user.IdTipoUsuario;

                        SqlParameter IdRol = new SqlParameter();
                        IdRol.SqlDbType = SqlDbType.Int;
                        IdRol.Direction = ParameterDirection.Input;
                        IdRol.ParameterName = "@IdRol";
                        IdRol.Value = user.IdRol;

                        cmd.Parameters.Add(UsuarioID);
                        cmd.Parameters.Add(Usuario);
                        cmd.Parameters.Add(Contrasenia);
                        cmd.Parameters.Add(NombreCompleto);
                        cmd.Parameters.Add(IdTipoUsuario);
                        cmd.Parameters.Add(IdRol);
                        cmd.Parameters.Add(Telefono);

                        cmd.ExecuteNonQuery();

                    }
                    cnn.Close();
                }

            }
            catch (Exception ex)
            {
                resultado = false;
            }

            return resultado;
        }
    }
}