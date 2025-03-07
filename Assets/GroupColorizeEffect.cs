using UnityEngine;
using DG.Tweening;

public class GroupColorizeEffect : MonoBehaviour
{
    [Tooltip("过渡持续时间")]
    public float duration = 2f;
    [Tooltip("目标状态：1 为全彩色，0 为全灰度")]
    public float targetColorize = 1f;
    
    private SpriteRenderer[] spriteRenderers;

    void Start()
    {
        // 获取当前 transform 下所有 SpriteRenderer
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

        // 为每个 SpriteRenderer 分配一个新的材质实例，使用自定义 Shader
        foreach (var sr in spriteRenderers)
        {
            Material newMat = new Material(Shader.Find("Custom/GreyscaleToColor"));
            // 复制原有材质的主纹理（如果有）
            if (sr.material.HasProperty("_MainTex"))
                newMat.SetTexture("_MainTex", sr.material.GetTexture("_MainTex"));
            newMat.SetFloat("_Colorize", 0f); // 初始设置为全灰度
            sr.material = newMat;
        }
    }
    
    void Update()
    {
        // 按下空格键启动颜色过渡效果
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartColorizeEffect();
        }
    }

    // 调用此方法启动颜色过渡效果
    public void StartColorizeEffect()
    {
        foreach (var sr in spriteRenderers)
        {
            sr.material.DOFloat(targetColorize, "_Colorize", duration);
        }
    }
}