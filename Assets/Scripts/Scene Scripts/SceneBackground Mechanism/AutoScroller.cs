using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ParallaxLayer
{
    public Transform layerTransform; // The child GameObject with SpriteRenderer
    public float scrollSpd = 1f;

    [HideInInspector] public float spriteWidth;
    [HideInInspector] public Vector3 startPosition;
}

public class AutoScroller : MonoBehaviour 
{
    public List<ParallaxLayer> layers = new List<ParallaxLayer>();

    void Start()
    {
        foreach (var layer in layers)
        {
            if (layer.layerTransform == null)
            {
                Debug.LogWarning("Layer transform not set.");
                continue;
            }

            var sr = layer.layerTransform.GetComponent<SpriteRenderer>();
            if (sr == null)
            {
                Debug.LogWarning("No SpriteRenderer found on " + layer.layerTransform.name);
                continue;
            }

            layer.spriteWidth = sr.bounds.size.x;
            layer.startPosition = layer.layerTransform.position;
        }
    }

    void Update()
    {
        foreach (var layer in layers)
        {
            if (layer.layerTransform == null) continue;

            // Scroll left over time
            layer.layerTransform.Translate(Vector3.left * layer.scrollSpd * Time.deltaTime);

            // Loop back if fully off screen
            if (layer.layerTransform.position.x < layer.startPosition.x - layer.spriteWidth)
            {
                layer.layerTransform.position = new Vector3(
                    layer.layerTransform.position.x + 2 * layer.spriteWidth,
                    layer.layerTransform.position.y,
                    layer.layerTransform.position.z
                );
            }
        }
    }
}
