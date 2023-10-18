namespace Bancard.Core.Models
{
    public class TokenPayModel:OperationModel
    {
        public int shop_process_id { get; set; }
        public string amount { get; set; }
        public string iva_amount { get; set; }
        public int number_of_payments { get; set; }
        public string currency { get; set; }
        public string additional_data { get; set; }
        public string preauthorization { get; set; }
        public string alias_token { get; set; }
    }
}
