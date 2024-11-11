﻿using WFDS.Common.Types;
using WFDS.Server.Network;
using WFDS.Server.Packets;

namespace WFDS.Server.Handlers;

[PacketType("request_actors")]
public class RequestActorsHandler : PacketHandler
{
    public override void HandlePacket(ISession sender, NetChannel channel, Dictionary<object, object> data)
    {
        Logger.LogInformation("received request_actors from {Sender} on channel {Channel}", sender.SteamId, channel);

        var packet = new ActorRequestSendPacket();
        ActorManager?.SelectOwnedActors(actor => packet.Actors.Add(ActorReplicationData.FromActor(actor)));
        sender.SendP2PPacket(NetChannel.GameState, packet);
        
        // update all actors
        ActorManager?.SelectOwnedActors(actor => actor.SendUpdatePacket(sender));
    }
}