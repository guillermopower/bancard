namespace Bancard.API.Models
{
    public class CreditCardNewModel:OperationModel
    {
        public int card_id { get; set; }
        public int user_id { get; set; }
        public string user_cell_phone { get; set; }
        public string user_mail { get; set; }
        public string return_url { get; set; }
    }
}
