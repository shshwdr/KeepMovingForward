using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float interactDistance = 2f; // Max distance for interaction
    private Transform player; // Reference to the player
    public Collider2D collider;
    public string name;
    public SpriteRenderer sprite;

    public int sortLayer;
    public int sortOrder;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        collider = GetComponentInChildren<Collider2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        sortLayer = sprite.sortingLayerID;
        sortOrder = sprite.sortingOrder;
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
            sprite.sortingLayerName = "Dog";
            sprite.sortingOrder = 1;
        }
    }

    public void DogDrop()
    {
        
    }
}
