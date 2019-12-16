using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Biblioteca.Entidades;

namespace Biblioteca.Datos
{
    public class Core_Actor_Foto
    {
        static string conexion_string = "Data Source=DESKTOP-1MG6DKU;Initial Catalog=dbBiblioteca;Integrated Security=True";
        SqlConnection conexion = new SqlConnection(conexion_string);
        SqlCommand cmd;

        //Crear una nueva foto
        public int CrearFoto(byte[] afoto)
        {
            conexion.Open();
            cmd = new SqlCommand("insert into actor_foto(foto) values(@foto)", conexion);

            if (afoto != null)
            {
                cmd.Parameters.AddWithValue("@foto", afoto);
            }
            else
            {
                cmd.Parameters.Add("@foto", SqlDbType.VarBinary, -1).Value = DBNull.Value;
            }
            cmd.ExecuteNonQuery();

            SqlCommand cargarId = new SqlCommand("SELECT count(*) from actor_foto ", conexion);
            var nuevoid = Convert.ToInt32(cargarId.ExecuteScalar());
            conexion.Close();
            return nuevoid;
        }

        //Cargar una foto
        public Actor_Foto CargarFoto(int? idfoto)
        {
            Actor_Foto foto = new Actor_Foto();
            
            if (idfoto == null)
            {
                return foto;
            }

            conexion.Open();
            cmd = new SqlCommand("SELECT * FROM actor_foto where idfoto=@idfoto", conexion);
            cmd.Parameters.AddWithValue("@idfoto", idfoto);
            SqlDataReader rdr = cmd.ExecuteReader();
            rdr.Read();

            foto.idfoto = Convert.ToInt32(rdr["idfoto"]);
            foto.foto = rdr["foto"] as byte[];

            conexion.Close();

            return foto;
        }

        //Actualizar una foto
        public void ActualizarFoto(int idfoto, byte[] foto)
        {
            cmd = new SqlCommand("update actor_foto set foto=@foto where idfoto=@idfoto", conexion);
            conexion.Open();
            cmd.Parameters.AddWithValue("@idfoto", idfoto);

            if (foto != null)
            {
                cmd.Parameters.AddWithValue("@foto", foto);
            }
            else
            {
                cmd.Parameters.Add("@foto", SqlDbType.VarBinary, -1).Value = DBNull.Value;
            }
            cmd.ExecuteNonQuery();

            conexion.Close();
        }

    }
}
