using System.Collections;
using UnityEngine;

public class SpriteEffects
{
    // assumes renderer has material 
    public static IEnumerator ColorFlasher(Renderer renderer, AnimationCurve flashAnim, Color flashColor, float flashDuration) {
        Debug.Log("renderer: " + renderer + "material: " + renderer.sharedMaterial + "name: " + renderer.sharedMaterial.name);
        if (renderer == null && renderer.sharedMaterial.name != "DamageFlash") {
            Debug.LogWarning("renderer does not have flash material, cannot flash color");
            yield break;
        }

        MaterialPropertyBlock block = new MaterialPropertyBlock();
        renderer.GetPropertyBlock(block);

        //set color
        block.SetColor("_FlashColor", flashColor); // name of material property in shader

        float currentFlashAmount, elapsedTime = 0f;

        //lerp flash amount during flash duration
        while(elapsedTime < flashDuration) {
            elapsedTime += Time.deltaTime;
            currentFlashAmount = Mathf.Lerp(1f, flashAnim.Evaluate(elapsedTime), elapsedTime / flashDuration);
            block.SetFloat("_FlashAmount", currentFlashAmount); // name of material property in shader
            renderer.SetPropertyBlock(block);
            yield return null;
        }

        block.SetFloat("_FlashAmount", 0f);
        renderer.SetPropertyBlock(block);
    }
}
