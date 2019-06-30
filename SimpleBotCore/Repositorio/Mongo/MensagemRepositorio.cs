using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using SimpleBotCore.App_Code;
using SimpleBotCore.Logic;
using SimpleBotCore.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBotCore.Repositorio.Mongo
{
    public class MensagemRepositorio
    {
        private readonly MongoClient _mongoClient;
        private readonly IMongoDatabase _dataBase;

        public MensagemRepositorio(IConfiguration config)
        {
            try
            {
                _mongoClient = new MongoClient(Util_ConfigSecrets.StrConnectionMongoDB(config));
                _dataBase = _mongoClient.GetDatabase("15Net");
            }
            catch (Exception ex)
            {

                Console.WriteLine($"MensagemRepositorio - Ocorreu um erro no metodo MensagemRepositorio. Erro: {ExceptionHelper.RecuperarDescricaoErro(ex)}");
            }
        }

        public void GravarMensagem(SimpleMessage mensagem)
        {
            var colecaoMensagem = _dataBase.GetCollection<SimpleMessage>("message");

            mensagem.Id = Convert.ToString(ObjectId.GenerateNewId(DateTime.Now));
            colecaoMensagem.InsertOne(mensagem);

        }


        public List<SimpleMessage> RetornarTodasMensagens()
        {
            var colecaoMensagem = _dataBase.GetCollection<SimpleMessage>("message");
            var linhas = colecaoMensagem.Find(FilterDefinition<SimpleMessage>.Empty);
            return linhas.ToList();
        }
    }
}
