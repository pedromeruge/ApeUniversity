using UnityEngine;

public class BombBagStaticCollectible : MonoBehaviour, IStaticCollectible
{
    [SerializeField] private int minBombValue = 1;
    [SerializeField] private int maxBombValue = 3;
    [SerializeField] private ParticleSystem particles;
    [SerializeField] private SpriteRenderer bagSprite;
    private bool once = false;
    public void onCollect(PlayerStats playerStats) {
        if (once) return; // if already collected, but still playing the particle animation, dont count it again, just ignore it
        once = true;
        playerStats.modifyBombs(Random.Range(minBombValue, maxBombValue + 1));
        ParticleEffects.PlayParticlesOnDestroy(particles, this.gameObject, bagSprite);
    }

}

