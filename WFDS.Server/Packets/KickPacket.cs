﻿using WFDS.Common.Types;

namespace WFDS.Server.Packets;

public record KickPacket : IPacket
{
    public void Deserialize(Dictionary<object, object> data)
    {
    }

    public void Serialize(Dictionary<object, object> data)
    {
        data.TryAdd("type", "kick");
    }
}