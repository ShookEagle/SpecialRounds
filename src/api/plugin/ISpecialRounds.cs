using CounterStrikeSharp.API.Core;
using SpecialRounds.api.plugin.services;
using SpecialRounds.plugin;

namespace SpecialRounds.api.plugin;

public interface ISpecialRounds
{
    BasePlugin GetBase();
    void LogLocalizedServerMessage(string local, params object[] args);
    IAnnouncer GetAnnouncer();
    SpecialRoundsConfig GetConfig();
    ISpecialRoundService GetSpecialRoundService();
}