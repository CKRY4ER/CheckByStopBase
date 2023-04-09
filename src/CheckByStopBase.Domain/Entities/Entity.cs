using System.Text.Json.Serialization;

namespace CheckByStopBase.Domain.Entities;

public class Entity
{
    [JsonIgnore]
    public long Id { get; set; }
}