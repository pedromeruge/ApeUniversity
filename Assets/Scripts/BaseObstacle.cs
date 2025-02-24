using UnityEngine;
using System;

// base obstacle functionality
public abstract class BaseObstacle: MonoBehaviour
{
    protected int livesDamage = 1;

    public static event Action<IDamageable, int> OnObstacleHit;

    // events can only be called from the class that declared them, so to be able to use in subclasses, they need to use this function
    protected virtual void Hit(IDamageable target)
    {
        OnObstacleHit?.Invoke(target, livesDamage);
    }

    // public abstract void sound() // abstract method to play sound, needed later?
}
