﻿using WFDS.Common.Actor;
using WFDS.Common.Steam;
using WFDS.Common.Types;


namespace WFDS.Server.Core.Actor;

internal sealed class HostSpawnScheduleService(ILogger<HostSpawnScheduleService> logger, IActorManager actor, IActorSpawnManager spawn, SteamManager steam) : IHostedService
{
    private const int DefaultAlienCooldown = 6;
    private const int ResetAlienCooldown = 16;

    private static readonly TimeSpan HostSpawnTimeoutPeriod = TimeSpan.FromSeconds(10);
    private readonly Random _random = new();
    private Timer? _timer;

    private int _alienCooldown = DefaultAlienCooldown;
    private float _rainChance;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _rainChance = _random.NextSingle() * 0.02f;

        _timer = new Timer(DoWork, null, TimeSpan.Zero, HostSpawnTimeoutPeriod);
        return Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        if (_timer != null)
            await _timer.DisposeAsync();
    }

    private void DoWork(object? state)
    {
        if (!steam.Initialized)
        {
            return;
        }
        
        var ownedActorCount = actor.GetOwnedActorCount();
        var ownedActorTypes = actor.GetOwnedActorTypes();

        logger.LogInformation("owned_actors: {OwnedActorCount}, owned_actors_types: {OwnedActorTypes}", ownedActorCount, string.Join(',', ownedActorTypes));

        var type = RandomPickSpawnType();
        IActor? spawnedActor = null;
        switch (type)
        {
            case HostSpawnTypes.Fish:
                spawnedActor = spawn.SpawnFishSpawnActor();
                break;
            case HostSpawnTypes.FishAlien:
                spawnedActor = spawn.SpawnFishSpawnAlienActor();
                break;
            case HostSpawnTypes.Rain:
                spawnedActor = spawn.SpawnRainCloudActor();
                break;
            case HostSpawnTypes.VoidPortal:
                spawnedActor = spawn.SpawnVoidPortalActor();
                break;
            case HostSpawnTypes.None:
            default:
                break;
        }

        if (spawnedActor == null)
        {
            return;
        }

        logger.LogInformation("spawn {ActorType} ({ActorId}) at {ActorPosition}", spawnedActor.Type, spawnedActor.ActorId, spawnedActor.Position);
    }

    private HostSpawnTypes RandomPickSpawnType()
    {
        var type = (HostSpawnTypes)(_random.Next() % 2);

        _alienCooldown -= 1;
        if (_random.NextSingle() <= 0.01 && _random.NextSingle() <= 0.4 && _alienCooldown <= 0)
        {
            type = HostSpawnTypes.FishAlien;
            _alienCooldown = ResetAlienCooldown;
        }

        if (_random.NextSingle() <= _rainChance && _random.NextSingle() <= 0.12f)
        {
            type = HostSpawnTypes.Rain;
            _rainChance = 0f;
        }
        else if (_random.NextSingle() < 0.75f)
        {
            _rainChance += 0.01f;
        }

        if (_random.NextSingle() <= 0.01 && _random.NextSingle() <= 0.25)
        {
            type = HostSpawnTypes.VoidPortal;
        }

        logger.LogDebug("select spawn type: {Type}", type);
        return type;
    }
}