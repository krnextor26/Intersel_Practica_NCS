using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Web_INTERTEL.Modelo;

namespace Web_INTERTEL.Controlador
{
    public class cCarga
    {
        //cLog cLog = new cLog();
        string strErr;
        DataTable tablaResultados = new DataTable();
        string mensaje = "";
        string nomArchivoprocesa;
        string nomUsrProcesa;
        int idUsrProcesa;

        public DataTable procesaArchivo(string rutaArchivo, int IDUsuario, string nombreArchivoProcesado, string nombreUsuarioProcesa)
        {
            DataTable resultado = new DataTable();
            resultado.Columns.Add("cerrar", typeof(string));
            DataTable dtExcelData = new DataTable();
            bool correcto = true;
            mLineasCelulares a = new mLineasCelulares();

            string ruta = rutaArchivo;

            nomArchivoprocesa = nombreArchivoProcesado;
            nomUsrProcesa = nombreUsuarioProcesa;
            idUsrProcesa = IDUsuario;

            DataTable csvData = ConvertCSVtoDataTable(rutaArchivo);

            //Validacion de formato de archivo
            for (int i = 0; i < csvData.Columns.Count; i++)
            {
                string nombreColumna = csvData.Columns[i].ColumnName;

                switch (i)
                {
                    case 0:
                        if (nombreColumna.ToUpper().Trim() != "MOBILELINEID")
                        {
                            resultado.Rows.Add("2|¡Revisar que el formato de columnas del archivo del archivo sea correcto: !");
                            return resultado;
                        }
                        break;
                    case 1:
                        if (nombreColumna.ToUpper().Trim() != "MOBILELINE")
                        {
                            resultado.Rows.Add("2|¡Revisar que el formato de columnas del archivo del archivo sea correcto: !");
                            return resultado;
                        }
                        break;
                    case 2:
                        if (nombreColumna.ToUpper().Trim() != "DESCRIPTION")
                        {
                            resultado.Rows.Add("2|¡Revisar que el formato de columnas del archivo del archivo sea correcto: !");
                            return resultado;
                        }
                        break;
                }
            }

            dtExcelData.Columns.Add("MobileLine", typeof(string));
            dtExcelData.Columns.Add("Description", typeof(string));
            dtExcelData.Columns.Add("UsuarioAltaId", typeof(int));

            for (int i = 1; i <= csvData.Rows.Count - 1; i++)
            {
                if (!string.IsNullOrEmpty(csvData.Rows[i][1].ToString().Trim()) || !string.IsNullOrEmpty(csvData.Rows[i][2].ToString().Trim()))
                {
                    try
                    {
                        a = new mLineasCelulares();

                        a.MobileLine = csvData.Rows[i][1].ToString();
                        a.Description = csvData.Rows[i][2].ToString();
                        a.UsuarioAltaId = IDUsuario;

                        dtExcelData.Rows.Add(a.MobileLine, a.Description, a.UsuarioAltaId);
                    }

                    catch (Exception exc)
                    {
                        registraLog(exc, "procesaArchivo");
                    }
                }
            }

            if (correcto)
            {
                using (SqlConnection con = new SqlConnection(Conexion.cadenaConexion))
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        try
                        {
                            con.Open();

                            sqlBulkCopy.DestinationTableName = "dbo.LineasCelulares";
                            sqlBulkCopy.ColumnMappings.Add("MobileLine", "MobileLine");
                            sqlBulkCopy.ColumnMappings.Add("Description", "Description");
                            sqlBulkCopy.ColumnMappings.Add("UsuarioAltaId", "UsuarioAltaId");
                            sqlBulkCopy.BulkCopyTimeout = 0;
                            sqlBulkCopy.WriteToServer(dtExcelData);
                            con.Close();

                        }
                        catch (Exception ex)
                        {
                            ex.Message.ToString();
                            registraLog(ex, "procesaArchivo");
                            resultado.Rows.Add("5|Error de conexion a la base de datos.");
                        }

                    }

                }
                if (mensaje.Length > 0)
                {
                    DataRow renglon;
                    renglon = tablaResultados.NewRow();
                    renglon["cerrar"] = "4|Error de formato.|" + dtExcelData.Rows.Count + "|" + tablaResultados.Rows.Count;

                    registraLogActividad("Archivo Procesado. Registros correctos: " + dtExcelData.Rows.Count + ", Registros erroneos: " + tablaResultados.Rows.Count, nombreArchivoProcesado, IDUsuario, nombreUsuarioProcesa);

                    tablaResultados.Rows.InsertAt(renglon, 0);

                    return tablaResultados;
                }
                else
                {
                    registraLogActividad("Archivo Procesado Correctamente. Se registraron " + dtExcelData.Rows.Count + " Registro(s)", nombreArchivoProcesado, IDUsuario, nombreUsuarioProcesa);
                    resultado.Rows.Add("0|Carga Exitosa.");
                }

            }

