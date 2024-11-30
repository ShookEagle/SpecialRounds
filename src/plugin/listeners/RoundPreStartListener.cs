using CounterStrikeSharp.API.Core;
using SpecialRounds.api.plugin;

namespace SpecialRounds.plugin.listeners;

public class RoundPreStartListener
{
    private readonly ISpecialRounds plugin;
    
    public RoundPreStartListener(ISpecialRounds plugin)
    {
        this.plugin = plugin;
        
        plugin.GetBase().RegisterEventHandler<EventRoundPrestart>(OnRoundPreStart);
    }
    
    private HookResult OnRoundPreStart(EventRoundPrestart @event, GameEventInfo info)
    {
        plugin.GetSpecialRoundService().startSpecialRound();
        
        return HookResult.Continue;
    }
}