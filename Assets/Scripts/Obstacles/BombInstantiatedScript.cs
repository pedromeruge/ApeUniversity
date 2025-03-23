using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class BombInstantiatedScript : BaseExplosive {
    [SerializeField] protected float countdown = 1.25f;
    [SerializeField] AnimationCurve flashAnim;

    [ColorUsage(true, true)]
    [SerializeField] Color flashColor;

    private Renderer spriteRenderer = null;
    AudioManager audioManager;

    void Start()
    {
        startCountdown();
    }
    
    void Awake() {

        if (spriteRenderer == null) {
            spriteRenderer = GetComponent<Renderer>();
        }
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void startCountdown() {
        StartCoroutine(countdownExplosion(countdown));
    }

    IEnumerator countdownExplosion(float seconds) {
        StartCoroutine(SpriteEffects.ColorFlasher(spriteRenderer, flashAnim, flashColor, seconds));
        yield return new WaitForSeconds(seconds);
        Explode();
        audioManager.PlaySFX(audioManager.bombExplosion);
    }
}