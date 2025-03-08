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
        ParticleEffects.PlayParticlesOnDestroy(moneyParticles, this.gameObject, oreSprite);
    }
}

