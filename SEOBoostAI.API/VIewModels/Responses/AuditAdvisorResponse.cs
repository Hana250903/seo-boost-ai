using System.Text.Json.Serialization;

namespace SEOBoostAI.API.VIewModels.Responses
{
    public class AuditAdvisorResponse
    {
        public class SeoAdvisorResponse
        {
            [JsonPropertyName("advice")]
            public AdviceData Advice { get; set; }

            [JsonPropertyName("next_steps_message")]
            public string NextStepsMessage { get; set; }

            [JsonPropertyName("summary")]
            public SummaryData Summary { get; set; }
        }

        public class AdviceData
        {
            [JsonPropertyName("critical_issues")]
            public List<IssueDetail> CriticalIssues { get; set; } = new List<IssueDetail>();

            [JsonPropertyName("opportunities")]
            public List<IssueDetail> Opportunities { get; set; } = new List<IssueDetail>();

            [JsonPropertyName("warnings")]
            public List<IssueDetail> Warnings { get; set; } = new List<IssueDetail>();
        }

        // Lớp chi tiết cho mỗi vấn đề (Critical, Warning, Opportunity)
        public class IssueDetail
        {
            [JsonPropertyName("affected_urls")]
            public List<string> AffectedUrls { get; set; } = new List<string>();

            // example_error_detail có thể là null, nên dùng kiểu nullable
            // Hoặc Dictionary<string, object> nếu cấu trúc bên trong thay đổi
            // Hoặc một lớp cụ thể nếu cấu trúc luôn cố định như JSON bạn cho
            [JsonPropertyName("example_error_detail")]
            public ExampleErrorDetailData? ExampleErrorDetail { get; set; } // Sử dụng kiểu nullable '?'

            [JsonPropertyName("fix_steps")]
            public List<string> FixSteps { get; set; } = new List<string>();

            [JsonPropertyName("importance")]
            public string Importance { get; set; }

            [JsonPropertyName("issue_type")]
            public string IssueType { get; set; }
        }

        // Lớp cụ thể cho ExampleErrorDetail
        // Dựa trên JSON bạn cung cấp, đây là cấu trúc của nó
        public class ExampleErrorDetailData
        {
            [JsonPropertyName("audit_repost_id")]
            public int AuditRepostId { get; set; }

            [JsonPropertyName("current_value")]
            public string CurrentValue { get; set; }

            [JsonPropertyName("description")]
            public string Description { get; set; }

            [JsonPropertyName("element")]
            public string Element { get; set; }

            [JsonPropertyName("id")]
            public int Id { get; set; }

            [JsonPropertyName("important")]
            public int Important { get; set; }

            [JsonPropertyName("status")]
            public string Status { get; set; }

            [JsonPropertyName("url")]
            public string Url { get; set; }
        }

        // Lớp tóm tắt thông tin tổng quan
        public class SummaryData
        {
            [JsonPropertyName("critical_issues_count")]
            public int CriticalIssuesCount { get; set; }

            [JsonPropertyName("opportunity_count")]
            public int OpportunityCount { get; set; }

            [JsonPropertyName("overall_score")]
            public int OverallScore { get; set; }

            [JsonPropertyName("url")]
            public string Url { get; set; }

            [JsonPropertyName("warning_count")]
            public int WarningCount { get; set; }
        }
    }
}
