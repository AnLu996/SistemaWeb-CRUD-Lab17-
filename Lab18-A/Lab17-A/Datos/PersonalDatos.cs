using Lab17_A.Models;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using MySql.Data.MySqlClient;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using static Mysqlx.Crud.Order.Types;

namespace Lab17_A.Datos
{
    public class PersonalDatos
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

        public List<PersonalModel> Listar() 
        {
            var oLista =  new List<PersonalModel>();

            var cn = new Conexion();

            using (var conexion = cn.ObtenerConexion())
            {
                conexion.Open();
                MySqlCommand cmd = new MySqlCommand("listarPersonal", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oLista.Add(new PersonalModel()
                        {
                            IdPersonal = Convert.ToInt32(dr["id"]),
                            Nombre = dr["Nombre"].ToString(),
                            ApellidoPaterno = dr["ApellPater"].ToString(),
                            ApellidoMaterno = dr["ApellMater"].ToString(),
                            Direccion = dr["Direccion"].ToString(),
                            Edad = Convert.ToInt32(dr["Edad"]),
                            Genero = dr["Genero"].ToString(),
                            Nacionalidad = dr["Nacionalidad"].ToString(),
                            FechaContratacion = ((DateTime)dr["FechaContratacion"]).Date,
                            Telefono = Convert.ToInt32(dr["Telefono"]),
                            Sueldo = dr["Sueldo"].ToString()
                        }) ;
                    }
                }
            }
            return oLista;
        }

        public PersonalModel Obtener(int IdPersonal)
        {
            var oPersonal = new PersonalModel();

            var cn = new Conexion();

            using (var conexion = cn.ObtenerConexion())
            {
                conexion.Open();
                MySqlCommand cmd = new MySqlCommand("obtenerPersonal", conexion);
                cmd.Parameters.AddWithValue("n_idPersonal", IdPersonal);                
                cmd.CommandType = CommandType.StoredProcedure;


                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oPersonal.Nombre = dr["Nombre"].ToString();
                        oPersonal.ApellidoPaterno = dr["ApellPater"].ToString();
                        oPersonal.ApellidoMaterno = dr["ApellMater"].ToString();
                        oPersonal.Direccion = dr["Direccion"].ToString();
                        oPersonal.FechaNacimiento = ((DateTime)dr["FechaNacimiento"]).Date;
                        oPersonal.Genero = dr["Genero"].ToString();
                        oPersonal.Nacionalidad = dr["Nacionalidad"].ToString();
                        oPersonal.FechaContratacion = ((DateTime)dr["FechaContratacion"]).Date;
                        oPersonal.Telefono = Convert.ToInt32(dr["Telefono"]);
                        oPersonal.Sueldo = dr["Sueldo"].ToString();
                    }
                }
            }
            return oPersonal;
        }

        public bool Guardar(PersonalModel oPersonal) 
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = cn.ObtenerConexion())
                {
                    conexion.Open();
                    MySqlCommand cmd = new MySqlCommand("registrarPersonal", conexion);
                    cmd.Parameters.AddWithValue("n_nombre", oPersonal.Nombre);
                    cmd.Parameters.AddWithValue("n_apellPater", oPersonal.ApellidoPaterno);
                    cmd.Parameters.AddWithValue("n_apellMater", oPersonal.ApellidoMaterno);
                    cmd.Parameters.AddWithValue("n_telefono", oPersonal.Telefono);
                    cmd.Parameters.AddWithValue("n_direccion", oPersonal.Direccion);
                    cmd.Parameters.AddWithValue("n_fechaNacimiento", oPersonal.FechaNacimiento);
                    cmd.Parameters.AddWithValue("n_genero", oPersonal.Genero);
                    cmd.Parameters.AddWithValue("n_nacionalidad", oPersonal.Nacionalidad);
                    cmd.Parameters.AddWithValue("n_sueldo", oPersonal.Sueldo);
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

        public bool Editar(PersonalModel oPersonal)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = cn.ObtenerConexion())
                {
                    conexion.Open();
                    MySqlCommand cmd = new MySqlCommand("editarPersonal", conexion);
                    cmd.Parameters.AddWithValue("n_idPersonal", oPersonal.IdPersonal);
                    cmd.Parameters.AddWithValue("n_apellPater", oPersonal.ApellidoPaterno);
                    cmd.Parameters.AddWithValue("n_apellMater", oPersonal.ApellidoMaterno);
                    cmd.Parameters.AddWithValue("n_telefono", oPersonal.Telefono);
                    cmd.Parameters.AddWithValue("n_direccion", oPersonal.Direccion);
                    cmd.Parameters.AddWithValue("n_fechaNacimiento", oPersonal.FechaNacimiento);
                    cmd.Parameters.AddWithValue("n_genero", oPersonal.Genero);
                    cmd.Parameters.AddWithValue("n_nacionalidad", oPersonal.Nacionalidad);
                    cmd.Parameters.AddWithValue("n_sueldo", oPersonal.Sueldo);
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

        public bool Eliminar(int IdPersonal)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = cn.ObtenerConexion())
                {
                    conexion.Open();
                    MySqlCommand cmd = new MySqlCommand("eliminarPersonal", conexion);
                    cmd.Parameters.AddWithValue("n_idPersonal", IdPersonal);
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
