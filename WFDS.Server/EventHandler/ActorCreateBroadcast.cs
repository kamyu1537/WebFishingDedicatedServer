﻿using WFDS.Common.Actor;
using WFDS.Common.GameEvents;
using WFDS.Common.GameEvents.Events;
using WFDS.Common.Network.Packets;
using WFDS.Common.Steam;
using WFDS.Common.Types;

namespace WFDS.Server.EventHandler;

public sealed class ActorCreateBroadcast(IActorManager actorManager, SessionManager sessionManager, LobbyManager lobby, SteamManager steam) : GameEventHandler<ActorCreateEvent>
{
    protected override void Handle(ActorCreateEvent e)
    {
        var actor = actorManager.GetActor(e.ActorId);
        if (actor == null) return;

        if (actor.CreatorId == steam.SteamId)
        {
            sessionManager.BroadcastP2PPacket(lobby.GetLobbyId(), NetChannel.GameState, InstanceActorPacket.Create(actor));
            sessionManager.BroadcastP2PPacket(lobby.GetLobbyId(), NetChannel.ActorAction, ActorActionPacket.CreateSetZonePacket(actor.ActorId, actor.Zone, actor.ZoneOwner));
            sessionManager.BroadcastP2PPacket(lobby.GetLobbyId(), NetChannel.ActorUpdate, ActorUpdatePacket.Create(actor));
        }
    }
}