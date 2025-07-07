namespace SEOBoostAI.Repository.ModelExtensions
{
    public class SearchRequest
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }

    public class KeywordSearchRequest : SearchRequest
    {
        public string Keyword { get; set; }
        public string? Competition { get; set; }
        public string? Intent { get; set; }
        public string? Trend { get; set; }

        // Thêm các thuộc tính cho sắp xếp
        public string? SortBy { get; set; } // Tên trường để sắp xếp (ví dụ: "Keyword1", "Difficulty", "Cpc")
        public string? SortOrder { get; set; } = "asc"; // Hướng sắp xếp: "asc" (tăng dần) hoặc "desc" (giảm dần)
    }
}
