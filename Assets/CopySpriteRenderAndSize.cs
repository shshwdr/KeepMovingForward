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
             spriteRenderer.transform.localScale = spriteRendererCopiedFrom.transform.localScale + new Vector3(0.1f,0.1f,0);
             spriteRenderer.sortingLayerID = spriteRendererCopiedFrom.sortingLayerID;
             spriteRenderer.sortingOrder = spriteRendererCopiedFrom.sortingOrder-10;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
