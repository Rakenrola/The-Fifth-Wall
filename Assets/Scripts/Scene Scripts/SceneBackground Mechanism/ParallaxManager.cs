using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxManager : MonoBehaviour
{
    public List<GameObject> levels;
    public float transitionDelay = 5f;  // Time to wait before transitioning to the next
    public float fadeDuration = 1f;     // Duration of fade in/out

    private int currentIndex = 0;

    private void Start()
    {
        // Ensure only the first level is visible at start
        for (int i = 0; i < levels.Count; i++)
        {
            SetAlpha(levels[i], i == 0 ? 1f : 0f);
        }

        StartCoroutine(CrossFadeCycle());
    }

    IEnumerator CrossFadeCycle()
    {
        while (true)
        {
            GameObject currentLevel = levels[currentIndex];
            int nextIndex = (currentIndex + 1) % levels.Count;
            GameObject nextLevel = levels[nextIndex];

            // Start fading in the next level and fading out the current one
            StartCoroutine(FadeLevel(currentLevel, 0f));
            StartCoroutine(FadeLevel(nextLevel, 1f));

            // Wait for crossfade to complete and display time
            yield return new WaitForSeconds(fadeDuration + transitionDelay);

            currentIndex = nextIndex;
        }
    }

    IEnumerator FadeLevel(GameObject level, float targetAlpha)
    {
        SpriteRenderer[] sprites = level.GetComponentsInChildren<SpriteRenderer>();
        float timer = 0f;

        // Get current alphas
        float[] startAlphas = new float[sprites.Length];
        for (int i = 0; i < sprites.Length; i++)
            startAlphas[i] = sprites[i].color.a;

        while (timer < fadeDuration)
        {
            float t = timer / fadeDuration;
            for (int i = 0; i < sprites.Length; i++)
            {
                Color c = sprites[i].color;
                c.a = Mathf.Lerp(startAlphas[i], targetAlpha, t);
                sprites[i].color = c;
            }

            timer += Time.deltaTime;
            yield return null;
        }

        // Final alpha
        for (int i = 0; i < sprites.Length; i++)
        {
            Color c = sprites[i].color;
            c.a = targetAlpha;
            sprites[i].color = c;
        }
    }

    void SetAlpha(GameObject level, float alpha)
    {
        SpriteRenderer[] sprites = level.GetComponentsInChildren<SpriteRenderer>();
        foreach (var sr in sprites)
        {
            Color c = sr.color;
            c.a = alpha;
            sr.color = c;
        }
    }
}
