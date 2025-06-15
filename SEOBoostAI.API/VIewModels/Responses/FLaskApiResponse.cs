using System.Text.Json.Serialization;

namespace SEOBoostAI.API.VIewModels.Responses
{
    public class ExternalApiStatus
    {
        [JsonPropertyName("keyword")]
        public string Keyword { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }
    }

    public class FlaskApiResponse
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("generated_keywords_count")]
        public int GeneratedKeywordsCount { get; set; }

        [JsonPropertyName("external_api_status")]
        public List<ExternalApiStatus> ExternalApiStatus { get; set; }
    }
}
