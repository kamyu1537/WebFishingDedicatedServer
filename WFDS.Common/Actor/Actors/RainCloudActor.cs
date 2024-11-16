﻿using System.Numerics;
using Steamworks;

namespace WFDS.Common.Actor.Actors;

public sealed class RainCloudActor : IActor
{
    public ActorType Type => ActorType.RainCloud;
    public long ActorId { get; init; }
    public SteamId CreatorId { get; init; }
    public string Zone { get; set; } = "main_zone";
    public long ZoneOwner { get; set; }
    public Vector3 Position { get; set; } = Vector3.Zero;
    public Vector3 Rotation { get; set; } = Vector3.Zero;
    public bool Decay => true;
    public long DecayTimer { get; set; } = 32500;
    public DateTimeOffset CreateTime { get; set; } = DateTimeOffset.UtcNow;
    
    public bool CanWipe => false;
    public bool IsRemoved { get; set; }
    
    public bool IsDead { get; set; } = false;
    public long NetworkShareDefaultCooldown => 8;
    public long NetworkShareCooldown { get; set; }

    public static float Speed => 0.17f;
    public float Direction { get; set; }
}