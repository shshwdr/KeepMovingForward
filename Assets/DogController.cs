// using UnityEngine;
//
// public class DogController : MonoBehaviour
// {
//     [Header("Movement Settings")]
//     public float moveSpeed = 5f; // 移动速度
//
//     [Header("Interaction Settings")]
//     public Transform interactionPoint;  // 用于检测交互/拾取的检测点（一般放在狗的前方）
//     public float interactionRange = 1f;   // 检测范围
//     public LayerMask interactableLayer;   // 可交互对象的层
//     public LayerMask pickupLayer;         // 可拾取物品的层
//     public Transform mouse;
//     private Rigidbody2D rb;
//     private Vector2 movement;
//     private bool facingRight = true;
//
//     void Start()
//     {
//         rb = GetComponent<Rigidbody2D>();
//     }
//
//     void Update()
//     {
//         // 获取左右键输入
//         float moveInput = Input.GetAxisRaw("Horizontal");
//         movement = new Vector2(moveInput, 0f);
//
//         // 翻转角色方向
//         if (moveInput > 0 && !facingRight)
//             Flip();
//         else if (moveInput < 0 && facingRight)
//             Flip();
//
//         // 按上键进行交互
//         if (Input.GetKeyDown(KeyCode.UpArrow))
//         {
//             Interact();
//         }
//
//         // 按下键拾取物品
//         if (Input.GetKeyDown(KeyCode.DownArrow))
//         {
//             PickUp();
//         }
//     }
//
//     void FixedUpdate()
//     {
//         // 通过刚体速度实现移动，碰撞系统会自动处理障碍物阻挡
//         rb.velocity = new Vector2(movement.x * moveSpeed, rb.velocity.y);
//     }
//
//     // 翻转角色的方法
//     void Flip()
//     {
//         facingRight = !facingRight;
//         Vector3 scale = transform.localScale;
//         scale.x *= -1;
//         transform.localScale = scale;
//     }
//
//     // 交互功能：通过射线检测前方是否有可交互对象
//     void Interact()
//     {
//         // // 从检测点向角色面朝方向发射射线
//         // Vector2 direction = facingRight ? Vector2.right : Vector2.left;
//         // RaycastHit2D hit = Physics2D.Raycast(interactionPoint.position, direction, interactionRange, interactableLayer);
//         //
//         // if (hit.collider != null)
//         // {
//         //     // 如果检测到可交互对象，则调用其 Interact 方法
//         //     Interactable interactable = hit.collider.GetComponent<Interactable>();
//         //     if (interactable != null)
//         //     {
//         //         interactable.Interact();
//         //     }
//         // }
//     }
//
//     // 拾取功能：通过射线检测前方是否有可拾取物品
//     void PickUp()
//     {
//         Vector2 direction = facingRight ? Vector2.right : Vector2.left;
//         Collider2D[] hits = Physics2D.OverlapCircleAll(interactionPoint.position, interactionRange);
//
//         foreach (var hit in hits)
//         {
//             
//             if (hit != null)
//             {
//                 // 如果检测到可拾取物品，则调用其 PickUp 方法
//                 Interactable item = hit.GetComponent<Interactable>();
//                 if (item != null)
//                 {
//                     item.DogInteract(this);
//                 }
//             }
//         }
//     }
// }
