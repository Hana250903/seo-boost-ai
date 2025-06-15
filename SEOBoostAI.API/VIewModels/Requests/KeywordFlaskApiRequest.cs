using System.Text.Json.Serialization;

namespace SEOBoostAI.API.VIewModels.Requests
{
    public class KeywordFlaskApiRequest
    {
        [JsonPropertyName("input_keyword")]
        public string InputKeyword { get; set; }
    }
}
