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
    public class ContadorRepositorio
    {
        private readonly MongoClient _mongoClient;
        private readonly IMongoDatabase _dataBase;

        public ContadorRepositorio(IConfiguration config)
        {
            try
            {
                _mongoClient = new MongoClient(Util_ConfigSecrets.StrConnectionMongoDB(config));
                _dataBase = _mongoClient.GetDatabase("15Net");
            }
            catch (Exception ex)
            {

                Console.WriteLine($"ContadorRepositorio - Ocorreu um erro no metodo ContadorRepositorio. Erro: {ExceptionHelper.RecuperarDescricaoErro(ex)}");
            }
        }

        public void GravarContador(SimpleMessage mensagem)
        {

            try
            {
                var colecaoContador = _dataBase.GetCollection<BsonDocument>("usuarioContador");
                var filter = Builders<BsonDocument>.Filter.Eq("id", mensagem.Id);

                var contador = colecaoContador.Find(filter);

                //Existe contador
                if (contador.Any())
                {
                    var contadorGravado = contador.FirstOrDefault();
                    var elemento = contadorGravado.GetElement("contador");

                    BsonElement bsonElement = new BsonElement("contador", Convert.ToInt32(elemento.Value) + 1);
                    contadorGravado.SetElement(bsonElement);

                    colecaoContador.DeleteMany(FilterDefinition<BsonDocument>.Empty);
                    colecaoContador.InsertOne(contadorGravado);
                }
                else
                {

                    //Zerar contador
                    colecaoContador.DeleteMany(FilterDefinition<BsonDocument>.Empty);

                    var usuarioContador = new BsonDocument()
                {
                    { "id", mensagem.Id },
                    { "contador", 1}
                };

                    colecaoContador.InsertOne(usuarioContador);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ContadorRepositorio - Ocorreu um erro no metodo GravarContador. Erro: {ExceptionHelper.RecuperarDescricaoErro(ex)}");
            }

            
        }

        public Int32 RetornarContador()
        {

            try
            {
                var colecaoContador = _dataBase.GetCollection<BsonDocument>("usuarioContador");
                var contador = colecaoContador.Find(FilterDefinition<BsonDocument>.Empty);

                if (contador.Any())
                {
                    var contadorGravado = contador.FirstOrDefault();
                    var elemento = contadorGravado.GetElement("contador");

                    return Convert.ToInt32(elemento.Value);

                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"ContadorRepositorio - Ocorreu um erro no metodo RetornarContador. Erro: {ExceptionHelper.RecuperarDescricaoErro(ex)}");
            }

            return 0;
        }

    }
}
