using UnityEngine;

public class ParticleEffects: MonoBehaviour
{
    public static void PlayParticlesOnDestroy(ParticleSystem particles, GameObject obj, SpriteRenderer sprite = null) {

        if (sprite == null) {
            sprite = obj.GetComponent<SpriteRenderer>();
        }

        var particlesDuration = particles.main.duration;
        particles.Play();
        sprite.enabled = false;
        Destroy(obj, particlesDuration);
    }
}