            return resultado;
        }


        public DataTable procesaArchivoDetalle(string rutaArchivo, int IDUsuario, string nombreArchivoProcesado, string nombreUsuarioProcesa)
        {
            DataTable resultado = new DataTable();
            resultado.Columns.Add("cerrar", typeof(string));
            DataTable dtExcelData = new DataTable();
            bool correcto = true;
            mDetallesLlamadas a = new mDetallesLlamadas();

            string ruta = rutaArchivo;

            nomArchivoprocesa = nombreArchivoProcesado;
            nomUsrProcesa = nombreUsuarioProcesa;
            idUsrProcesa = IDUsuario;

            DataTable csvData = ConvertCSVtoDataTable(rutaArchivo);

            //Validacion de formato de archivo
            for (int i = 0; i < csvData.Columns.Count; i++)
            {
                string nombreColumna = csvData.Columns[i].ColumnName;

                switch (i)
                {
                    case 0:
                        if (nombreColumna.ToUpper().Trim() != "CALLDETAILID")
                        {
                            resultado.Rows.Add("2|¡Revisar que el formato de columnas del archivo del archivo sea correcto: !");
                            return resultado;
                        }
                        break;
                    case 1:
                        if (nombreColumna.ToUpper().Trim() != "MOBILELINE")
                        {
                            resultado.Rows.Add("2|¡Revisar que el formato de columnas del archivo del archivo sea correcto: !");
                            return resultado;
                        }
                        break;
                    case 2:
                        if (nombreColumna.ToUpper().Trim() != "CALLEDPARTYNUMBER")
                        {
                            resultado.Rows.Add("2|¡Revisar que el formato de columnas del archivo del archivo sea correcto: !");
                            return resultado;
                        }
                        break;
                    case 3:
                        if (nombreColumna.ToUpper().Trim() != "CALLEDPARTYDESCRIPTION")
                        {
                            resultado.Rows.Add("2|¡Revisar que el formato de columnas del archivo del archivo sea correcto: !");
                            return resultado;
                        }
                        break;
                    case 4:
                        if (nombreColumna.ToUpper().Trim() != "DATETIME")
                        {
                            resultado.Rows.Add("2|¡Revisar que el formato de columnas del archivo del archivo sea correcto: !");
                            return resultado;
                        }
                        break;
                    case 5:
                        if (nombreColumna.ToUpper().Trim() != "DURATION")
                        {
                            resultado.Rows.Add("2|¡Revisar que el formato de columnas del archivo del archivo sea correcto: !");
                            return resultado;
                        }
                        break;
                    case 6:
                        if (nombreColumna.ToUpper().Trim() != "TOTALCOST")
                        {
                            resultado.Rows.Add("2|¡Revisar que el formato de columnas del archivo del archivo sea correcto: !");
                            return resultado;
                        }
                        break;
                }
            }

            dtExcelData.Columns.Add("MobileLine", typeof(string));
            dtExcelData.Columns.Add("CalledPartyNumber", typeof(string));
            dtExcelData.Columns.Add("CalledPartyDescription", typeof(string));
            dtExcelData.Columns.Add("FechaHora", typeof(DateTime));
            dtExcelData.Columns.Add("Duration", typeof(int));
            dtExcelData.Columns.Add("TotalCost", typeof(decimal));
            dtExcelData.Columns.Add("UsuarioAltaId", typeof(int));

            for (int i = 1; i <= csvData.Rows.Count - 1; i++)
            {
                if (!string.IsNullOrEmpty(csvData.Rows[i][1].ToString().Trim()) || !string.IsNullOrEmpty(csvData.Rows[i][2].ToString().Trim()))
                {
                    try
                    {
                        a = new mDetallesLlamadas();

                        a.MobileLine = csvData.Rows[i][1].ToString();
                        a.CalledPartyNumber = csvData.Rows[i][2].ToString();
                        a.CalledPartyDescription = csvData.Rows[i][3].ToString();
                        a.FechaHora = Convert.ToDateTime(csvData.Rows[i][4].ToString());
                        a.Duration = Convert.ToInt32(csvData.Rows[i][5].ToString());
                        a.TotalCost = Convert.ToDecimal(csvData.Rows[i][6].ToString());
                        a.UsuarioAltaId = IDUsuario;

                        dtExcelData.Rows.Add(a.MobileLine, a.CalledPartyNumber, a.CalledPartyDescription, a.FechaHora, a.Duration, a.TotalCost, a.UsuarioAltaId);
                    }

                    catch (Exception exc)
                    {
                        registraLog(exc, "procesaArchivo");
                    }
                }
            }

            if (correcto)
            {
                using (SqlConnection con = new SqlConnection(Conexion.cadenaConexion))
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        try
                        {
                            con.Open();

                            sqlBulkCopy.DestinationTableName = "dbo.DetallesLlamadas";
                            sqlBulkCopy.ColumnMappings.Add("MobileLine", "MobileLine");
                            sqlBulkCopy.ColumnMappings.Add("CalledPartyNumber", "CalledPartyNumber");
                            sqlBulkCopy.ColumnMappings.Add("CalledPartyDescription", "CalledPartyDescription");
                            sqlBulkCopy.ColumnMappings.Add("FechaHora", "FechaHora");
                            sqlBulkCopy.ColumnMappings.Add("Duration", "Duration");
                            sqlBulkCopy.ColumnMappings.Add("TotalCost", "TotalCost");
                            sqlBulkCopy.ColumnMappings.Add("UsuarioAltaId", "UsuarioAltaId");
                            sqlBulkCopy.BulkCopyTimeout = 0;
                            sqlBulkCopy.WriteToServer(dtExcelData);
                            con.Close();

                        }
                        catch (Exception ex)
                        {
                            ex.Message.ToString();
                            registraLog(ex, "procesaArchivo");
                            resultado.Rows.Add("5|Error de conexion a la base de datos.");
                        }

                    }

                }
                if (mensaje.Length > 0)
                {
                    DataRow renglon;
                    renglon = tablaResultados.NewRow();
                    renglon["cerrar"] = "4|Error de formato.|" + dtExcelData.Rows.Count + "|" + tablaResultados.Rows.Count;

                    registraLogActividad("Archivo Procesado. Registros correctos: " + dtExcelData.Rows.Count + ", Registros erroneos: " + tablaResultados.Rows.Count, nombreArchivoProcesado, IDUsuario, nombreUsuarioProcesa);

                    tablaResultados.Rows.InsertAt(renglon, 0);

                    return tablaResultados;
                }
                else
                {
                    registraLogActividad("Archivo Procesado Correctamente. Se registraron " + dtExcelData.Rows.Count + " Registro(s)", nombreArchivoProcesado, IDUsuario, nombreUsuarioProcesa);
                    resultado.Rows.Add("0|Carga Exitosa.");
                }

            }

            return resultado;
        }

        protected void registraLog(Exception e, string metodo)
        {
            FileInfo file;
            String mensaje;
            try
            {
                if (!Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["RutaLog"]))
                {
                    Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["RutaLog"]);
                }

                string archivo = ConfigurationManager.AppSettings["LogError"] + "_" + DateTime.Now.ToString("dd-MM-yyyy") + ".txt";

                if (!File.Exists(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["RutaLog"] + archivo)))
                    File.Create(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["RutaLog"] + archivo)).Close();

                file = new FileInfo(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["RutaLog"], archivo));

                mensaje = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " Metodo: " + metodo + ", Message: " + e.Message + "\\t Source: " + e.Source + "\\t StackTrace: " + e.StackTrace + "\\t TagetSite: " + e.TargetSite;
                StreamWriter Log = new StreamWriter(System.AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["RutaLog"] + archivo, true);
                Log.WriteLine(mensaje);
                Log.Flush();
                Log.Close();
            }
            catch (Exception exc)
            {
                string error = exc.Message;
            }
        }

        protected void registraLogActividad(string Mens, string nomArchivo, int id_Usr, string UserName)
        {
            FileInfo file;
            string mensaje, mensaje2, mensajeFinal;
            mensaje = "Fecha: {0}, Nombre Archivo: {1}, Mensaje: {2}, ";
            mensaje2 = "Id Usuario: {0}, Usuario: {1}";
            mensajeFinal = "{0}{1}";

            try
            {
                if (!Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["RutaActividad"]))
                {
                    Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["RutaActividad"]);
                }

                string archivo = ConfigurationManager.AppSettings["LogActividad"] + "_" + DateTime.Now.ToString("dd-MM-yyyy") + ".txt";

                if (!File.Exists(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["RutaActividad"] + archivo)))
                    File.Create(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["RutaActividad"] + archivo)).Close();

                file = new FileInfo(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["RutaActividad"], archivo));

                mensajeFinal = string.Format(mensajeFinal, (string.Format(mensaje, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), nomArchivo, Mens)), (string.Format(mensaje2, id_Usr, UserName)));

                StreamWriter Log = new StreamWriter(System.AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["RutaActividad"] + archivo, true);
                Log.WriteLine(mensajeFinal);
                Log.Flush();
                Log.Close();

            }
            catch (Exception exc)
            {
                string error = exc.Message;
                registraLog(exc, "registraLogActividad");
            }
        }

        public static DataTable ConvertCSVtoDataTable(string strFilePath)
        {
            DataTable dt = new DataTable();
            using (StreamReader sr = new StreamReader(strFilePath))
            {
                string[] headers = sr.ReadLine().Split(',');
                foreach (string header in headers)
                {
                    dt.Columns.Add(header);
                }
                while (!sr.EndOfStream)
                {
                    string[] rows = sr.ReadLine().Split(',');
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < headers.Length; i++)
                    {
                        dr[i] = rows[i];
                    }
                    dt.Rows.Add(dr);
                }

            }

            return dt;
        }
    }
}