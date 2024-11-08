﻿using FSDS.Server.Common;
using FSDS.Server.Packets;
using Steamworks;

namespace FSDS.Server.Handlers;

[PacketType("letter_recieved")]
public class LetterReceivedHandler : PacketHandler
{
    public override void HandlePacket(Session sender, NetChannel channel, Dictionary<object, object> data)
    {
        var packet = new LetterReceivedPacket();
        packet.Parse(data);

        if (packet.To != SteamClient.SteamId.Value.ToString()) 
            return;
        
        Logger.LogInformation("received letter from {Sender} ({From} -> {To}) on channel {Channel} / {Header}: {Body} - {Closing} {User}", sender.SteamId, packet.From, packet.To, channel, packet.Header, packet.Body, packet.Closing, packet.User);
        
        packet.LatterId = new Random().Next();
        (packet.From, packet.To) = (packet.To, packet.From);

        sender.Send(NetChannel.GameState, packet);
    }
}