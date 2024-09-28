using Javsdt.Shared.Converters;
using Javsdt.Shared.Enums;
using System.Text.Json.Serialization;

namespace Javsdt.Domain.Entitys
{
    /// <summary>
    /// 一部影片的元数据
    /// </summary>
    /// <remarks>从MovieDb查询得到</remarks>
    public partial class Movie
    {
        [JsonPropertyName("car")]
        public string Car { get; set; } = default!;

        [JsonPropertyName("type")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public JavType Type { get; set; }

        [JsonPropertyName("carPref")]
        public string CarPref { get; set; } = default!;

        public string? OriginName { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; } = default!;

        [JsonPropertyName("zhTitle")]
        public string? ZhTitle { get; set; }

        [JsonPropertyName("plot")]
        public string? Plot { get; set; }

        [JsonPropertyName("zhPlot")]
        public string? ZhPlot { get; set; }

        [JsonPropertyName("series")]
        public string? Series { get; set; }

        [JsonPropertyName("review")]
        public string? Review { get; set; }

        [JsonPropertyName("release")]
        [JsonConverter(typeof(JsonDateTimeConverter))]
        public DateTime? Release { get; set; }

        [JsonPropertyName("runtime")]
        public int Runtime { get; set; }

        [JsonPropertyName("score")]
        public int Score { get; set; }

        [JsonPropertyName("makers")]
        public List<string> Makers { get; set; } = new List<string>();

        [JsonPropertyName("publishers")]
        public List<string> Publishers { get; set; } = new List<string>();

        [JsonPropertyName("actors")]
        public List<string> Actors { get; set; } = new List<string>();

        [JsonPropertyName("directors")]
        public List<string> Directors { get; set; } = new List<string>();

        [JsonPropertyName("tags")]
        public List<string> Tags { get; set; } = new List<string>();

        [JsonPropertyName("items")]
        public WebsiteItems Items { get; set; } = new WebsiteItems();

        /// <summary>
        /// 在各大网站的Id
        /// </summary>
        public class WebsiteItems
        {
            public string? JavLibrary { get; set; }

            public string? JavDb { get; set; }

            public string? JavBus { get; set; }

            public string? Jav321 { get; set; }

            public string? Arzon { get; set; }
        }
    }
}