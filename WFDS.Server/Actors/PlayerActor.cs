﻿using Steamworks;
using WFDS.Common.Types;
using WFDS.Common.Types.Manager;
using WFDS.Godot.Types;

namespace WFDS.Server.Actors;

public sealed class PlayerActor : IPlayerActor
{
    public ILogger? Logger { get; set; }
    public ISessionManager? SessionManager { get; set; }
    
    public string ActorType => "player";
    public long ActorId { get; init; }
    public SteamId CreatorId { get; init; }
    public string Zone { get; set; } = "main_zone";
    public long ZoneOwner { get; set; }
    public Vector3 Position { get; set; } = Vector3.Zero;
    public Vector3 Rotation { get; set; } = Vector3.Zero;
    public bool Decay => false;
    public long DecayTimer { get; set; }
    public DateTimeOffset CreateTime { get; set; } = DateTimeOffset.UtcNow;
    public bool IsDeadActor { get; set; } = true;
    public long ActorUpdateDefaultCooldown => 0;
    public long ActorUpdateCooldown { get; set; }
    
    public GameItem HeldItem { get; set; } = GameItem.CreateDefault();
    public Cosmetics Cosmetics { get; set; } = Cosmetics.CreateDefault();

    public void OnCreated()
    {
    }
    
    public void OnRemoved(ActorRemoveTypes type)
    {
    }

    public void OnUpdate(double delta)
    {
    }

    public void OnZoneUpdated(string zone, long zoneOwner)
    {
        Zone = zone;
        ZoneOwner = zoneOwner;
    }
    
    public void OnActorUpdated(Vector3 position, Vector3 rotation)
    {
        Position = position;
        Rotation = rotation;
    }

    public void Dispose()
    {
        Logger = null;
        SessionManager = null;
    }
    
    public void OnCosmeticsUpdated(Cosmetics cosmetics)
    {
        Cosmetics = cosmetics;
    }
    
    public void OnHeldItemUpdated(GameItem item)
    {
        HeldItem = item;
    }
    
    public void OnLevelUp()
    {
    }

    public void OnMessage(string message, string color, bool local, Vector3 position, string zone, long zoneOwner)
    {
    }
    
    public void OnChatMessage(string message)
    {
    }
}