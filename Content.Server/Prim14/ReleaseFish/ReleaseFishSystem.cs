﻿// using System.Linq;
// using Content.Shared.Interaction;
using Content.Shared.Sound;
using Content.Shared.Throwing;
using Robust.Server.Containers;
using Robust.Shared.Audio;
using Robust.Shared.Containers;
using Robust.Shared.Player;

namespace Content.Server.Prim14.ReleaseFish;

public sealed class ReleaseFishSystem : EntitySystem
{
    [Dependency] private readonly ContainerSystem _containerSystem = default!;
    private readonly SoundSpecifier _waterSplash = new SoundPathSpecifier("/Audio/Anprim14/Effects/watersplash.ogg");
    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<ReleaseFishComponent, ComponentInit>(HandleReleaseFishInit);
        SubscribeLocalEvent<ReleaseFishComponent, ThrowHitByEvent>(HandleThrowCollide);
        // SubscribeLocalEvent<ReleaseFishComponent, InteractUsingEvent>(OnInteractUsing);
    }
    private void HandleReleaseFishInit(EntityUid uid, ReleaseFishComponent component, ComponentInit args)
    {
        component.Container = _containerSystem.EnsureContainer<Container>(component.Owner, "ocean_water", out _);
    }

    private void HandleThrowCollide(EntityUid uid, ReleaseFishComponent component, ThrowHitByEvent args)
    {
        /*
        if (!component.Container.CanInsert(args.Thrown) ||
            !component.Container.Insert(args.Thrown))
            return;
        */
        SoundSystem.Play(_waterSplash.GetSound(), Filter.Pvs(args.Thrown), args.Thrown);
        QueueDel(args.Thrown);
    }

    /*
    private void OnInteractUsing(EntityUid uid, ReleaseFishComponent component, InteractUsingEvent args)
    {
        // Maybe if we want to allow for people to fish out what they throw into the water?
        if (!component.Container.ContainedEntities.Any()) return;
        TryEjectContents(component);
    }
    */

    /*
    /// <summary>
    /// Remove all entities currently in the body of water.
    /// </summary>
    private void TryEjectContents(ReleaseFishComponent component)
    {
        var contained = component.Container.ContainedEntities.ToArray();
        foreach (var entity in contained)
        {
            Remove(component, entity);
        }
    }

    private void Remove(ReleaseFishComponent component, EntityUid entity)
    {
        component.Container.Remove(entity);
    }
    */
}
