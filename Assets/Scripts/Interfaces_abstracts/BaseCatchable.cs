using UnityEngine;
using System;

// base obstacle functionality
public abstract class BaseCatchable: MonoBehaviour, ICatchable
{

    public abstract void OnCaught();

    // TODO: any common behavior for all catchable objects needed?
}
