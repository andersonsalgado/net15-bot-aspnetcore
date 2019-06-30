using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using SimpleBotCore.App_Code;
using SimpleBotCore.Logic;
using SimpleBotCore.Repositorio.Mongo;

namespace SimpleBotCore.Controllers
{
    [Route("api/[controller]")]
    public class MessagesController : Controller
    {
        SimpleBotUser _bot;

        public String _strConnection;

        public MessagesController(SimpleBotUser bot, IConfiguration config)
        {
            this._bot = bot;
            this._strConnection = Util_ConfigSecrets.StrConnectionMongoDB(config);
        }

        [HttpGet]
        public string Get()
        {
            return "Hello World";
        }

        [HttpGet]
        [Route("getAllMessagens")]
        public List<SimpleMessage> GetAllMessagens()
        {
            var cliente = new MongoClient(this._strConnection);
            var db = cliente.GetDatabase("15Net");
            var col = db.GetCollection<SimpleMessage>("message");

            var linhas = col.Find(FilterDefinition<SimpleMessage>.Empty);
            return linhas.ToList();
        }


        // POST api/messages
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Activity activity)
        {
            if (activity != null && activity.Type == ActivityTypes.Message)
            {
                await HandleActivityAsync(activity);
            }

            // HTTP 202
            return Accepted();
        }

        // Estabelece comunicacao entre o usuario e o SimpleBotUser
        async Task HandleActivityAsync(Activity activity)
        {
            string text = activity.Text;
            string userFromId = activity.From.Id;
            string userFromName = activity.From.Name;

            var message = new SimpleMessage(userFromId, userFromName, text);

            string response = _bot.Reply(message);

            await ReplyUserAsync(activity, response);
        }

        // Responde mensagens usando o Bot Framework Connector
        async Task ReplyUserAsync(Activity message, string text)
        {
            var connector = new ConnectorClient(new Uri(message.ServiceUrl));
            var reply = message.CreateReply(text);

            await connector.Conversations.ReplyToActivityAsync(reply);
        }
    }
}
