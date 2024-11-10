﻿using System.Collections.Concurrent;
using Steamworks;
using WFDS.Server.Common;
using WFDS.Server.Common.Actor;
using WFDS.Server.Managers;

namespace WFDS.Server.Network;

public interface ISession
{
    LobbyManager LobbyManager { get; set; }
    ILogger Logger { get; set; }

    bool Disposed { get; set; }

    Friend Friend { get; set; }
    SteamId SteamId { get; set; }

    PlayerActor Actor { get; set; }
    bool ActorCreated { get; set; }

    bool HandshakeReceived { get; set; }
    DateTimeOffset ConnectTime { get; set; }
    DateTimeOffset HandshakeReceiveTime { get; set; }
    DateTimeOffset PingReceiveTime { get; set; }
    DateTimeOffset PacketReceiveTime { get; set; }

    ConcurrentQueue<(NetChannel, byte[])> Packets { get; }
}