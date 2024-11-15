﻿using Steamworks;
using WFDS.Common.Network;
using WFDS.Common.Types;
using WFDS.Common.Types.Manager;
using WFDS.Common.Network.Packets;
using ISession = WFDS.Common.Types.ISession;

namespace WFDS.Server.PacketHandlers;

[PacketType("new_player_join")]
public class NewPlayerJoinHandler(ILogger<NewPlayerJoinHandler> logger, ISessionManager sessionManager) : PacketHandler<NewPlayerJoinPacket>
{
    protected override async Task HandlePacketAsync(ISession sender, NetChannel channel, NewPlayerJoinPacket packet)
    {
        logger.LogDebug("received new_player_join from {Sender} on channel {Channel}", sender.SteamId, channel);
        
        // request actors
        sessionManager.BroadcastP2PPacket(NetChannel.GameState, new RequestActorsPacket
        {
            UserId = SteamClient.SteamId.Value.ToString()
        });
        await Task.Yield();
    }
}