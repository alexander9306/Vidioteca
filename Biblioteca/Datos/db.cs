using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Biblioteca.Web.Datos
{
    public class db
    {
        //Esta Clase se creo para configurar el connection string en todas las demas clases 

        public static string GetConfiguration()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            return builder.Build().GetSection("ConnectionString").GetSection("Conexion").Value;
        }


    }
    
}
