using Biblioteca.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;

namespace Biblioteca.Datos
{
    public class Core_Actor
    {

        //Debe cambiar el string de conexion aqui
       // static string conStr = ConfigurationManager.ConnectionStrings["Conexion"].ToString();
        
        static string conexion_string = "Data Source=DESKTOP-1MG6DKU;Initial Catalog=dbBiblioteca;Integrated Security=True";
        SqlConnection conexion = new SqlConnection(conexion_string);
        SqlCommand cmd;

        //Crear un actor
        public void CrearActor(Actor actor)
        {
            cmd = new SqlCommand("insert into actor(nombre, fechanac, sexo, idfoto) values(@nombre,@fechanac,@sexo,@idfoto)", conexion);
            conexion.Open();
            cmd.Parameters.AddWithValue("@nombre", actor.nombre);
            cmd.Parameters.AddWithValue("@fechanac", actor.fechanac);
            cmd.Parameters.AddWithValue("@sexo", actor.sexo);
            cmd.Parameters.AddWithValue("@idfoto", actor.idfoto);
            
            cmd.ExecuteNonQuery();
            conexion.Close();
        }

        //Actualizar un actor
        public void ActualizarActor(Actor actor)
        {

            cmd = new SqlCommand("update actor set nombre=@nombre, fechanac=@fechanac, sexo=@sexo where idactor=@idactor", conexion);
            conexion.Open();
            cmd.Parameters.AddWithValue("@idactor", actor.idactor);
            cmd.Parameters.AddWithValue("@nombre", actor.nombre);
            cmd.Parameters.AddWithValue("@fechanac", actor.fechanac);
            cmd.Parameters.AddWithValue("@sexo", actor.sexo);
            
            cmd.ExecuteNonQuery();
            conexion.Close();
        }

        //Cargar actor
        public Actor CargarActor(int idactor)
        {
            conexion.Open();
            cmd = new SqlCommand("SELECT * FROM actor where idactor=@idactor", conexion);
            cmd.Parameters.AddWithValue("@idactor", idactor);
            SqlDataReader rdr = cmd.ExecuteReader();
            rdr.Read();

            Actor actor = new Actor();
            actor.idactor = Convert.ToInt32(rdr["idactor"]);
            actor.nombre = rdr["nombre"].ToString();
            actor.fechanac = Convert.ToDateTime(rdr["fechanac"]);
            actor.sexo = Convert.ToChar(rdr["sexo"]);
            actor.idfoto = Convert.ToInt32(rdr["idfoto"]);
            
            conexion.Close();

            return actor;
        }

        //Cargar actores por sexo
        public IEnumerable<Actor> CargarActorSexo(string sexo)
        {
            conexion.Open();
            cmd = new SqlCommand("SELECT * FROM actor where sexo=@sexo", conexion);
            cmd.Parameters.AddWithValue("@sexo", sexo);
            SqlDataReader rdr = cmd.ExecuteReader();
            
            List<Actor> lstactor = new List<Actor>();

            while (rdr.Read())
            {
                if (rdr.HasRows)
                {
                    Actor actor = new Actor();
                    actor.idactor = Convert.ToInt32(rdr["idactor"]);
                    actor.nombre = rdr["nombre"].ToString();
                    actor.fechanac = Convert.ToDateTime(rdr["fechanac"]);
                    actor.sexo = Convert.ToChar(rdr["sexo"]);
                    actor.idfoto = Convert.ToInt32(rdr["idfoto"]);

                    lstactor.Add(actor);
                }

            }
            conexion.Close();

            return lstactor;
        }

        //Cargar todos los actores
        public IEnumerable<Actor> CargarActores()
        {
            conexion.Open();
            cmd = new SqlCommand("SELECT * FROM actor", conexion);
            SqlDataReader rdr = cmd.ExecuteReader();

            List<Actor> lstactor = new List<Actor>();

            while (rdr.Read())
            {
                if (rdr.HasRows)
                {
                    Actor actor = new Actor();
                    actor.idactor = Convert.ToInt32(rdr["idactor"]);
                    actor.nombre = rdr["nombre"].ToString();
                    actor.fechanac = Convert.ToDateTime(rdr["fechanac"]);
                    actor.sexo = Convert.ToChar(rdr["sexo"]);
                    actor.idfoto = Convert.ToInt32(rdr["idfoto"]);

                    lstactor.Add(actor);
                }

            }
            conexion.Close();

            return lstactor;
        }

        //Borrar actor
        public int BorrarActor(int idactor)
        {
            cmd = new SqlCommand("delete actor where idactor=@idactor", conexion);
            conexion.Open();
            cmd.Parameters.AddWithValue("@idactor", idactor);
            int f_afectada = cmd.ExecuteNonQuery();
            conexion.Close();
            return f_afectada;
        }

    }
}
