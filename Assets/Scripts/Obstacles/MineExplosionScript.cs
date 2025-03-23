using UnityEngine;
using System.Collections;
public class MineExplosionScript : BaseExplosive
{
    [SerializeField] protected float countdown = 2.0f;
    [SerializeField] AnimationCurve flashAnim;

    [ColorUsage(true, true)]
    [SerializeField] Color flashColor;
    AudioManager audioManager;

    private Renderer spriteRenderer = null;

    void Awake() {

        if (spriteRenderer == null) {
            spriteRenderer = GetComponent<Renderer>();
        }
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        StartCoroutine(countdownExplosion(countdown));
    }

    IEnumerator countdownExplosion(float seconds) {
        StartCoroutine(SpriteEffects.ColorFlasher(spriteRenderer, flashAnim, flashColor, seconds));
        yield return new WaitForSeconds(seconds);
        Explode();
        audioManager.PlaySFX(audioManager.bombExplosion);
    }
}