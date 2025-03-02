using UnityEngine;
using System;

// base obstacle functionality
public class EventObstacle: MonoBehaviour
{
    public static event Action<IDamageable, int> OnObstacleHit;

    // events can only be called from the class that declared them, so to be able to use in subclasses, they need to use this function
    public static void Hit(IDamageable target, int livesDamage=1)
    {
        OnObstacleHit?.Invoke(target, livesDamage);
    }

    // public abstract void sound() // abstract method to play sound, needed later?
}
