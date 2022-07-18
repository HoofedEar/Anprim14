namespace Content.Server.Prim14.Blocker;

[RegisterComponent]
[Access(typeof(BlockerSystem))]
public sealed class BlockerComponent : Component
{
    /// <summary>
    /// How long should the blocker exist?
    /// </summary>
    [DataField("timer")] public int Timer;

    public float Accumulator;
}
