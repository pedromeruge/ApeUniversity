using UnityEngine;

public interface IKnockbackable
{
    void Knockback(Vector3 force);

    Transform getTransform();
}
