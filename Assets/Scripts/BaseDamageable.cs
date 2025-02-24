using System.Collections;
using UnityEngine;

public abstract class BaseDamageable : MonoBehaviour, IDamageable
{
    public abstract void TakeDamage(int damage);

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Color flashColor = Color.white;
    [SerializeField] private float flashDuration = 0.25f;
    [SerializeField] private int numberOfFlashes = 4;

    private Coroutine damageFlashCoroutine;

    protected void CallDamageFlash() {
        damageFlashCoroutine = StartCoroutine(DamageFlasher());
    }
    
    private IEnumerator DamageFlasher() {
        //set color
        spriteRenderer.material.SetColor("_FlashColor", flashColor); // name of material property in shader

        float currentFlashAmount, elapsedTime;

        //flash number of flashes amount of times
        for (int i=0; i <numberOfFlashes; i++) {
            elapsedTime = 0f;

            //lerp flash amount during flash duration
            while(elapsedTime < flashDuration) {
                elapsedTime += Time.deltaTime;
                currentFlashAmount = 1 - (elapsedTime / flashDuration);
                spriteRenderer.material.SetFloat("_FlashAmount", currentFlashAmount); // name of material property in shader
                yield return null;
            }
        }
    }

    public Rigidbody2D GetRigidbody() {
        return rb;
    }
}
