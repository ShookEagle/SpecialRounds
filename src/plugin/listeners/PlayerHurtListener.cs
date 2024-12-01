using CounterStrikeSharp.API.Core;
using SpecialRounds.api.plugin;

namespace SpecialRounds.plugin.listeners;

public class PlayerHurtListener
{
    private readonly ISpecialRounds plugin;

    public PlayerHurtListener(ISpecialRounds plugin)
    {
        this.plugin = plugin;
        
        plugin.GetBase().RegisterEventHandler<EventPlayerHurt>(OnPlayerHurt, HookMode.Pre);
    }

    private HookResult OnPlayerHurt(EventPlayerHurt @event, GameEventInfo info)
    {
        return HookResult.Handled;
    }
}