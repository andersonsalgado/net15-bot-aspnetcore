using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBotCore.App_Code
{
    public class Util_ConfigSecrets
    {
        public static String StrConnectionMongoDB(IConfiguration config)
        {
            String strConnection = config.GetConnectionString("mongoDBConnection");
            return strConnection;
        }

        /// <summary>
        /// A conexão do banco Sql Server deve ser feita por um arquivo secrets. 
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public static String StrConnectionSqlServer(IConfiguration config)
        {
            String strConnection = config.GetConnectionString("sqlServerConnection");
            return strConnection;
        }

    }
}
