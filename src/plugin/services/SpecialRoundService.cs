using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using SpecialRounds.api.plugin;
using SpecialRounds.api.plugin.services;
using SpecialRounds.plugin.enums;
using SpecialRounds.plugin.extensions;
using SpecialRounds.plugin.listeners;

namespace SpecialRounds.plugin.services;

public class SpecialRoundService(SpecialRounds plugin) : ISpecialRoundService
{
    private SpecialRoundTypes RoundType = SpecialRoundTypes.Regular;
    private Random random = new Random();
    

    public void generateSpecialRound()
    {
        var values = Enum.GetValues(typeof(SpecialRoundTypes))
            .Cast<SpecialRoundTypes>()
            .Where(type => type != SpecialRoundTypes.Regular)
            .ToArray();

        RoundType = values[random.Next(values.Length)];

        plugin.GetAnnouncer().Announce("SR_round_next", RoundType.ToFriendlyString());
    }

    public void resetSettings()
    {
        RoundType = SpecialRoundTypes.Regular;
        Server.ExecuteCommand("sv_autobunnyhopping 0;sv_enablebunnyhopping 0;sv_gravity 800");
        
        var allPlayers = Utilities.GetPlayers().Where(p => p.IsReal());

        foreach (var player in allPlayers)
        {
            player.PlayerPawn.Value!.VelocityModifier = 1.0f;
        }
        plugin.GetBase().RemoveListener<Listeners.OnTick>(OnTick);
        //plugin.GetBase().DeregisterEventHandler<EventPlayerHurt>(OnPlayerHurt);
    }

    public void startSpecialRound()
    {
        plugin.LogLocalizedServerMessage("test");
        if (RoundType == SpecialRoundTypes.Regular) return;

        switch (RoundType)
        {
            case SpecialRoundTypes.AutoBhop: StartAutoBhop(); break;
            case SpecialRoundTypes.Speed: StartSpeed(); break;
            case SpecialRoundTypes.LowGrav: StartLowGrav(); break;
            //case SpecialRoundTypes.HeadshotOnly: StartHeadshotOnly(); break;
            case SpecialRoundTypes.KnifeOnly: StartKnifeOnly(); break;
            case SpecialRoundTypes.NoScope: StartNoScope(); break;
        }
    }

    private void StartAutoBhop()
    {
        Server.ExecuteCommand("sv_autobunnyhopping 1;sv_enablebunnyhopping 1");
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
    
    /*private void StartHeadshotOnly()
    {
        foreach (var player in Utilities.GetPlayers())
        {
            player.RemoveWeapons();
            player.GiveNamedItem("weapon_deagle");
            
            plugin.GetBase().RegisterEventHandler<EventPlayerHurt>(OnPlayerHurt);
        }
    }*/

    private void StartKnifeOnly()
    {
        foreach (var player in Utilities.GetPlayers())
        {
            player.RemoveWeapons();
            player.GiveNamedItem("weapon_knife");
        }
    }
    
    private void StartNoScope()
    {
        plugin.GetBase().RegisterListener<Listeners.OnTick>(OnTick);
    }
    
    
    //Event Handlers
    private HookResult OnPlayerHurt(EventPlayerHurt @event, GameEventInfo info)
    {
        var attacker = @event.Attacker;
        var victim = @event.Userid;
        if (victim == null || attacker == null) return HookResult.Continue;
        
        var attackerPawn = attacker.PlayerPawn.Value;
        var victimPawn = victim.PlayerPawn.Value;
        if (attackerPawn == null || victimPawn == null) return HookResult.Continue;
        
        if (@event.Hitgroup != 1)
        {
            @event.DmgHealth = 0;
            @event.DmgArmor = 0;
        }
        return HookResult.Continue;
    }
    private void OnTick()
    {
        foreach (var player in Utilities.GetPlayers().Where(p => p.PawnIsAlive))
        {
            if (!player.IsReal()) return;
            var pawn = player.PlayerPawn.Value;
            if (pawn == null || !pawn.IsValid) return;
            var weaponServices = pawn.WeaponServices;
            if (weaponServices == null) return;
            var activeWeapon = weaponServices.ActiveWeapon.Value;
            if (activeWeapon == null || !activeWeapon.IsValid) return;
            activeWeapon.NextSecondaryAttackTick = Server.TickCount + 500;
        }
    }
}