namespace Bancard.Core.Models
{
    public class RequestConfirmationModel
    {
        public string public_key { get; set; } = string.Empty;
        public OperationConfirmationModel operation { get; set; }
    }
}
