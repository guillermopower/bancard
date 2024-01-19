namespace Bancard.Core.Models
{
    public class SecurityInformation
    {
        public string customer_ip { get; set; }
        public string card_source { get; set; }
        public string card_country { get; set; }
        public string version { get; set; }
        public int risk_index { get; set; }
    }
}
