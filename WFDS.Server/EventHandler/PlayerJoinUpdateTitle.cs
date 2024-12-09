﻿using WFDS.Common.GameEvents;
using WFDS.Common.GameEvents.Events;
using WFDS.Common.Helpers;
using WFDS.Common.Steam;

namespace WFDS.Server.EventHandler;

public sealed class PlayerJoinUpdateTitle(LobbyManager lobby, SessionManager session) : GameEventHandler<PlayerJoinedEvent>
{
    protected override void Handle(PlayerJoinedEvent e)
    {
        ConsoleHelper.UpdateConsoleTitle(lobby.GetName(), lobby.GetCode(), session.GetSessionCount(), lobby.GetCap());
    }
}