namespace Bancard.Core.Models
{
    public class RequestSingleBuyModel
    {
        public string public_key { get; set; }
        public RequestSingleBuyOperationModel operation { get; set; }
    }
}
