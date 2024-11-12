﻿using WFDS.Common.Types;
using WFDS.Server.Network;
using WFDS.Server.Packets;

namespace WFDS.Server.Handlers;

[PacketType("handshake")]
public class HandshakeHandler : PacketHandler<HandshakePacket>
{
    protected override void HandlePacket(ISession sender, NetChannel channel, HandshakePacket packet)
    {
        Logger.LogInformation("received handshake from {Sender} : {UserId}", sender.Friend, packet.UserId);

        sender.HandshakeReceived = true;
        sender.HandshakeReceiveTime = DateTimeOffset.UtcNow;
    }
}