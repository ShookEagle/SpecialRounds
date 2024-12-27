using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Admin;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using SpecialRounds.plugin.enums;
using SpecialRounds.plugin.utils;

namespace SpecialRounds.plugin.extensions;

public static class PlayerExtensions
{
    public static bool IsReal(this CCSPlayerController? player)
    {
        if (player == null) return false;
        if (!player.IsValid) return false;

        if (player.Connected != PlayerConnectedState.PlayerConnected) return false;

        if (player.IsHLTV) return false;

        return true;
    }
    
    public static bool CanTarget(this CCSPlayerController controller,
        CCSPlayerController target) {
        if (target.IsBot) return true;
        return AdminManager.CanPlayerTarget(controller, target);
    }

    public static MAULPermission GetRank(this CCSPlayerController player) {
        if (!player.IsReal()) return MAULPermission.None;
        if (AdminManager.PlayerInGroup(player, "#ego/root")
            || AdminManager.PlayerHasPermissions(player, "@css/root"))
            return MAULPermission.Root;
        if (AdminManager.PlayerInGroup(player, "#ego/executive"))
            return MAULPermission.Executive;
        if (AdminManager.PlayerInGroup(player, "#ego/directory"))
            return MAULPermission.Director;
        if (AdminManager.PlayerInGroup(player, "#ego/commgr"))
            return MAULPermission.CommunityManager;
        if (AdminManager.PlayerInGroup(player, "#ego/srmanager"))
            return MAULPermission.SeniorManager;
        if (AdminManager.PlayerInGroup(player, "#ego/manager"))
            return MAULPermission.Manager;
        if (AdminManager.PlayerInGroup(player, "#ego/advisor"))
            return MAULPermission.Advisor;
        if (AdminManager.PlayerInGroup(player, "#ego/ego"))
            return MAULPermission.EGO;
        if (AdminManager.PlayerInGroup(player, "#ego/eg")) return MAULPermission.EG;
        if (AdminManager.PlayerInGroup(player, "#ego/e")) return MAULPermission.E;
        return MAULPermission.None;
    }

    public static bool IsDedicatedSupporter(this CCSPlayerController player) {
        if (!player.IsReal()) return false;
        return AdminManager.PlayerHasPermissions(player, "@ego/ds");
    }

    public static int GetDSTier(this CCSPlayerController player) {
        if (!player.IsReal() || !player.IsDedicatedSupporter()) return 0;
        if (AdminManager.PlayerHasPermissions(player, "@ego/dsroyal")) return 4;
        if (AdminManager.PlayerHasPermissions(player, "@ego/dsplatinum")) return 3;
        if (AdminManager.PlayerHasPermissions(player, "@ego/dsgold")) return 2;
        if (AdminManager.PlayerHasPermissions(player, "@ego/dssilver")) return 1;

        return 0;
    }

    public static void PrintLocalizedChat(this CCSPlayerController? controller, IStringLocalizer localizer, string local,
        params object[] args)
    {
        if (controller == null || !controller.IsReal()) return;
        string message = localizer[local, args];
        message = message.Replace("%admin%", localizer["admin"]);
        message = message.Replace("%prefix%", localizer["prefix"]);
        message = StringUtils.ReplaceChatColors(message);
        controller.PrintToChat(message);
    }

    public static void PrintLocalizedConsole(this CCSPlayerController? controller, IStringLocalizer localizer,
        string local, params object[] args)
    {
        if (controller == null || !controller.IsReal()) return;
        string message = localizer[local, args];
        message = message.Replace("%prefix%", localizer["prefix"]);
        message = StringUtils.ReplaceChatColors(message);
        controller.PrintToConsole(message);
    }
    
    public static void PrintLocalizedError(this CCSPlayerController? controller, IStringLocalizer localizer, BasePlugin pluginBase, string local,
        params object[] args)
    {
        if (controller == null || !controller.IsReal()) return;
        string message = localizer[local, args];
        string serverMessage = message.Replace("%error%", "");
        message = message.Replace("%error%", localizer["error"]);
        message = message.Replace("%prefix%", localizer["prefix"]);
        message = StringUtils.ReplaceChatColors(message);
        controller.PrintToChat(message);
        
        pluginBase.Logger.LogError(serverMessage);
    }
}