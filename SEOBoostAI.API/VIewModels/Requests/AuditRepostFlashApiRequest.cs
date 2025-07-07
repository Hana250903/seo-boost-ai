using SEOBoostAI.Repository.Models;
using System.Text.Json.Serialization;

namespace SEOBoostAI.API.VIewModels.Requests
{
    public class AuditRepostFlashApiRequest
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("user_id")]
        public int UserId { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("overall_score")]
        public int OverallScore { get; set; }

        [JsonPropertyName("critical_issue")]
        public int CriticalIssue { get; set; }

        [JsonPropertyName("warning")]
        public int Warning { get; set; }

        [JsonPropertyName("opportunity")]
        public int Opportunity { get; set; }

        [JsonPropertyName("passed_check")]
        public string PassedCheck { get; set; }

        [JsonPropertyName("failed_elements")]
        public List<Element> FailedElements { get; set; }
    }

    public class AuditRequest
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Url { get; set; }
        public int OverallScore { get; set; }
        public int CriticalIssue { get; set; }
        public int Warning { get; set; }
        public int Opportunity { get; set; }
        public string PassedCheck { get; set; }
    }

    public class Element
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("element")]
        public string Element1 { get; set; }

        [JsonPropertyName("current_value")]
        public string CurrentValue { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("important")]
        public int Important { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("audit_repost_id")]
        public int? AuditReportId { get; set; }
    }
}
