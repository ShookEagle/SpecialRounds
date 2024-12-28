using CounterStrikeSharp.API.Core;
using SpecialRounds.api.plugin;

namespace SpecialRounds.plugin.listeners;

public class RoundPreStartListener
{
    private readonly ISpecialRounds plugin;
    
    public RoundPreStartListener(ISpecialRounds plugin)
    {
        this.plugin = plugin;
        
        plugin.GetBase().RegisterEventHandler<EventRoundStart>(OnRoundPreStart);
    }
    
    private HookResult OnRoundPreStart(EventRoundStart @event, GameEventInfo info)
    {
        plugin.GetSpecialRoundService().startSpecialRound();
        
        return HookResult.Continue;
    }
}