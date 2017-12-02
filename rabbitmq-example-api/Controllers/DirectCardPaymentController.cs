using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using rabbitmq_example_api.Models;
using rabbitmq_example_api.RabbitMQ;

namespace rabbitmq_example_api.Controllers
{
    [Route("api/[controller]")]
    public class DirectCardPaymentController : Controller
    {       
        [HttpPost]
        public ActionResult MakePayment([FromBody] CardPayment payment)
        {
            string reply;

            try
            {
                RabbitMQDirectClient client = new RabbitMQDirectClient();
                client.CreateConnection();
                reply = client.MakePayment(payment);

                client.Close();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(reply);
        }
    }
}
