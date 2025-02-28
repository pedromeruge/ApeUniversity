using System.Collections;
using UnityEngine;

public class SpriteEffects
{
    // assumes sprite has material 
    public static IEnumerator ColorFlasher(SpriteRenderer sprite, AnimationCurve _flashSpeed, Color flashColor, float flashDuration) {
        if (sprite.material.name != "DamageFlash") {
            Debug.LogWarning("Sprite does not have flash material, cannot flash color");
            yield break;
        }

        //set color
        sprite.material.SetColor("_FlashColor", flashColor); // name of material property in shader

        float currentFlashAmount, elapsedTime = 0f;

        //lerp flash amount during flash duration
        while(elapsedTime < flashDuration) {
            elapsedTime += Time.deltaTime;
            currentFlashAmount = Mathf.Lerp(1f, _flashSpeed.Evaluate(elapsedTime), elapsedTime / flashDuration);
            sprite.material.SetFloat("_FlashAmount", currentFlashAmount); // name of material property in shader
            yield return null;
        }
    }
}
