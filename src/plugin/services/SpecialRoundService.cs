using CounterStrikeSharp.API;
using SpecialRounds.api.plugin.services;
using SpecialRounds.plugin.enums;
using SpecialRounds.plugin.extensions;

namespace SpecialRounds.plugin.services;

public class SpecialRoundService(SpecialRounds plugin) : ISpecialRoundService
{
    private SpecialRoundTypes RoundType = SpecialRoundTypes.Regular;
    private Random random = new Random();

    public SpecialRoundTypes GetCurrentRoundType()
    {
        return RoundType;
    }

    public void generateSpecialRound()
    {
        var count = Enum.GetNames(typeof(SpecialRoundTypes)).Length;

        RoundType = (SpecialRoundTypes)random.Next(2, count);

        plugin.GetAnnouncer().Announce("SR_round_next", RoundType.ToFriendlyString());
    }

    public void resetSettings()
    {
        RoundType = SpecialRoundTypes.Regular;
        Server.ExecuteCommand("sv_autobunnyhopping 0;sv_enablebunnyhopping 0");
        
        var allPlayers = Utilities.GetPlayers().Where(p => p.IsReal());

        foreach (var player in allPlayers)
        {
            player.PlayerPawn.Value!.VelocityModifier = 1.0f;
        }
    }

    public void startSpecialRound()
    {
        if (RoundType == SpecialRoundTypes.Regular) return;

        switch (RoundType)
        {
            case SpecialRoundTypes.AutoBhop: StartAutoBhop(); break;
            case SpecialRoundTypes.Speed: StartSpeed(); break;
            case SpecialRoundTypes.LowGrav: StartLowGrav(); break;
            case SpecialRoundTypes.HeadshotOnly: StartHeadshotOnly(); break;
            case SpecialRoundTypes.KnifeOnly: StartKnifeOnly(); break;
            case SpecialRoundTypes.NoScope: StartNoScope(); break;
        }
    }

    private void StartAutoBhop()
    {
        Server.ExecuteCommand("sv_autobunnyhopping 1;sv_enablebunnyhopping 1;sv_gravity 800");
    }
    
    private void StartSpeed()
    {
        var allPlayers = Utilities.GetPlayers().Where(p => p.IsReal());

        foreach (var player in allPlayers)
        {
            player.PlayerPawn.Value!.VelocityModifier = plugin.GetConfig().SpeedRoundMultiplier;
        }
    }
    
    private void StartLowGrav()
    {
        Server.ExecuteCommand("sv_gravity 400");
    }
    
    private void StartHeadshotOnly()
    {
        
    }
    
    private void StartKnifeOnly()
    {
        
    }
    
    private void StartNoScope()
    {
        
    }
}