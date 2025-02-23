using UnityEngine;
using System;

public class ObstacleEvent : MonoBehaviour
{
    public int livesDamage = 1;

    public static event Action<IDamageable, int> OnObstacleHit;

    private void OnTriggerEnter2D(Collider2D collision)
    {   
        Debug.Log("Triggered by: " + collision.gameObject.name);

        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null) // if the object has a damageable component
        {
            OnObstacleHit?.Invoke(damageable, livesDamage);
        }
    }
}
