using Unity.Mathematics;
using UnityEngine;

public class DogClickController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public LayerMask groundLayer;          // 地面层
    public LayerMask interactableLayer;    // 可交互物品层

    private Rigidbody2D rb;
    private Vector2 targetPos;
    private bool isMoving = false;
    private Interactable targetItem;
    private int currentLayer = 0;
    private int targetLayer = 0;
    private Transform targetDoor = null;
    private Collider2D collider;
    private bool facingRight = true;
    public Transform mouth;

    public Interactable holdingItem;
    [HideInInspector]
    public Animator animator;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponentInChildren<Collider2D>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // 鼠标左键点击检测
        if (Input.GetMouseButton(1))
        {
            // 获取鼠标点击的世界坐标（z设为0）
            Vector3 clickWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            clickWorldPos.z = 0f;

            // 使用射线检测点击处是否有物体
            RaycastHit2D hit = Physics2D.Raycast(clickWorldPos, Vector2.zero);
            bool needCheckGround = true;
            if (hit.collider != null)
            {
                // 点击到可交互物品
                if (((1 << hit.collider.gameObject.layer) & interactableLayer) != 0)
                {
                    targetItem = hit.collider.GetComponentInParent<Interactable>();
                    
                    targetPos = hit.collider.transform.position;
                    isMoving = true;
                    
                    animator.SetBool("walk",true);
                    //return;
                }
                // 点击到地面
                else if (((1 << hit.collider.gameObject.layer) & groundLayer) != 0)
                {
                    // 只取点击点的x值，y保持当前角色的y
                    targetPos = new Vector2(clickWorldPos.x, transform.position.y);
                    isMoving = true;
                    needCheckGround = false;
                    targetLayer = hit.collider.GetComponent<Ground>().layer;
                    //return;
                    targetItem = null;
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

            if (needCheckGround)
            {
                // 如果点击处既不是 interactable 也不是 ground，
                // 则从点击位置向下发射射线寻找 ground
                RaycastHit2D hitDown = Physics2D.Raycast(clickWorldPos, Vector2.down, 100f, groundLayer);
                if (hitDown.collider != null)
                {
                    targetPos = hitDown.point;
                    targetLayer = hitDown.collider.GetComponent<Ground>().layer;
                    isMoving = true;
                    
                    animator.SetBool("walk",true);
                }
            }

            if (targetLayer != currentLayer)
            {
                targetDoor = SceneManager.Instance.doors[currentLayer].transform;
            }
        }
    }

    private Vector2 lastPosition;
    void FixedUpdate()
    {
        
        if (isMoving)
        {
            
            var velocity = rb.position - lastPosition;
            if (velocity.x > 0 && !facingRight)
                Flip();
            else if (velocity.x < 0 && facingRight)
                Flip();
            
            lastPosition = rb.position;
            
            var targetPos = this.targetPos;
            if (currentLayer != targetLayer)
            {
                targetPos = SceneManager.Instance.doors[currentLayer].transform.position;
            }
            
            targetPos.y = this.transform.position.y;
            // 使用 MoveTowards 平滑移动到目标位置
            Vector2 newPos = Vector2.MoveTowards(rb.position, targetPos, moveSpeed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);

            // 如果接近目标位置，则停止移动
            if (math.abs(rb.position.x - targetPos.x)  < 0.1f)
            {
                //isMoving = false;
                animator.SetBool("walk",false);
                isMoving = false;
            
                if (targetItem != null)
                {
                    if (Vector2.Distance(targetItem.transform.position, collider.transform.position)<2)
                    //if(targetItem.collider.bounds.Intersects(collider.bounds))
                    {
                        //isMoving = false; 
                        targetItem.DogInteract(this);
                        targetItem = null;
                    }
                }
            }

            if (targetDoor && math.abs(rb.position.x- targetDoor.position.x) < 0.1f)
            {
                if (currentLayer != targetLayer)
                {
                    targetDoor = null;
                    currentLayer = targetLayer;
                    newPos.y = SceneManager.Instance.doors[targetLayer].transform.position.y;

                    rb.position = newPos;
                }
            }


        }
    }
    
    
    // 翻转角色的方法
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }


    // 碰撞时停止移动
    void OnCollisionEnter2D(Collision2D collision)
    {
        //isMoving = false;
    }
}
