using Newtonsoft.Json;

namespace EmuStore.Models
{
    public partial class Game
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("fileName")]
        public string FileName { get; set; }

        [JsonProperty("platform")]
        public string Platform { get; set; }

        [JsonProperty("size")]
        public long Size { get; set; }
    }
}
