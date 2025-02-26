using UnityEngine;

public interface IDamageable
{
    void TakeDamage(int damage);
    Rigidbody2D GetRigidbody();
}
