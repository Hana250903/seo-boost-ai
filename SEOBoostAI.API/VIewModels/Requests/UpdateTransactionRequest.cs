namespace SEOBoostAI.API.VIewModels.Requests
{
    public class UpdateTransactionRequest
    {
        public int TransactionId { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
    }
}
