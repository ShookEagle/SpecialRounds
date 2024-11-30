using CounterStrikeSharp.API.Core;

namespace SpecialRounds.api.plugin.services;

public interface IAnnouncer
{
    void AnnounceAnonymous(CCSPlayerController admin, string local, params object[] args);
    void Announce(string local, params object[] args);
    void AnnounceToAdmins(string local, params object[] args);
}