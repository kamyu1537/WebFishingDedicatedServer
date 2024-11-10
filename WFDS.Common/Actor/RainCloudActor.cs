﻿using Steamworks;
using WFDS.Godot.Types;

namespace WFDS.Server.Common.Actor;

public class RainCloudActor : IActor
{
    public string ActorType => "raincloud";
    public long ActorId { get; init; }
    public SteamId CreatorId { get; init; }
    public string Zone { get; set; } = "main_zone";
    public long ZoneOwner { get; set; }
    public Vector3 Position { get; set; } = Vector3.Zero;
    public Vector3 Rotation { get; set; } = Vector3.Zero;
    public bool Decay => true;
    public long DecayTimer { get; set; } = 32500;
    public DateTimeOffset CreateTime { get; set; } = DateTimeOffset.UtcNow;
    public bool IsDeadActor { get; set; } = false;
    public long ActorUpdateDefaultCooldown => 8;
    public long ActorUpdateCooldown { get; set; }
    
    private const float Speed = 0.17f;
    private float _direction;

    public void OnCreated()
    {
        CreateTime = DateTimeOffset.UtcNow;
        
        var center = (Position - new Vector3(30, 40, -50)).Normalized();
        _direction = new Vector2(center.X, center.Z).Angle();
    }
    
    public void OnRemoved()
    {
    }

    public void OnUpdate(double delta)
    {
        var vel = new Vector2(1, 0).Rotate(_direction) * Speed;
        Position += new Vector3(vel.X, 0f, vel.X) * (float)delta;
    }
}