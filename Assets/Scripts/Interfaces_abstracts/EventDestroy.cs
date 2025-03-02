using UnityEngine;
using System;

// base obstacle functionality
public class EventDestroy: MonoBehaviour
{
    public static event Action<IDestructible, Vector3, float> OnDestroy;

    // events can only be called from the class that declared them, so to be able to use in subclasses, they need to use this function
    // target: the object that is destroyed
    // objPos: the position where the object is destroyed
    // radius: the radius of the explosion (useful for tilemap destructible objects)
    public static void Destroy(IDestructible target, Vector3 objPos, float radius)
    {
        OnDestroy?.Invoke(target, objPos, radius);
    }

    // public abstract void sound() // abstract method to play sound, needed later?
}
