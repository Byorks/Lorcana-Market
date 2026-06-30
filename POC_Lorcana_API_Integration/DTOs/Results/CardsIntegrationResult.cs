using System.Text.Json.Serialization;

namespace POC_Lorcana_API_Integration.DTOs.Results;

public class CardsIntegrationResult
{
    public List<CardIntegrationResult> Cards { get; set; } = new();

    public CardsIntegrationResult()
    {
    }

    public CardsIntegrationResult(List<CardIntegrationResult> cards)
    {
        Cards = cards;
    }
}

public class CardIntegrationResult
{
    [JsonPropertyName("Artist")]
    public string Artist { get; set; }

    [JsonPropertyName("Set_Name")]
    public string SetName { get; set; }

    [JsonPropertyName("Classifications")]
    public string Classifications { get; set; }

    [JsonPropertyName("Date_Added")]
    public string DateAdded { get; set; }

    [JsonPropertyName("Set_Num")]
    public int SetNum { get; set; }

    [JsonPropertyName("Color")]
    public string Color { get; set; }

    [JsonPropertyName("Gamemode")]
    public string Gamemode { get; set; }

    [JsonPropertyName("Franchise")]
    public string Franchise { get; set; }

    [JsonPropertyName("Image")]
    public string ImageUrl { get; set; }

    [JsonPropertyName("Cost")]
    public int Cost { get; set; }

    [JsonPropertyName("Inkable")]
    public bool Inkable { get; set; }

    [JsonPropertyName("Name")]
    public string Name { get; set; }

    [JsonPropertyName("Type")]
    public string Type { get; set; }

    [JsonPropertyName("Lore")]
    public int Lore { get; set; }

    [JsonPropertyName("Rarity")]
    public string Rarity { get; set; }

    [JsonPropertyName("Flavor_Text")]
    public string FlavorText { get; set; }

    [JsonPropertyName("Unique_ID")]
    public string UniqueId { get; set; }

    [JsonPropertyName("Card_Num")]
    public int CardNum { get; set; }

    [JsonPropertyName("Body_Text")]
    public string BodyText { get; set; }

    [JsonPropertyName("Willpower")]
    public int Willpower { get; set; }

    [JsonPropertyName("Date_Modified")]
    public string DateModified { get; set; }

    [JsonPropertyName("Strength")]
    public int Strength { get; set; }

    [JsonPropertyName("Set_ID")]
    public string SetId { get; set; }
}
