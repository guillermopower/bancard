namespace Bancard.API.Models
{
    public class TokenPayModel
    {
        public int shop_process_id { get; set; }
        public string amount { get; set; }
        public string iva_amount { get; set; }
        public int number_of_payments { get; set; }
        public string currency { get; set; }
        public string additional_data { get; set; }
        public string description { get; set; }
        public string alias_token { get; set; }
    }
}
