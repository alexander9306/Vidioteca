using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Biblioteca.Entidades;

namespace Biblioteca.Web.Datos
{
    public class Core_Elenco
    {
        SqlConnection conexion = new SqlConnection(db.GetConfiguration());
        SqlCommand cmd;

        private readonly Core_Pelicula _pelicula = new Core_Pelicula();
        private readonly Core_Actor _actor = new Core_Actor();

        //Crear un elenco
        public void CrearElenco(Elenco elenco)
        {
            cmd = new SqlCommand("insert into elenco(idpelicula, idactor) values(@idpelicula,@idactor)", conexion);
            conexion.Open();
            cmd.Parameters.AddWithValue("@idpelicula", elenco.idpelicula);
            cmd.Parameters.AddWithValue("@idactor", elenco.idactor);
            cmd.ExecuteNonQuery();
            conexion.Close();
        }

        //Actualizar un elenco
        public void ActualizarElenco(Elenco elenco)
        {
            cmd = new SqlCommand("update elenco set idpelicula=@idpelicula, idactor=@idactor where idelenco=@idelenco", conexion);
            conexion.Open();
            cmd.Parameters.AddWithValue("@idelenco", elenco.idelenco);
            cmd.Parameters.AddWithValue("@idpelicula", elenco.idpelicula);
            cmd.Parameters.AddWithValue("@idactor", elenco.idactor);
            cmd.ExecuteNonQuery();
            conexion.Close();
        }

        //Cargar elenco
        public Elenco CargarElenco(int idelenco)
        {
            conexion.Open();
            cmd = new SqlCommand("SELECT * FROM elenco where idelenco=@idelenco", conexion);
            cmd.Parameters.AddWithValue("@idelenco", idelenco);
            SqlDataReader rdr = cmd.ExecuteReader();
            rdr.Read();

            Elenco elenco = new Elenco();
            elenco.idelenco = Convert.ToInt32(rdr["idelenco"]);
            elenco.idpelicula = Convert.ToInt32(rdr["idpelicula"]);
            elenco.idactor = Convert.ToInt32(rdr["idactor"]);

            conexion.Close();

            return elenco;
        }

        //Cargar Peliculas por actor
        public IEnumerable<Pelicula> CargarPeliculas(int idactor)
        {
            conexion.Open();
            cmd = new SqlCommand("SELECT * FROM elenco where idactor=@idactor", conexion);
            cmd.Parameters.AddWithValue("@idactor", idactor);
            SqlDataReader rdr = cmd.ExecuteReader();
           
            List<Pelicula> lstpelicula = new List<Pelicula>();

            while (rdr.Read())
            {
                if (rdr.HasRows)
                {
                    lstpelicula.Add(_pelicula.CargarPelicula(Convert.ToInt32(rdr["idpelicula"])));
                }
            }

            conexion.Close();

            return lstpelicula;
        }

        //Cargar Actores por pelicula
        public IEnumerable<Actor> CargarActores(int idpelicula)
        {
            conexion.Open();
            cmd = new SqlCommand("SELECT * FROM elenco where idpelicula=@idpelicula", conexion);
            cmd.Parameters.AddWithValue("@idpelicula", idpelicula);
            SqlDataReader rdr = cmd.ExecuteReader();

            List<Actor> lstactor = new List<Actor>();

            while (rdr.Read())
            {
                if (rdr.HasRows)
                {
                    lstactor.Add(_actor.CargarActor(Convert.ToInt32(rdr["idactor"])));
                }
            }

            conexion.Close();

            return lstactor;
        }

        //Cargar todos los elencos
        public IEnumerable<Elenco> CargarElencos()
        {
            conexion.Open();
            cmd = new SqlCommand("SELECT * FROM elencos", conexion);
            SqlDataReader rdr = cmd.ExecuteReader();

            List<Elenco> lstelenco = new List<Elenco>();

            while (rdr.Read())
            {
                if (rdr.HasRows)
                {
                    Elenco elenco = new Elenco();
                    elenco.idelenco = Convert.ToInt32(rdr["idelenco"]);
                    elenco.idpelicula = Convert.ToInt32(rdr["idpelicula"]);
                    elenco.idactor = Convert.ToInt32(rdr["idactor"]);

                    lstelenco.Add(elenco);
                }
            }
            conexion.Close();
            return lstelenco;
        }

        //Borrar un elenco
        public int BorrarElenco(int idelenco)
        {
            cmd = new SqlCommand("delete elenco where idelenco=@idelenco", conexion);
            conexion.Open();
            cmd.Parameters.AddWithValue("@idelenco", idelenco);
            int f_afectada = cmd.ExecuteNonQuery();
            conexion.Close();
            return f_afectada;
        }
    }
}
