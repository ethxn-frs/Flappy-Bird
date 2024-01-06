using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public float backgroundSpeed = 0.1f;
    float spriteSize;

    public SpriteRenderer backgroundSprite;

    Vector3 startPosition;

    private void Awake()
    {
        
    }

    void Start()
    {
        startPosition = transform.position;
        spriteSize = backgroundSprite.sprite.bounds.size.x * backgroundSprite.transform.localScale.x;
    }

    void Update()
    {
        float newPosition = Mathf.Repeat(Time.time * backgroundSpeed, spriteSize);
        transform.position = startPosition + Vector3.left * newPosition;

    }
}
