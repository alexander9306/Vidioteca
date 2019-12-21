using Biblioteca.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Biblioteca.Web.Datos
{
    public class Core_Pelicula
    {
        SqlConnection conexion = new SqlConnection(db.GetConfiguration());
        SqlCommand cmd;

        //Crear una pelicula
        public int CrearPelicula(Pelicula pelicula)
        {
            cmd = new SqlCommand("insert into pelicula(titulo, genero, fechaestreno, idfoto) values(@titulo,@genero,@fechaestreno); SELECT SCOPE_IDENTITY()", conexion);
            conexion.Open();
            cmd.Parameters.AddWithValue("@titulo", pelicula.titulo);
            cmd.Parameters.AddWithValue("@genero", pelicula.genero);
            cmd.Parameters.AddWithValue("@fechaestreno", pelicula.fechaestreno);

            var nuevoid = Convert.ToInt32(cmd.ExecuteScalar());
            conexion.Close();

            return nuevoid;
        }

        //Actualizar una pelicula
        public void ActualizarPelicula(Pelicula pelicula)
        {
            cmd = new SqlCommand("update pelicula set titulo=@titulo, genero=@genero, fechaestreno=@fechaestreno where idpelicula=@idpelicula", conexion);
            conexion.Open();
            cmd.Parameters.AddWithValue("@titulo", pelicula.titulo);
            cmd.Parameters.AddWithValue("@genero", pelicula.genero);
            cmd.Parameters.AddWithValue("@fechaestreno", pelicula.fechaestreno);

            cmd.ExecuteNonQuery();
            conexion.Close();
        }

        //Cargar pelicula
        public Pelicula CargarPelicula(int idpelicula)
        {
            conexion.Open();
            cmd = new SqlCommand("SELECT * FROM pelicula where idpelicula=@idpelicula", conexion);
            cmd.Parameters.AddWithValue("@idpelicula", idpelicula);
            SqlDataReader rdr = cmd.ExecuteReader();
            rdr.Read();

            Pelicula pelicula = new Pelicula();
            pelicula.idpelicula = Convert.ToInt32(rdr["idpelicula"]);
            pelicula.titulo = rdr["titulo"].ToString();
            pelicula.fechaestreno = Convert.ToDateTime(rdr["fechaestreno"]);

            conexion.Close();

            return pelicula;
        }

        //Cargar pelicula por genero
        public IEnumerable<Pelicula> CargarPeliculaGenero(string genero)
        {
            conexion.Open();
            cmd = new SqlCommand("SELECT * FROM pelicula where genero=@genero", conexion);
            cmd.Parameters.AddWithValue("@genero", genero);
            SqlDataReader rdr = cmd.ExecuteReader();

            List<Pelicula> lstpelicula = new List<Pelicula>();

            while (rdr.Read())
            {
                if (rdr.HasRows)
                {
                    Pelicula pelicula = new Pelicula();
                    pelicula.idpelicula = Convert.ToInt32(rdr["idpelicula"]);
                    pelicula.titulo = rdr["titulo"].ToString();
                    pelicula.fechaestreno = Convert.ToDateTime(rdr["fechaestreno"]);

                    lstpelicula.Add(pelicula);
                }
            }
            conexion.Close();
            return lstpelicula;
        }

        //Cargar todas las peliculas
        public IEnumerable<Pelicula> CargarPeliculas()
        {
            conexion.Open();
            cmd = new SqlCommand("SELECT * FROM peliculas", conexion);
            SqlDataReader rdr = cmd.ExecuteReader();

            List<Pelicula> lstpelicula = new List<Pelicula>();

            while (rdr.Read())
            {
                if (rdr.HasRows)
                {
                    Pelicula pelicula = new Pelicula();
                    pelicula.idpelicula = Convert.ToInt32(rdr["idpelicula"]);
                    pelicula.titulo = rdr["titulo"].ToString();
                    pelicula.fechaestreno = Convert.ToDateTime(rdr["fechaestreno"]);

                    lstpelicula.Add(pelicula);
                }
            }
            conexion.Close();
            return lstpelicula;
        }

        //Borrar una pelicula
        public int BorrarPelicula(int idpelicula)
        {
            cmd = new SqlCommand("delete pelicula where idpelicula=@idpelicula", conexion);
            conexion.Open();
            cmd.Parameters.AddWithValue("@idpelicula", idpelicula);
            int f_afectada = cmd.ExecuteNonQuery();
            conexion.Close();
            return f_afectada;
        }
    }
}
