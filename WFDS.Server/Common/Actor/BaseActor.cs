﻿using Steamworks;
using WFDS.Godot.Types;

namespace WFDS.Server.Common.Actor;

public class BaseActor : IActor
{
    public string ActorType { get; set; } = string.Empty;
    public long ActorId { get; set; }
    public SteamId CreatorId { get; set; }
    public string Zone { get; set; } = string.Empty;
    public long ZoneOwner { get; set; } = -1;
    public Vector3 Position { get; set; } = Vector3.Zero;
    public Vector3 Rotation { get; set; } = Vector3.Zero;
    public bool Decay { get; set; }
    public long DecayTimer { get; set; } = 600;
    public DateTimeOffset CreateTime { get; set; } = DateTimeOffset.UtcNow;

    public bool IsDeadActor { get; set; }
    public long ActorUpdateDefaultCooldown { get; set; } = 32;
    public long ActorUpdateCooldown { get; set; }
    

    public virtual void OnCreated()
    {
    }

    public virtual void OnUpdate(double delta)
    {
    }
}