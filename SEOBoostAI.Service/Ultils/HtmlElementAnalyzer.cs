using HtmlAgilityPack;
using SEOBoostAI.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SEOBoostAI.Service.Ultils
{
    public static class HtmlElementAnalyzer
    {
        public static async System.Threading.Tasks.Task<List<Element>> AnalyzeUrlAsync(string url, int auditId)
        {
            var httpClient = new HttpClient();
            var html = await FetchHtmlAsync(url);
            html = await httpClient.GetStringAsync(url);

            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            string[] importantTags = {
                "title", "meta", "h1", "h2", "img", "a",
                "link", "script", "form", "input",
                "label", "button", "textarea", "select"
            };

            var nodes = doc.DocumentNode
                .Descendants()
                .Where(n => n.NodeType == HtmlNodeType.Element && importantTags.Contains(n.Name.ToLower()))
                .ToList();

            var results = new List<Element>();

            foreach (var node in nodes)
            {
                string tag = node.Name.ToLower();
                string mainValue = "";
                string status = "pass";
                string description = "";
                int important = 0;

                switch (tag)
                {
                    case "meta":
                        mainValue = node.GetAttributeValue("content", "");
                        if (string.IsNullOrWhiteSpace(mainValue))
                        {
                            status = "not pass";
                            description = "Thẻ <meta> không có thuộc tính content hoặc nội dung bị rỗng.";
                        }
                        important = 2;
                        break;
                    case "img":
                        mainValue = node.GetAttributeValue("alt", "");
                        if (string.IsNullOrWhiteSpace(mainValue))
                        {
                            status = "not pass";
                            description = "Thẻ <img> thiếu thuộc tính alt (mô tả hình ảnh).";
                            important = 2;
                        }
                        else
                        {
                            important = 1;
                        }
                        break;
                    case "link":
                        string href = node.GetAttributeValue("href", "");
                        mainValue = href;
                        if (string.IsNullOrWhiteSpace(href))
                        {
                            status = "not pass";
                            description = "Thẻ <link> thiếu thuộc tính href.";
                            important = 1;
                        }
                        else
                        {
                            important = 1;
                        }
                        break;
                    case "a":
                        mainValue = node.InnerText?.Trim() ?? "";
                        string ahref = node.GetAttributeValue("href", "");
                        if (string.IsNullOrWhiteSpace(ahref) || ahref == "#")
                        {
                            status = "not pass";
                            description = "Thẻ <a> không có liên kết hợp lệ (href).";
                            important = 1;
                        }
                        else if (string.IsNullOrWhiteSpace(mainValue))
                        {
                            status = "not pass";
                            description = "Thẻ <a> có href nhưng không có nội dung văn bản.";
                            important = 1;
                        }
                        else
                        {
                            important = 1;
                        }
                        break;
                    case "input":
                        mainValue = node.GetAttributeValue("name", "");
                        if (string.IsNullOrWhiteSpace(mainValue))
                        {
                            status = "not pass";
                            description = "Thẻ <input> thiếu thuộc tính name.";
                            important = 1;
                        }
                        else
                        {
                            important = 1;
                        }
                        break;
                    case "label":
                        mainValue = node.GetAttributeValue("for", "");
                        if (string.IsNullOrWhiteSpace(mainValue))
                        {
                            status = "not pass";
                            description = "Thẻ <label> thiếu thuộc tính for (không liên kết được với input).";
                            important = 1;
                        }
                        else
                        {
                            important = 1;
                        }
                        break;
                    case "script":
                        mainValue = node.GetAttributeValue("src", "") ?? node.InnerText?.Trim() ?? "";
                        if (string.IsNullOrWhiteSpace(mainValue))
                        {
                            status = "not pass";
                            description = "Thẻ <script> không có src hoặc không chứa nội dung JavaScript.";
                            important = 1;
                        }
                        else
                        {
                            important = 1;
                        }
                        break;
                    default:
                        mainValue = node.InnerText?.Trim() ?? "";
                        if (string.IsNullOrWhiteSpace(mainValue))
                        {
                            status = "not pass";
                            description = $"Thẻ <{tag}> không có nội dung.";
                        }
                        important = importantTags.Contains(tag) ? 1 : 0;
                        break;
                }

                results.Add(new Element
                {
                    Url = url,
                    Element1 = tag,
                    CurrentValue = mainValue,
                    Status = status,
                    Important = important,
                    Description = description,
                    AuditReportId = auditId
                });
            }

            return results ?? new List<Element>();
        }

        public static async Task<string> FetchHtmlAsync(string url)
        {
            var httpClient = new HttpClient();
            httpClient.Timeout = TimeSpan.FromSeconds(10);
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (compatible; SEOAuditBot/1.0)");

            HttpResponseMessage response;

            try
            {
                response = await httpClient.GetAsync(url);
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Không thể kết nối tới URL: {ex.Message}");
            }

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Trang trả về mã lỗi: {(int)response.StatusCode} - {response.ReasonPhrase}");
            }

            if (!response.Content.Headers.ContentType.MediaType.Contains("text/html"))
            {
                throw new Exception("Nội dung không phải HTML.");
            }

            return await response.Content.ReadAsStringAsync();
        }
    }
}
