using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopySpriteRenderAndSize : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public SpriteRenderer spriteRendererCopiedFrom;
    // Start is called before the first frame update
    void Start()
    {
        
        if (spriteRenderer != null)
        {
             spriteRenderer.sprite = spriteRendererCopiedFrom.sprite;
             spriteRenderer.transform.localScale = spriteRendererCopiedFrom.transform.localScale * 1.1f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
