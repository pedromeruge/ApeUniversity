using UnityEngine;
using System;

// base obstacle functionality
public abstract class BaseHittable: MonoBehaviour, IHittable
{

    public abstract void OnHit();

    // TODO: any common behavior for all catchable objects needed?
}
