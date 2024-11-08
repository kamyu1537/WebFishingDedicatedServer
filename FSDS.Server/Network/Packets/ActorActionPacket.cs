﻿using FSDS.Server.Common;
using FSDS.Server.Common.Extensions;

namespace FSDS.Server.Packets;

/*
 * queue_free : 프롭 삭제
 */

public class ActorActionPacket : IPacket
{
    public long ActorId { get; private set; }
    public string Action { get; private set; } = string.Empty;
    public List<object> Params { get; private set; } = [];

    public void Parse(Dictionary<object, object> data)
    {
        ActorId = data.GetNumber("actor_id");
        Action = data.GetString("action");
        Params = data.GetObjectList("params");
    }

    public Dictionary<object, object> ToDictionary()
    {
        return new Dictionary<object, object>
        {
            ["type"] = "actor_action",
            ["actor_id"] = ActorId,
            ["action"] = Action,
            ["params"] = Params
        };
    }
    
    public static ActorActionPacket CreateWipeActorPacket(long actorId)
    {
        return new ActorActionPacket
        {
            ActorId = actorId,
            Action = "_wipe_actor",
            Params = []
        };
    }

    public static ActorActionPacket CreateQueueFreePacket(long actorId)
    {
        return new ActorActionPacket
        {
            ActorId = actorId,
            Action = "queue_free",
            Params = []
        };
    }
}