using System.Text.Json.Serialization;

namespace Booky.Domain.Dtos.Base;

public class HeaderParameter
{
    [JsonIgnore]
    public string? Token { get; set; }
    
    [JsonIgnore]
    public string DeviceId { get; set; }
    
    [JsonIgnore]
    public string UserId { get; set; }
    
    [JsonIgnore]
    public string? AcceptLanguage { get; set; }
}