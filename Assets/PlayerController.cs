using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 2f; // Movement speed
    public float stopDistance = 0.1f; // Distance threshold to stop moving

    public LayerMask interactableLayer;    // 可交互物品层
    private Vector2 targetPosition;
    private bool isMoving = false;

    public Transform characterRenderer;

    private Interactable targetItem;

    private Vector3 lastPosition;
    private bool facingRight = true;

    public CanvasGroup dialogue;
    private Sequence dialogueDotween;
    void Update()
    {
        var velocity = transform.position - lastPosition;
        if (velocity.x > 0 && !facingRight)
            Flip();
        else if (velocity.x < 0 && facingRight)
            Flip();
        lastPosition = transform.position;
        // Update target position based on mouse input
        if (Input.GetMouseButton(0)) // Left-click to move
        {
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isMoving = true;

            RaycastHit2D hit = Physics2D.Raycast(targetPosition, Vector2.zero);
            bool needCheckGround = true;
            if (hit.collider != null)
            {
                // 点击到可交互物品
                if (((1 << hit.collider.gameObject.layer) & interactableLayer) != 0)
                {
                    targetItem = hit.collider.GetComponentInParent<Interactable>();
                    isMoving = true;
                    //return;
                }
                else
                {
                    targetItem = null;
                }
            }
            else
            {
                targetItem = null;
            }
        }



    }

    // 翻转角色的方法
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = characterRenderer.localScale;
        scale.x *= -1;
        characterRenderer.localScale = scale;
    }
    private void FixedUpdate()
    {
        // Move character toward target
        if (isMoving)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Stop moving when close enough
            if (Vector2.Distance(transform.position, targetPosition) < stopDistance)
            {
                isMoving = false;
                
                if (targetItem != null)
                {
                    targetItem.Interact();

                    if (targetItem.commentName != "" &&
                        CSVLoader.Instance.dialogueIndex.ContainsKey(targetItem.commentName))
                    {
                        if (dialogueDotween != null && dialogueDotween.IsActive())
                            dialogueDotween.Kill();
                        
                        
                        dialogueDotween = DOTween.Sequence();
                        dialogueDotween.Append(dialogue.DOFade(1f, 0.5f));
                        dialogueDotween.AppendInterval(5);
                        dialogueDotween.Append(dialogue.DOFade(0f, 0.5f));
                        var texts = CSVLoader.Instance.DialogueInfoMap[targetItem.commentName];
                        var index = CSVLoader.Instance.dialogueIndex[targetItem.commentName];
                        
                        dialogue.GetComponentInChildren<TMP_Text>().text = texts[index].text;
                        index++;
                        if (index >= texts.Count)
                        {
                            index = 0;
                        }
                        CSVLoader.Instance.dialogueIndex[targetItem.commentName] = index;
                    }
                    
                    targetItem = null;
                }
            }
            
        }
    }
}
