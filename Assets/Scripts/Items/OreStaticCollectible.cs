using UnityEngine;

public class OreStaticCollectible : MonoBehaviour, IStaticCollectible
{
    [SerializeField] private int oreValue = 200;
    [SerializeField] private ParticleSystem moneyParticles;
    [SerializeField] private SpriteRenderer oreSprite;
    private bool once = false;
    public void onCollect(PlayerStats playerStats) {
        if (once) return; // if already collected, but still playing the particle animation, dont count it again, just ignore it
        once = true;
        playerStats.modifyMoney(oreValue);
        destroyObj();
    }

    private void destroyObj() {
        var particlesDuration = moneyParticles.main.duration;

        moneyParticles.Play();
        oreSprite.enabled = false; // Hide the ore sprite, so it appears it has been collected
        this.Invoke(nameof(destroyParticles), particlesDuration);
    }

    // Destroy the whole gameobject after particles have finished playing
    void destroyParticles() {
        Destroy(this.gameObject);
    }  
}

