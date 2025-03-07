using System.Collections;
using UnityEngine;
using DG.Tweening;

public class GroupFade : MonoBehaviour
{
    // 淡入淡出的持续时间
    public float fadeDuration = 0.5f;
    // 目标透明度（0: 全透明，1: 不透明）
    public float targetAlpha = 0f;

    // 存储所有子物体上的 SpriteRenderer
    private SpriteRenderer[] spriteRenderers;

    void Awake()
    {
        // 获取当前 transform 下所有的 SpriteRenderer（包括子物体）
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
    }

    // 调用此方法使所有 SpriteRenderer 淡出
    public void FadeOut()
    {
        foreach (var sr in spriteRenderers)
        {
            // DOTween 直接淡出颜色 alpha（保留原有颜色的 RGB 部分）
            sr.DOColor(new Color(sr.color.r, sr.color.g, sr.color.b, targetAlpha), fadeDuration);
        }
        StartCoroutine(test());
    }

    IEnumerator test()
    {
        yield return new WaitForSeconds(fadeDuration);
        gameObject.SetActive(false);
    }

    // 调用此方法使所有 SpriteRenderer 淡入（恢复到不透明或指定透明度）
    public void FadeIn(float alpha = 1f)
    {
        foreach (var sr in spriteRenderers)
        {
            sr.color = new Color(1, 1, 1, 0);
;            sr.DOColor(new Color(sr.color.r, sr.color.g, sr.color.b, alpha), fadeDuration);
        }
    }
}