using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Biblioteca.Entidades;

namespace Biblioteca.Web.Datos
{
    public class Core_Actor_Foto
    {
        SqlConnection conexion = new SqlConnection(db.GetConfiguration());
        SqlCommand cmd;

        //Crear una nueva foto
        public void CrearFoto(Actor_Foto foto)
        {
            conexion.Open();
            cmd = new SqlCommand("insert into actor_foto(idfoto,foto) values(@idfoto,@foto)", conexion);

            if (foto.foto != null)
            {
                cmd.Parameters.AddWithValue("@foto", foto.foto);
            }
            else
            {
                cmd.Parameters.Add("@foto", SqlDbType.VarBinary, -1).Value = DBNull.Value;
            }
            cmd.Parameters.AddWithValue("@idfoto", foto.idfoto);

            cmd.ExecuteNonQuery();

           
            conexion.Close();
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
        public void ActualizarFoto(Actor_Foto foto)
        {
            cmd = new SqlCommand("update actor_foto set foto=@foto where idfoto=@idfoto", conexion);
            conexion.Open();
            cmd.Parameters.AddWithValue("@idfoto", foto.idfoto);

            if (foto.foto != null)
            {
                cmd.Parameters.AddWithValue("@foto", foto.foto);
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
