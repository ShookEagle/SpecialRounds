using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Commands;
using SpecialRounds.api.plugin;
using SpecialRounds.plugin.extensions;

namespace SpecialRounds.plugin.commands;

public abstract class Command(ISpecialRounds plugin)
{
    public string? Description => null;
    public abstract void OnCommand(CCSPlayerController? executor, CommandInfo info);
    internal CCSPlayerController? GetSingleTarget(CommandInfo command, int argIndex = 1,
        bool print = true) {
        var matches = command.GetArgTargetResult(argIndex);

        if (!matches.Any()) {
            if (print)
                command.ReplyLocalized(plugin.GetBase().Localizer, "player_not_found",
                    command.GetArg(argIndex));
            return null;
        }

        if (matches.Count() > 1) {
            if (print)
                command.ReplyLocalized(plugin.GetBase().Localizer,
                    "player_found_multiple", command.GetArg(argIndex));
            return null;
        }

        var target = matches.Players.FirstOrDefault(p => p == command.CallingPlayer);
        if (target == null)
        {
            command.ReplyLocalized(plugin.GetBase().Localizer, "error_player_null");
            return null;
        }

        return matches.Players.FirstOrDefault(p => p== command.CallingPlayer);
    }
    
}