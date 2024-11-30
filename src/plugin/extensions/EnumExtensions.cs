using SpecialRounds.plugin.enums;

namespace SpecialRounds.plugin.extensions;

public static class EnumExtensions
{
    public static string ToFriendlyString(this SpecialRoundTypes roundType)
    {
        switch (roundType)
        {
            case SpecialRoundTypes.Regular: return "Regular";
            case SpecialRoundTypes.Speed: return "Speed";
            case SpecialRoundTypes.AutoBhop: return "Auto Bhop";
            case SpecialRoundTypes.LowGrav: return "Low Gravity";
            case SpecialRoundTypes.HeadshotOnly: return "Headshots Only";
            case SpecialRoundTypes.KnifeOnly: return "Knife Only";
            case SpecialRoundTypes.NoScope: return "No-Scope";
            default: return "Unknown";
        }
    }
}