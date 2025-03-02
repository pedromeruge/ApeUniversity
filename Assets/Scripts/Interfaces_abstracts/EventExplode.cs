using UnityEngine;
using System;

// base obstacle functionality
public class EventExplode: MonoBehaviour
{
    public static event Action<IExplosive> OnExplode;

    // target: the object that is exploding

    public static void Explode(IExplosive target)
    {
        OnExplode?.Invoke(target);
    }

    // public abstract void sound() // abstract method to play sound, needed later?
}
