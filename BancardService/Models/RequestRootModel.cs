namespace Bancard.API.Models
{
    public class RequestRootModel
    {
        public string public_key { get; set; }
        public OperationModel operation { get; set; }
    }
}
