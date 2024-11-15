using System.Numerics;

namespace WFDS.Common.ChannelEvents.Events;

public class ActorTransformUpdateEvent(long actorId, Vector3 position, Vector3 rotation) : ChannelEvent
{
    public long ActorId { get; init; } = actorId;
    public Vector3 Position { get; init; } = position;
    public Vector3 Rotation { get; init; } = rotation;
}