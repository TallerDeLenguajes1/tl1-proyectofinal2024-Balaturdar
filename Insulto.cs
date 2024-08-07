using System.Text.Json.Serialization;

public class Insulto
{
    [JsonPropertyName("language")]
    public string Language { get; set; }
    [JsonPropertyName("insult")]
    public string Insult { get; set; }
    [JsonPropertyName("created")]
    public string Created { get; set; }
}
