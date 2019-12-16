using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Biblioteca.Entidades;

namespace Biblioteca.Web.Datos
{
    public class Core_Pelicula_Foto
    {
        SqlConnection conexion = new SqlConnection(db.GetConfiguration());
        SqlCommand cmd;

        //Crear una nueva foto
        public int CrearFoto(byte[] pfoto)
        {
            conexion.Open();
            cmd = new SqlCommand("insert into pelicula_foto(foto) values(@foto)", conexion);

            if (pfoto != null)
            {
                cmd.Parameters.AddWithValue("@foto", pfoto);
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
        public Pelicula_Foto CargarFoto(int? idfoto)
        {
            Pelicula_Foto foto = new Pelicula_Foto();

            if (idfoto == null)
            {
                return foto;
            }

            conexion.Open();
            cmd = new SqlCommand("SELECT * FROM pelicula_foto where idfoto=@idfoto", conexion);
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
            cmd = new SqlCommand("update pelicula_foto set foto=@foto where idfoto=@idfoto", conexion);
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
