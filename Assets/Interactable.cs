using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float interactDistance = 2f; // Max distance for interaction
    private Transform player; // Reference to the player
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void OnMouseDown()
    {
        if (IsPlayerCloseEnough())
        {
            Interact();
        }
        else
        {
            Debug.Log("You are too far away!");
        }
    }

    private bool IsPlayerCloseEnough()
    {
        return Vector2.Distance(player.position, transform.position) <= interactDistance;
    }

    public virtual void Interact()
    {
        Debug.Log("Interacted with: " + gameObject.name);

        GetComponent<Rigidbody2D>().isKinematic = false;
    }
}
