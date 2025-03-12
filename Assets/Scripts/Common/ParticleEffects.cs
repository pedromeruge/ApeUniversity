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
        Collider2D collider = obj.GetComponent<Collider2D>();
        if (collider != null) {
            collider.enabled = false; // disable collider to prevent collisions when object "appears" gone but hasn't been destructed yet
        }
        Destroy(obj, particlesDuration);
    }
}
