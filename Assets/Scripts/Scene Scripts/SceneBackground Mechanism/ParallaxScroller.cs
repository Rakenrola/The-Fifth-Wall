using UnityEngine;

public class ParallaxScroller : MonoBehaviour
{
    public float scrollSpeed = 1f;

    private float startPosX;
    private float spriteWidth;

    void Start()
    {
        startPosX = transform.position.x;
        SpriteRenderer sr = GetComponentInChildren<SpriteRenderer>();
        if (sr != null)
        {
            spriteWidth = sr.bounds.size.x;
        }
    }

    void FixedUpdate()
    {
        transform.position += Vector3.right * scrollSpeed * Time.deltaTime;

        if (transform.position.x >= startPosX + spriteWidth)
        {
            transform.position = new Vector3(startPosX - spriteWidth, transform.position.y, transform.position.z);
        }
    }
}