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
             
             SpriteRenderer sr = spriteRenderer;
                 // 使用 bounds 来获取世界单位下的尺寸
                 float width = sr.sprite.bounds.size.x;
                 float height = sr.sprite.bounds.size.y;
                 float ratio = width / height;
                 //Debug.Log("Sprite 的高度宽度比例为: " + ratio);
             
             
             var scaleAdd = new Vector3(0.1f, 0.1f * ratio, 0);
             
             spriteRenderer.transform.localScale = spriteRendererCopiedFrom.transform.localScale +scaleAdd;
             spriteRenderer.sortingLayerID = spriteRendererCopiedFrom.sortingLayerID;
             spriteRenderer.sortingOrder = spriteRendererCopiedFrom.sortingOrder-10;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
