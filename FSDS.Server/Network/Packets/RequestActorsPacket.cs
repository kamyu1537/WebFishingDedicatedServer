﻿using FSDS.Server.Common;
using FSDS.Server.Common.Extensions;

namespace FSDS.Server.Packets;

public class RequestActorsPacket : IPacket
{
    public string UserId { get; set; } = string.Empty;
    
    public void Parse(Dictionary<object, object> data)
    {
        UserId = data.GetString("user_id");
    }

    public Dictionary<object, object> ToDictionary()
    {
        return new Dictionary<object, object>
        {
            ["type"] = "request_actors",
            ["user_id"] = UserId
        };
    }
}