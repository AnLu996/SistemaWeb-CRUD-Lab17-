using Lab17_A.Models;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using MySql.Data.MySqlClient;
using System.Data;
using System.Diagnostics.Eventing.Reader;

namespace Lab17_A.Datos
{
    public class EnfermedadDatos
    {

        public bool Registrar(string usuario, string contraseña)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = cn.ObtenerConexion())
                {
                    conexion.Open();
                    MySqlCommand cmd = new MySqlCommand("RegistrarUser", conexion);
                    cmd.Parameters.AddWithValue("n_perfil", usuario);
                    cmd.Parameters.AddWithValue("n_contraseña", contraseña);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();

                    cmd.ExecuteReader();

                    rpta = Convert.ToBoolean(cmd.Parameters["mensaje"].Value);
                }
            }

            catch (Exception e)
            {
                string error = e.Message;
                rpta = false;
            }

            return rpta;
        }

        public int Login(string usuario, string contraseña)
        {
            int rpta;

            try
            {
                var cn = new Conexion();

            using (var conexion = cn.ObtenerConexion())
                {
                    conexion.Open();
                    MySqlCommand cmd = new MySqlCommand("LoginUsuario", conexion);
                    cmd.Parameters.AddWithValue("n_perfil", usuario);
                    cmd.Parameters.AddWithValue("n_contraseña", contraseña);
                    cmd.Parameters.Add(new MySqlParameter("n_mensaje", MySqlDbType.Int32));
                    cmd.Parameters["n_mensaje"].Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();

                    cmd.ExecuteReader();

                    rpta = Convert.ToInt32(cmd.Parameters["n_mensaje"].Value);
                }
            }

            catch (Exception e)
            {
                string error = e.Message;
                rpta = 0;
            }

            return rpta;
        }

        public List<EnfermedadModel> Listar() 
        {
            var oLista =  new List<EnfermedadModel>();

            var cn = new Conexion();

            using (var conexion = cn.ObtenerConexion())
            {
                conexion.Open();
                MySqlCommand cmd = new MySqlCommand("listarEnfermedades", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oLista.Add(new EnfermedadModel()
                        {
                            IdEnfermedad = Convert.ToInt32(dr["IdEnfermedad"]),
                            Nombre = dr["Nombre"].ToString(),
                            TasaMortalidad = dr["TasaMortalidad"].ToString(),
                            Sintoma= dr["Sintoma"].ToString(),
                            Medicamento = dr["Medicamento"].ToString()

                        }) ;
                    }
                }
            }
            return oLista;
        }

        public EnfermedadModel Obtener(int IdEnfermedad)
        {
            var oEnfermedad = new EnfermedadModel();

            var cn = new Conexion();

            using (var conexion = cn.ObtenerConexion())
            {
                conexion.Open();
                MySqlCommand cmd = new MySqlCommand("obtenerEnfermedad", conexion);
                cmd.Parameters.AddWithValue("n_idEnfermedad", IdEnfermedad);                
                cmd.CommandType = CommandType.StoredProcedure;


                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oEnfermedad.Nombre = dr["Nombre"].ToString();
                        oEnfermedad.TasaMortalidad = dr["TasaMortalidad"].ToString();
                        oEnfermedad.Sintoma = dr["Sintoma"].ToString();
                        oEnfermedad.Medicamento = dr["Medicamento"].ToString();
                    }
                }
            }
            return oEnfermedad;
        }

        public bool Guardar(EnfermedadModel oEnfermedad) 
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = cn.ObtenerConexion())
                {
                    conexion.Open();
                    MySqlCommand cmd = new MySqlCommand("registrarEnfermedad", conexion);
                    cmd.Parameters.AddWithValue("n_nombre", oEnfermedad.Nombre);
                    cmd.Parameters.AddWithValue("n_tasaMortalidad", oEnfermedad.TasaMortalidad);
                    cmd.Parameters.AddWithValue("n_medicamento", oEnfermedad.Medicamento);
                    cmd.Parameters.AddWithValue("n_sintoma", oEnfermedad.Sintoma);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                rpta = true;
            }

            catch (Exception e) 
            {
                string error = e.Message;
                rpta = false;
            }

            return rpta;
        }

        public bool Editar(EnfermedadModel oEnfermedad)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = cn.ObtenerConexion())
                {
                    conexion.Open();
                    MySqlCommand cmd = new MySqlCommand("editarEnfermedad", conexion);
                    cmd.Parameters.AddWithValue("n_idEnfermedad", oEnfermedad.IdEnfermedad);
                    cmd.Parameters.AddWithValue("n_nombre", oEnfermedad.Nombre);
                    cmd.Parameters.AddWithValue("n_tasaMortalidad", oEnfermedad.TasaMortalidad);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                rpta = true;
            }

            catch (Exception e)
            {
                string error = e.Message;
                rpta = false;
            }

            return rpta;
        }

        public bool Eliminar(int IdEnfermedad)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = cn.ObtenerConexion())
                {
                    conexion.Open();
                    MySqlCommand cmd = new MySqlCommand("eliminarEnfermedad", conexion);
                    cmd.Parameters.AddWithValue("n_idEnfermedad", IdEnfermedad);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                rpta = true;
            }

            catch (Exception e)
            {
                string error = e.Message;
                rpta = false;
            }

            return rpta;
        }
    }
}
