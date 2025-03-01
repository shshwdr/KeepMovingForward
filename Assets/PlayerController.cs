using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 2f; // Movement speed
    public float stopDistance = 0.1f; // Distance threshold to stop moving

    private Vector2 targetPosition;
    private bool isMoving = false;

    void Update()
    {
        // Update target position based on mouse input
        if (Input.GetMouseButton(0)) // Left-click to move
        {
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isMoving = true;
        }

        // Move character toward target
        if (isMoving)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Stop moving when close enough
            if (Vector2.Distance(transform.position, targetPosition) < stopDistance)
            {
                isMoving = false;
            }
        }
    }
}
