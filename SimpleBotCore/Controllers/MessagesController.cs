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
        private readonly MensagemRepositorio _mensagemRepositorio;
        private readonly ContadorRepositorio _contadorRepositorio;

        public MessagesController(SimpleBotUser bot, IConfiguration config, MensagemRepositorio mensagemRepositorio, ContadorRepositorio contadorRepositorio)
        {
            this._bot = bot;
            _mensagemRepositorio = mensagemRepositorio;
            _contadorRepositorio = contadorRepositorio;
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
            return _mensagemRepositorio.RetornarTodasMensagens();
        }


        // POST api/messages
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Activity activity)
        {
            if (activity != null && activity.Type == ActivityTypes.Message)
            {
                await HandleActivityAsync(activity);
            } else if (activity != null && activity.Type == ActivityTypes.ConversationUpdate)
            {
                await ReplyUserAsync(activity, "Digite \"contador\" para listar quantas mensagens já foram enviadas.");
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


            string response = "";

            if ("contador".Equals(text))
            {
                response = $"Você já digitou {Convert.ToString(_contadorRepositorio.RetornarContador())} mensagens";
            } else
            {
                response = _bot.Reply(message);
            }

            

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
