﻿namespace Bancard.API.Models
{
    [Serializable]
    public class PreauthorizationModel : OperationModel
    {
        public int shop_process_id { get; set; }
    }
}
