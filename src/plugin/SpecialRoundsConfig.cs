using System.Text.Json.Serialization;
using CounterStrikeSharp.API.Core;

namespace SpecialRounds.plugin;

public class SpecialRoundsConfig : BasePluginConfig
{
    [JsonPropertyName("SpecialRoundChance")] public int SpecialRoundChance { get; set; } = 10;
    [JsonPropertyName("AdminsCanForceSR")] public bool AdminsCanForceSR { get; set; } = false;
    [JsonPropertyName("SpeedRoundMultiplier")] public float SpeedRoundMultiplier { get; set; } = 2.0f;
}