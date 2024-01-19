using Bancard.Core.Models;
using Bancard.DAL;
using Bancard.DAL.Models;

namespace Bancard.API.Business
{
    public class FpgBancardBusiness
    {
        private readonly FpgBancardDAL fpgBancardDAL;
        public FpgBancardBusiness()
        { 
            this.fpgBancardDAL = new FpgBancardDAL();
        }

        public bool Save(RequestConfirmationModel request)
        {
            var op = request.operation;
            var res = false;
            try
            {
                var item = new FpgBancard()
                {
                    Date = DateTime.Now,
                    Token = op.token,
                    ShopProcessId = op.shop_process_id,
                    Response = op.response,
                    ResponseDetails = op.response_details,
                    Amount = op.amount,
                    Currency = op.currency,
                    AuthorizationNumber = op.authorization_number,
                    TicketNumber = op.ticket_number,
                    ResponseCode = op.response_code,
                    ResponseDescription = op.response_description,
                    ExtendedResponseDescription = op.extended_response_description,

                };

                this.fpgBancardDAL.Save(item);
                res = true;
            }
            catch (Exception)
            {
                throw;
            }
            return res;

        }

        public bool Save(RequestSingleBuyOperationModel op)
        {
            var res = false;
            try
            {
                var item = new FpgBancard()
                {
                    Date = DateTime.Now,
                    Token = op.token,
                    ShopProcessId = op.shop_process_id,
                    //Response = op.response,
                    ResponseDetails = op.description,
                    Amount = op.amount,
                    Currency = op.currency,
                    //AuthorizationNumber = op.authorization_number,
                    //TicketNumber = op.ticket_number,
                    //ResponseCode = op.response_code,
                    //ResponseDescription = op.response_description,
                    //ExtendedResponseDescription = op.extended_response_description,

                };

                this.fpgBancardDAL.Save(item);
                res = true;
            }
            catch (Exception)
            {
                throw;
            }
            return res;

        }

        public List<FpgBancard> Get(int transactionId)
        {
            var res = new List<FpgBancard>();
            try
            {
                res = this.fpgBancardDAL.Get(transactionId);
            }
            catch (Exception)
            {

                throw;
            }

            return res;
        }
    }
}
