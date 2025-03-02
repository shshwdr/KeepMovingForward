using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float interactDistance = 2f; // Max distance for interaction
    private Transform player; // Reference to the player
    public Collider2D collider;
    public string name;
    public string actionName;
    public string commentName;
    public SpriteRenderer sprite;
    public GameObject outline;
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
        if (actionName == "ball")
        {
            
            GetComponent<Rigidbody2D>().isKinematic = false;
        }
        Debug.Log("Interacted with: " + gameObject.name);

    }

    public void DogInteract(DogClickController dog)
    {
        switch (actionName)
        {
            case "ball":
                
                if (dog.holdingItem)
                {
                    dog.holdingItem.DogDrop(dog);
                }
            
            
                transform.parent = dog.transform;
                transform.position = dog.transform.position;
                GetComponent<Rigidbody2D>().isKinematic = true;
                GetComponentInChildren<Collider2D>().enabled = false;
                sprite.sortingLayerName = "Dog";
                sprite.sortingOrder = 1;
                dog.holdingItem = this;
                break;
            case "human":
                if (dog.holdingItem && dog.holdingItem.name == "correctCD")
                {
                    dog.holdingItem.gameObject.SetActive(false);
                    //dog.holdingItem.transform.position = transform.position;
                    //dog.holdingItem.transform.parent = transform;
                    if (GetComponent<HumanRequest>())
                    {
                        GetComponent<HumanRequest>().finishRequest();
                    }
                    
                    dog.holdingItem = null;
                }
                else
                {
                    GetComponent<HumanRequest>().wrongDeliver();
                }
                break;
        }

    }

    public void DogDrop(DogClickController dog)
    {
        GetComponent<Rigidbody2D>().isKinematic = false;
        GetComponentInChildren<Collider2D>().enabled = true;
        sprite.sortingLayerID = sortLayer;
        sprite.sortingOrder = sortOrder;
        transform.parent = null;
        dog.holdingItem = null;
    }
}
