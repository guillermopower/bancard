using Bancard.API.Business;
using Bancard.Core;
using Bancard.Core.Helper;
using Bancard.Core.Models;
using Bancard.DAL.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Bancard.API.Controllers
{
    [EnableCors("AllowSpecific")]
    [ApiController]
    [Route("api/[controller]")]
    public class BancardController : Controller
    {
        private readonly BancardService bancardService;
        private readonly FileService fileService;
        private readonly FpgBancardBusiness fpgBancardService;
        public BancardController()
        {
            this.bancardService = new BancardService();
            this.fileService = new FileService();
            this.fpgBancardService = new FpgBancardBusiness();
        }

        [Route("singlebuy")]
        [HttpPost]
        public async Task<ActionResult> SimpleBuy(SingleBuyModel model)
        {
            var singlebuyRequestModel = this.bancardService.Convert(model.shop_process_id, model.currency, model.amount, model.iva_amount, model.additional_data, model.description, model.return_url, model.cancel_url);
            var res = await this.bancardService.SingleBuy(singlebuyRequestModel);
            var resJson = JsonConvert.DeserializeObject<ResponseSingleBuy>(res);
            if (resJson.status != "error")
            {
                singlebuyRequestModel.operation.description = "res";
                this.fpgBancardService.Save(singlebuyRequestModel.operation);
            }
            
            return Ok(res);
        }

        [Route("singlebuyrollback")]
        [HttpPost]
        public async Task<ActionResult> SimpleBuyRollBack([FromBody] SingleBuyRollBackModel model)
        {
            //resJson se tendria que guardar en la tabla de logs
            var resJson = JsonConvert.SerializeObject(model);
            var res = this.bancardService.SingleBuyRollBack(model.shop_process_id);
            return Ok(res);
        }

        [Route("singlebuygetconfirmations/{shop_process_id}")]
        [HttpGet]
        public async Task<ActionResult> SingleBuyGetConfirmations(int shop_process_id)
        {
            var res = this.bancardService.SingleBuyGetConfirmation(shop_process_id);
            return Ok(res);
        }

        [Route("singlebuyconfirmation")]
        [HttpPost]
        public async Task<ActionResult> SingleBuyConfirmation([FromBody] RequestConfirmationModel model)
        {
            //resJson se tendria que guardar en la tabla de logs
            var resJson = JsonConvert.SerializeObject(model);
            var res = this.fpgBancardService.Save(model);
            //this.fileService.write(resJson, "c:\\prueba.json");
            if(res) return Ok(); else return BadRequest();
        }

        [Route("checktransaction/{id}")]
        [HttpGet]
        public async Task<ActionResult<FpgBancard>> CheckTransaction(int id)
        {
            var res = this.fpgBancardService.Get(id);
            if (res.Count>0) return Ok(res.FirstOrDefault()); else return NoContent();
        }
    }
       
}
