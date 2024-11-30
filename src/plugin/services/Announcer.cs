using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Admin;
using SpecialRounds.api.plugin.services;
using SpecialRounds.plugin.enums;
using SpecialRounds.plugin.extensions;

namespace SpecialRounds.plugin.services;

public class Announcer(SpecialRounds plugin) : IAnnouncer
{
    public void AnnounceAnonymous(CCSPlayerController admin, string local, params object[] args)
    {
        foreach (var player in Utilities.GetPlayers())
            player.PrintLocalizedChat(plugin.GetBase().Localizer, local, player.GetRank() > MAULPermission.Advisor ? $"{admin.PlayerName} " : "", args);
    }
    
    public void Announce(string local, params object[] args)
    {
        foreach (var player in Utilities.GetPlayers())
            player.PrintLocalizedChat(plugin.GetBase().Localizer, local, args);
    }

    public void AnnounceToAdmins(string local, params object[] args)
    {
        foreach (var player in Utilities.GetPlayers())
        {
            if (!AdminManager.PlayerInGroup(player, "#ego/eg")) continue;
            
            player.PrintLocalizedChat(plugin.GetBase().Localizer, local, args);
        }
    }
}