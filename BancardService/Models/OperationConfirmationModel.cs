namespace Bancard.Core.Models
{
    public class OperationConfirmationModel: OperationModel
    {
        public string response { get; set; }
        public string response_details { get; set; }
        public string amount { get; set; }
        public string currency { get; set; }
        public string authorization_number { get; set; }
        public string ticket_number { get; set; }
        public string response_code { get; set; }
        public string response_description { get; set; }
        public string? extended_response_description { get; set; }
        public SecurityInformation security_information { get; set; }
    }
}
