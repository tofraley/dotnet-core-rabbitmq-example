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
    public class QueuePurchaseOrderController : Controller
    {       
        [HttpPost]
        public ActionResult SendPurchaseOrder([FromBody] PurchaseOrder purchaseOrder)
        {
            try
            {
                RabbitMQClient client = new RabbitMQClient();
                client.SendPurchaseOrder(purchaseOrder);
                client.Close();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(purchaseOrder);
        }
    }
}
