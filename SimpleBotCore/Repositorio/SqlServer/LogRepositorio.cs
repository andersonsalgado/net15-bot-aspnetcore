using Microsoft.Extensions.Configuration;
using SimpleBotCore.App_Code;
using SimpleBotCore.Tools;
using System;
using System.Data.SqlClient;

namespace SimpleBotCore.Repositorio.SqlServer
{
    public class LogRepositorio
    {
        private readonly IConfiguration _config;

        public LogRepositorio(IConfiguration config)
        {
            
            _config = config;
        }

        public void GravarLog(string mensagemLog)
        {

            try
            {

                using (var _sqlClient = new SqlConnection(Util_ConfigSecrets.StrConnectionSqlServer(_config)))
                {
                    _sqlClient.Open();

                    SqlCommand sqlCommand = new SqlCommand("INSERT INTO LOGS_BOT (MENSAGEM, DATACRIACAO) VALUES (@MENSAGEM, @DATACRIACAO)", _sqlClient);

                    sqlCommand.Parameters.AddWithValue("@MENSAGEM", mensagemLog);
                    sqlCommand.Parameters.AddWithValue("@DATACRIACAO", DateTime.Now);
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"LogRepositorio - Ocorreu um erro no metodo GravarLog. Erro: {ExceptionHelper.RecuperarDescricaoErro(ex)}");
            }
        }
    }
}
