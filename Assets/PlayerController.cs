using System;
using System.Linq;
using Cinemachine;
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

    public GameObject tissue;

    public Transform characterRenderer;

    private Interactable targetItem;

    private Vector3 lastPosition;
    private bool facingRight = true;

    DialogueBubble dialogue;

    private Interactable currentOver;

    private void Awake()
    {
        dialogue = GetComponentInChildren<DialogueBubble>();
    }

    void Update()
    {
        var velocity = transform.position - lastPosition;
        if (velocity.x > 0 && !facingRight)
            Flip();
        else if (velocity.x < 0 && facingRight)
            Flip();
        lastPosition = transform.position;

        Camera cineCam = CinemachineCore.Instance.GetActiveBrain(0).OutputCamera;
        {
            
            Interactable over;
            var targetPosition = cineCam.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.RaycastAll(targetPosition, Vector2.zero,100, interactableLayer);
            if (hits.Length > 0)
            {
                // 根据 SpriteRenderer 的 sortingOrder 倒序排列（排序越大，渲染越靠前）
                RaycastHit2D topHit = hits.OrderByDescending(hit =>
                {
                    // 如果没有 SpriteRenderer，则返回 0
                    SpriteRenderer sr = hit.collider.GetComponent<SpriteRenderer>();
                    int layerValue = SortingLayer.GetLayerValueFromID(sr.sortingLayerID);

                    return layerValue*10000+sr.sortingOrder;
                }).First();
                if (topHit.collider != null && ((1 << topHit.collider.gameObject.layer) & interactableLayer) != 0)
                {
                    over = topHit.collider.GetComponentInParent<Interactable>();
                }
                else
                {
                    over = null;
                }

                //Debug.Log("最上层的Collider是：" + topHit.collider.name);
            }
            else
            {
                over = null;
            }
            
            if (over != currentOver)
            {
                if (currentOver != null && currentOver.outline != null)
                {
                    
                    currentOver.outline.SetActive(false);
                }
                currentOver = over;
                if (over != null && over.outline != null)
                {
                    over.outline.SetActive(true);
                }
            }
            
        }
        
        // Update target position based on mouse input
        if (Input.GetMouseButton(0)) // Left-click to move
        {
            targetPosition = cineCam.ScreenToWorldPoint(Input.mousePosition);
            isMoving = true;
            RaycastHit2D[] hits = Physics2D.RaycastAll(targetPosition, Vector2.zero,100, interactableLayer);
            if (hits.Length > 0)
            {
                // 根据 SpriteRenderer 的 sortingOrder 倒序排列（排序越大，渲染越靠前）
                RaycastHit2D topHit = hits.OrderByDescending(hit =>
                {
                    // 如果没有 SpriteRenderer，则返回 0
                    SpriteRenderer sr = hit.collider.GetComponent<SpriteRenderer>();
                    int layerValue = SortingLayer.GetLayerValueFromID(sr.sortingLayerID);

                    return layerValue*10000+sr.sortingOrder;
                }).First();

                if (topHit.collider != null)
                {
                    // 点击到可交互物品
                    if (((1 << topHit.collider.gameObject.layer) & interactableLayer) != 0)
                    {
                        targetItem = topHit.collider.GetComponentInParent<Interactable>();
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

            //RaycastHit2D hit = Physics2D.Raycast(targetPosition, Vector2.zero);
            //bool needCheckGround = true;
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

                    
                        dialogue.Show(targetItem.commentName);
                    
                    targetItem = null;
                }
            }
            
        }
    }
}
