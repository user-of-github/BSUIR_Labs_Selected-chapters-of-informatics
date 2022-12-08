using System.Text.Json.Serialization;


namespace BlazorApp2.Shared
{
  public class DetailViewModel
  {
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("categoryId")]
    public int? CategoryId { get; set; }

    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("duration")]
    public float? Duration { get; set; }

    [JsonPropertyName("imagePath")]
    public string? ImagePath { get; set; }

    [JsonPropertyName("mimeType")]
    public string? MimeType { get; set; }
  }
}