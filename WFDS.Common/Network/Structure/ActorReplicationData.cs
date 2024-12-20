﻿using WFDS.Common.Extensions;
using WFDS.Common.Network;

namespace WFDS.Common.Actor;

public class ActorReplicationData : Packet
{
    public string ActorType { get; set; } = string.Empty;
    public long ActorId { get; set; }
    public long OwnerId { get; set; }
    
    public static ActorReplicationData FromActor(IActor actor)
    {
        return new ActorReplicationData
        {
            ActorType = actor.Type.Name,
            ActorId = actor.ActorId,
            OwnerId = (long)actor.CreatorId.m_SteamID
        };
    }
    
    public override void Deserialize(Dictionary<object, object> data)
    {
        ActorType = data.GetString("type");
        ActorId = data.GetInt("id");
        OwnerId = data.GetInt("owner");
    }

    public override void Serialize(Dictionary<object, object> data)
    {
        data.TryAdd("type", ActorType);
        data.TryAdd("id", ActorId);
        data.TryAdd("owner", OwnerId);
    }
}