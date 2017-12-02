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
    public class QueueCardPaymentController : Controller
    {       
        [HttpPost]
        public ActionResult SendPayment([FromBody] CardPayment payment)
        {
            try
            {
                RabbitMQClient client = new RabbitMQClient();
                client.SendPayment(payment);
                client.Close();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(payment);
        }
    }
}

