using Cysharp.Threading;
using Microsoft.Extensions.Options;
using WFDS.Common.Steam;
using WFDS.Server.Core.Configuration;
using WFDS.Server.Core.Network;

namespace WFDS.Server.Core;

internal class WebFishingServer(
    IHostApplicationLifetime lifetime,
    ILogger<WebFishingServer> logger,
    IOptions<ServerSetting> settings,
    SteamManager steam,
    LobbyManager lobby,
    SessionManager session
) : BackgroundService
{
    private readonly Mutex _mutex = new(false, "WFDS.Server");
    private readonly LogicLooper _looper = new(100);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (!_mutex.WaitOne(0, false))
        {
            logger.LogError("WFDS.Server is already running");
            lifetime.StopApplication();
            return;
        }

        logger.LogInformation("WebFishingServer start");
        _ = _looper.RegisterActionAsync(Update).ConfigureAwait(false);

        if (!steam.Init())
        {
            lifetime.StopApplication();
            return;
        }

        lobby.Initialize(
            settings.Value.ServerName,
            settings.Value.LobbyType,
            settings.Value.MaxPlayers,
            settings.Value.Adult,
            settings.Value.RoomCode
        );
        lobby.CreateLobby();

        session.BanPlayers(lobby.GetLobbyId(), settings.Value.BannedPlayers);
        while (!stoppingToken.IsCancellationRequested)
        {
            if (lobby.GetLobbyId().IsValid())
            {
                if (!lobby.IsInLobby())
                {
                    break;
                }
            }

            await Task.Delay(1000, stoppingToken);
        }

        // 프로그램을 를 종료한다.
        logger.LogInformation("WebFishingServer stop");
        lifetime.StopApplication();
    }

    private bool Update(in LogicLooperActionContext ctx)
    {
        if (ctx.CancellationToken.IsCancellationRequested)
        {
            logger.LogError("update canceled");
            return false;
        }

        if (!steam.Initialized)
        {
            return true;
        }

        steam.RunCallbacks();
        return true;
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("try steam looper is stopping");
        await _looper.ShutdownAsync(TimeSpan.Zero);
        _looper.Dispose();

        logger.LogInformation("try session close");
        session.ServerClose();

        logger.LogInformation("steam api shutdown");
        steam.Shutdown();

        await base.StopAsync(cancellationToken);
        logger.LogInformation("WebFishingServer stopped");
    }
}