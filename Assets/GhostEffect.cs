using UnityEngine;
using DG.Tweening;

public class GhostEffect : MonoBehaviour
{
    [Header("Float Settings")]
    public float floatDistance = 0.5f;    // 浮动幅度
    public float floatDuration = 2f;      // 浮动时长

    [Header("Fade Settings")]
    public float fadeDuration = 1f;       // 淡入淡出时长
    public float minAlpha = 0.3f;         // 最低透明度
    public float maxAlpha = 1f;           // 最高透明度

    private SpriteRenderer spriteRenderer;
    private Vector3 startPos;

    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        startPos = transform.localPosition;

        // 上下浮动：以初始位置为基准，向上移动 floatDistance，然后往下往返循环
        spriteRenderer.transform.DOLocalMoveY(startPos.y + floatDistance, floatDuration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);

        // 淡入淡出效果：循环改变 SpriteRenderer 的 alpha
        spriteRenderer.DOFade(minAlpha, fadeDuration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }
}
