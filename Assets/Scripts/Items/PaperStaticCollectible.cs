using UnityEngine;

public class PaperStaticCollectible : MonoBehaviour, IStaticCollectible
{

    [SerializeField] private ParticleSystem particles;
    [SerializeField] private SpriteRenderer bagSprite;
    private bool once = false;
    public void onCollect(PlayerStats playerStats) {
        if (once) return; // if already collected, but still playing the particle animation, dont count it again, just ignore it
        once = true;
        playerStats.modifyMonkeys(1);
        ParticleEffects.PlayParticlesOnDestroy(particles, this.gameObject, bagSprite);
    }
}

