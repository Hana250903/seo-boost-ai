﻿using System.Text.Json.Serialization;

namespace SEOBoostAI.API.VIewModels.Requests
{
    public class RankTrackingFlaskApiRequest
    {

        [JsonPropertyName("input_keyword")]
        public string InputKeyword { get; set; }

        [JsonPropertyName("user_id")]
        public string UserId { get; set; }

    }

    public class RankTrackingUpdateRequest
    {
        [JsonPropertyName("input_keyword")]
        public string InputKeyword { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }
    } 
}
