using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Bson;
using SimpleBotCore.Repositorio.Mongo;

namespace SimpleBotCore.Logic
{
    public class SimpleBotUser
    {
        private readonly MensagemRepositorio _mensagemRepositorio;
        private readonly ContadorRepositorio _contadorRepositorio;

        public SimpleBotUser(MensagemRepositorio mensagemRepositorio, ContadorRepositorio contadorRepositorio)
        {
            _mensagemRepositorio = mensagemRepositorio;
            _contadorRepositorio = contadorRepositorio;
        }

        public string Reply(SimpleMessage message)
        {

            try
            {
                _contadorRepositorio.GravarContador(message);
                _mensagemRepositorio.GravarMensagem(message);

                return $"{message.User} disse '{message.Text}'";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //throw;
            }


            return "";
            
        }

    }
}