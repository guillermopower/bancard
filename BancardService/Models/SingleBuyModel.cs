namespace Bancard.API.Models
{
    [Serializable]
    public class SingleBuyModel: OperationModel
    {
        public int shop_process_id { get; set; }
        public string currency { get; set; }
        public string amount { get; set; }
        public string additional_data { get; set; }
        public string description { get; set; }
        public string return_url { get; set; }
        public string cancel_url { get; set; }

    }
}
