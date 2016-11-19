using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RawRabbit.vNext;
using RawRabbit.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit.Common;
using RawRabbit.Attributes;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MessagingService.Controllers
{
    [Route("api/[controller]")]
    public class MessagesController : Controller
    {
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]BasicMessageDto messageDto)
        {
            var config = new RawRabbitConfiguration
            {
                Username = "guest",
                Password = "guest",
                Port = 5672,
                VirtualHost = "/",
                Hostnames = { "52.175.209.90" }
                // more props here.
            };
            var client = BusClientFactory.CreateDefault(config);

            var messageId = Guid.NewGuid();
            await client.PublishAsync<BasicMessage>(new BasicMessage { Text = messageDto.Message}, messageId)
                .ConfigureAwait(false);

            return Created("http://localhost:5000/api/messages", messageId);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
