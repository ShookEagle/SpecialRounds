using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using Microsoft.Extensions.Logging;
using SpecialRounds.api.plugin;
using SpecialRounds.api.plugin.services;
using SpecialRounds.plugin.commands;
using SpecialRounds.plugin.services;

namespace SpecialRounds.plugin;

public class SpecialRounds : BasePlugin, IPluginConfig<SpecialRoundsConfig>, IPlugin
{
    private readonly Dictionary<string, Command> commands = new();
    public override string ModuleName => "SpecialRounds";
    public override string ModuleVersion => "1.0.0";
    public override string ModuleAuthor => "ShookEagle";
    public override string ModuleDescription => "Special Rounds Plugin for the EdgeGamers AWP Server";
    
    private IAnnouncer? _announcer;
    private ISpecialRoundService _specialRoundService;
    public SpecialRoundsConfig Config { get; set; } = new();
    public void OnConfigParsed(SpecialRoundsConfig config)
    {
       Config = config;
    }

    public BasePlugin GetBase()
    {
        return this;
    }

    public SpecialRoundsConfig GetConfig()
    {
        return Config;
    }

    public void LogLocalizedServerMessage(string local, params object[] args)
    {
        string message = Localizer[local, args];
        Logger.LogInformation(message);
    }
    
    public IAnnouncer GetAnnouncer() { return _announcer!; }
    public ISpecialRoundService GetSpecialRoundService() { return _specialRoundService!; }
    
    public override void Load(bool hotReload)
    {
        _announcer = new Announcer(this);
        _specialRoundService = new SpecialRoundService(this);
        
        LoadCommands();
    }
    
    private void LoadCommands()
    {
        foreach (var command in commands)
            AddCommand(command.Key, command.Value.Description ?? "No Description Provided", command.Value.OnCommand);
    }
}