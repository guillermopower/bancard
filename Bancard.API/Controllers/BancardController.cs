using Bancard.API.Models;
using Bancard.Core;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Bancard.API.Controllers
{
    [EnableCors("AllowSpecific")]
    [ApiController]
    [Route("api/[controller]")]
    public class BancardController : Controller
    {
        private readonly BancardService bancardService;
        public BancardController()
        {
            this.bancardService = new BancardService();

        }

        [Route("singlebuy")]
        [HttpPost]
        public async Task<ActionResult> SimpleBuy(SimpleBuyModel model)
        {
            var res = this.bancardService.SingleBuy(model.shop_process_id, model.currency, model.amount, model.iva_amount, model.additional_data, model.description, model.return_url, model.cancel_url);

            var resJson = JObject.Parse(res.Result.ToString());
            //if (resJson.GetValue("status").ToString().Contains("error")) return BadRequest(res);

            return Ok(res);

        }
    }
       
}
