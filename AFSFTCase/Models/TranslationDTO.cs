using Newtonsoft.Json;

namespace AFSFTCase.Models
{
    public class TranslationDTO
    {
        [JsonProperty("text")]
        public string InputString { get; set; }

        [JsonProperty("translated")]
        public string TranslatedString { get; set; }

        [JsonProperty("translation")]
        public string Translator { get; set; }
    }

    public class TranslationObjectDTO
    {
        [JsonProperty("contents")]
        public TranslationDTO Contents { get; set; }

    }
}
