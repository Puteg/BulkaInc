using Newtonsoft.Json;

namespace Bulka.Models.GameProcess
{
    public class PlayerItemViewItem
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }
        [JsonProperty(PropertyName = "image")]
        public string ImageUrl { get; set; }
    }
}