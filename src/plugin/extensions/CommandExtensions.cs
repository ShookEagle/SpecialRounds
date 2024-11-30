using CounterStrikeSharp.API.Modules.Commands;
using Microsoft.Extensions.Localization;
using SpecialRounds.plugin.utils;

namespace SpecialRounds.plugin.extensions;

public static class CommandExtensions
{
    public static void ReplyLocalized(this CommandInfo cmd, IStringLocalizer localizer, string local,
        params object[] args)
    {
        string message = localizer[local, args];
        message = message.Replace("%prefix%", localizer["prefix"]);
        message = StringUtils.ReplaceChatColors(message);
        cmd.CallingPlayer?.PrintToChat(message);
    }
}