using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float interactDistance = 2f; // Max distance for interaction
    private Transform player; // Reference to the player

    public string name;
    
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
        if (name == "ball")
        {
            
            GetComponent<Rigidbody2D>().isKinematic = false;
        }
        Debug.Log("Interacted with: " + gameObject.name);

    }

    public void DogInteract(DogController dog)
    { 
        if (name == "ball")
        {
            
            transform.parent = dog.transform;
            transform.position = dog.transform.position;
            GetComponent<Rigidbody2D>().isKinematic = true;
            GetComponentInChildren<Collider2D>().enabled = false;
        }
    }
}
