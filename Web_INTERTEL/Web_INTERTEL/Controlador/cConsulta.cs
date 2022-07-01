using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Web_INTERTEL.Modelo;

namespace Web_INTERTEL.Controlador
{
    public class cConsulta
    {
        public List<mLineasCelulares> ListaLineasCelulares()
        {
            List<mLineasCelulares> Lista = new List<mLineasCelulares>();

            string sQueryFill = "dbo.sp_ListaLineasCelulares";
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
                        mLineasCelulares nodo = new mLineasCelulares();
                        nodo.MobileLineId = Convert.ToInt32(datos.Rows[i]["MobileLineId"].ToString());
                        nodo.MobileLine = datos.Rows[i]["MobileLine"].ToString();
                        nodo.Description = datos.Rows[i]["Description"].ToString();
                        nodo.Estatus = datos.Rows[i]["Estatus"].ToString();
                        nodo.Usuario = datos.Rows[i]["Usuario"].ToString();
                        nodo.FechaAlta = Convert.ToDateTime(datos.Rows[i]["FechaAlta"].ToString());

                        Lista.Add(nodo);
                    }

                    cnn.Close();
                }
            }
            catch (Exception ex) { ex.ToString(); }

            return Lista;
        }

        public List<mDetallesLlamadas> DetalleLineasCelulares(string MobileLine)
        {
            List<mDetallesLlamadas> Lista = new List<mDetallesLlamadas>();

            string sQueryFill = "dbo.sp_DetalleLineasCelulares";
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

                        SqlParameter P_MobileLine = new SqlParameter();
                        P_MobileLine.Direction = ParameterDirection.Input;
                        P_MobileLine.SqlDbType = SqlDbType.VarChar;
                        P_MobileLine.Size = 12;
                        P_MobileLine.ParameterName = "@MobileLine";
                        P_MobileLine.Value = MobileLine;

                        cmd.Parameters.Add(P_MobileLine);

                        SqlDataAdapter SqlData = new SqlDataAdapter(cmd);
                        SqlData.Fill(datos);
                    }

                    for (int i = 0; i < datos.Rows.Count; i++)
                    {
                        mDetallesLlamadas nodo = new mDetallesLlamadas();
                        nodo.MobileLineId = Convert.ToInt32(datos.Rows[i]["MobileLineId"].ToString());
                        nodo.MobileLine = datos.Rows[i]["MobileLine"].ToString();
                        nodo.Description = datos.Rows[i]["Description"].ToString();
                        nodo.CalledPartyNumber = datos.Rows[i]["CalledPartyNumber"].ToString();
                        nodo.CalledPartyDescription = datos.Rows[i]["CalledPartyDescription"].ToString();
                        nodo.Duration = Convert.ToInt32(datos.Rows[i]["Duration"].ToString());
                        nodo.TotalCost = Convert.ToDecimal(datos.Rows[i]["TotalCost"].ToString());
                        nodo.Estatus = datos.Rows[i]["Estatus"].ToString();
                        nodo.NombreCompleto = datos.Rows[i]["NombreCompleto"].ToString();
                        nodo.FechaAlta = Convert.ToDateTime(datos.Rows[i]["FechaHora"].ToString());

                        Lista.Add(nodo);
                    }

                    cnn.Close();
                }
            }
            catch (Exception ex) { ex.ToString(); }

            return Lista;
        }
    }
}