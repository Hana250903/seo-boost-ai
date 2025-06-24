using System.Text.Json.Serialization;

namespace SEOBoostAI.API.VIewModels.Requests
{
    public class ContentFlaskApiRequest
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("user_id")]
        public int UserId { get; set; }

        [JsonPropertyName("keyword")]
        public string Keyword { get; set; }

        [JsonPropertyName("content")]
        public string Content { get; set; }

        [JsonPropertyName("content_length")]
        public string ContentLenght { get; set; }

        [JsonPropertyName("optimization_level")]
        public int OptimizationLevel { get; set; }

        [JsonPropertyName("readability_level")]
        public string ReadabilityLevel { get; set; }

        [JsonPropertyName("include_citation")]
        public bool IncludeCitation { get; set; }
    }
}
