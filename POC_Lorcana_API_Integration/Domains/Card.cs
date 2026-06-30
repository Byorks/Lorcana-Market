namespace POC_Lorcana_API_Integration.Domains;

public class Card
{
    public long Id { get; set; }

    public string Artist { get; set; }

    public string SetName { get; set; }

    public string? Classifications { get; set; }

    public DateTime DateAdded { get; set; }

    public int SetNum { get; set; }

    public string Color { get; set; }

    public string? Gamemode { get; set; }

    public string Franchise { get; set; }

    public string ImageUrl { get; set; }

    public string ImageBase64 { get; set; } // Para caso a API que foi consumida para adquirir os dados cair, é possível utilizar a imagem salva no banco de dados como base64

    public int Cost { get; set; }

    public bool Inkable { get; set; }

    public string Name { get; set; }

    public string Type { get; set; }

    public int Lore { get; set; }

    public string Rarity { get; set; }

    public string? FlavorText { get; set; }

    public string UniqueId { get; set; }

    public int CardNum { get; set; }

    public string? BodyText { get; set; }

    public int Willpower { get; set; }

    public DateTime DateModified { get; set; }

    public int Strength { get; set; }

    public string SetId { get; set; }

    public Card(string artist, string setName, string? classifications, DateTime dateAdded, int setNum, string color, string? gamemode, string franchise, string imageUrl, string imageBase64, int cost, bool inkable, string name, string type, int lore, string rarity, string? flavorText, string uniqueId, int cardNum, string? bodyText, int willpower, DateTime dateModified, int strength, string setId)
    {
        Artist = artist;
        SetName = setName;
        Classifications = classifications;
        DateAdded = dateAdded;
        SetNum = setNum;
        Color = color;
        Gamemode = gamemode;
        Franchise = franchise;
        ImageUrl = imageUrl;
        ImageBase64 = imageBase64;
        Cost = cost;
        Inkable = inkable;
        Name = name;
        Type = type;
        Lore = lore;
        Rarity = rarity;
        FlavorText = flavorText;
        UniqueId = uniqueId;
        CardNum = cardNum;
        BodyText = bodyText;
        Willpower = willpower;
        DateModified = dateModified;
        Strength = strength;
        SetId = setId;
    }
}
