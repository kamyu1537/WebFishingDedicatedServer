﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Steamworks;
using WFDS.Common.Steam;

namespace WFDS.Server.Pages.Session;

public class Index(SessionManager sessionManager, LobbyManager lobbyManager) : PageModel
{
    public int MaxSessionCount { get; } = lobbyManager.GetCap();
    public int SessionCount { get; } = sessionManager.GetSessionCount();
    public IEnumerable<Common.Network.Session> GetSessions() => sessionManager.GetSessions();
    

    public void OnGet()
    {
        ViewData["Title"] = "Sessions";
    }

    public IActionResult OnPostKick(string steamId)
    {
        if (!ulong.TryParse(steamId, out var value))
        {
            return RedirectToPage();
        }

        sessionManager.KickPlayer(new CSteamID(value));
        return RedirectToPage(new { message = $"kicked {steamId}" });
    }
    
    public IActionResult OnPostBan(string steamId)
    {
        if (!ulong.TryParse(steamId, out var value))
        {
            return RedirectToPage();
        }

        sessionManager.TempBanPlayer(lobbyManager.GetLobbyId(), new CSteamID(value));
        return RedirectToPage(new { message = $"banned {steamId}" });
    }
}