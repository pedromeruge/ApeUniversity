using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]

 //based on https://www.youtube.com/watch?v=MEy-kIGE-lI

 //manager of parallax layers in the scene
public class ParallaxBackground : MonoBehaviour
{
    public ParallaxCamera parallaxCamera;
    List<ParallaxLayer> parallaxLayers = new List<ParallaxLayer>();

    void Start()
    {
        if (parallaxCamera != null)
            parallaxCamera.onCameraTranslate += Move; // subscribing to the event, meaning any time the camera moves, it calls the Move method

        SetLayers();
    }

    void SetLayers()
    {
        parallaxLayers.Clear();

        for (int i = 0; i < this.transform.childCount; i++)
        {
            ParallaxLayer layer = this.transform.GetChild(i).GetComponent<ParallaxLayer>(); // get all child parallax layers

            if (layer != null)
            {
                layer.name = "Layer-" + i;
                parallaxLayers.Add(layer);
            }
        }
    }

    void Move(float delta)
    {
        foreach (ParallaxLayer layer in parallaxLayers)
        {
            layer.Move(delta); // move each layer with the camera displacement delta
        }
    }
}