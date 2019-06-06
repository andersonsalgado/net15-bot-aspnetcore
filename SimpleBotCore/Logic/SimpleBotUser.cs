using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Bson;


namespace SimpleBotCore.Logic
{
    public class SimpleBotUser
    {
        public string Reply(SimpleMessage message)
        {
            var cliente = new MongoClient("mongodb://localhost:27017");

            var db = cliente.GetDatabase("15Net");

            var col = db.GetCollection<SimpleMessage>("message");

            var colContador = db.GetCollection<UsuarioContador>("usuarioContador");

            var contador = colContador.Find(x => x.Id.Equals(message.Id));

            var usuarioContador = new UsuarioContador();
            //Existe contador
            if (contador.Any())
            {
                var contadorGravado = contador.FirstOrDefault();
                contadorGravado.contador++;
                //var filterContador = Builders<UsuarioContador>.Filter.Where(x => x.Id.Equals(message.Id));
                colContador.DeleteMany(FilterDefinition<UsuarioContador>.Empty);
                colContador.InsertOne(contadorGravado);
            }
            else
            {

                //Zerar contador
                colContador.DeleteMany(FilterDefinition<UsuarioContador>.Empty);

                usuarioContador.Id = message.Id;
                usuarioContador.contador++;

                colContador.InsertOne(usuarioContador);

                
            }
            

            message.Id = $"{message.Id}_{DateTime.Now}";

            col.InsertOne(message);

            return $"{message.User} disse '{message.Text}'";
        }

    }
}