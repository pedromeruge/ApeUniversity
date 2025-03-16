using UnityEngine;

public class BatDamageScript : BaseDamageable
{
    //[SerializeField] private MonkeyCatchableScript monkeyCatchableScript;
    public PlayerStats playerStats;

    public override void TakeDamage(int damage = -1)
    {
        bool isDead = playerStats.modifyLives(damage);
        CallDamageFlash(); // blink effect
        //monkeyCatchableScript.OnHit();
    }
}