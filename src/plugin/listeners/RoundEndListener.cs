using CounterStrikeSharp.API.Core;
using SpecialRounds.api.plugin;

namespace SpecialRounds.plugin.listeners;

public class RoundEndListener
{
    private readonly ISpecialRounds plugin;
    private Random random = new Random();

    public RoundEndListener(ISpecialRounds plugin)
    {
        this.plugin = plugin;
        
        plugin.GetBase().RegisterEventHandler<EventRoundEnd>(OnRoundEnd);
    }

    private HookResult OnRoundEnd(EventRoundEnd @event, GameEventInfo info)
    {
        plugin.GetSpecialRoundService().resetSettings();
        
        int chance = random.Next(1, 101);
        int successRate = plugin.GetConfig().SpecialRoundChance;
        plugin.LogLocalizedServerMessage("server_rng_generated", chance, successRate);

        if (chance >= successRate) return HookResult.Continue;
        
        plugin.GetSpecialRoundService().generateSpecialRound();

        return HookResult.Continue;
    }
}