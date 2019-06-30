using System;
using SimpleBotCore.Repositorio.Mongo;
using SimpleBotCore.Tools;
using SimpleBotCore.Repositorio.SqlServer;

namespace SimpleBotCore.Logic
{
    public class SimpleBotUser
    {
        private readonly MensagemRepositorio _mensagemRepositorio;
        private readonly ContadorRepositorio _contadorRepositorio;
        private readonly LogRepositorio _logRepositorio;

        public SimpleBotUser(MensagemRepositorio mensagemRepositorio, ContadorRepositorio contadorRepositorio, LogRepositorio logRepositorio)
        {
            _mensagemRepositorio = mensagemRepositorio;
            _contadorRepositorio = contadorRepositorio;
            _logRepositorio = logRepositorio;
        }

        public string Reply(SimpleMessage message)
        {

            try
            {
                _logRepositorio.GravarLog("Adicionando o contador");
                _contadorRepositorio.GravarContador(message);

                _logRepositorio.GravarLog($"Mensagem enviada: {message.Text}");
                _mensagemRepositorio.GravarMensagem(message);

                return $"{message.User} disse '{message.Text}'";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SimpleBotUser - Ocorreu um erro no metodo Reply. Erro: {ExceptionHelper.RecuperarDescricaoErro(ex)}");
            }

            return "";
            
        }

    }
}