using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
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
    private Animator animator;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        collider = GetComponentInChildren<Collider2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        sortLayer = sprite.sortingLayerID;
        sortOrder = sprite.sortingOrder;
        animator = GetComponentInChildren<Animator>();
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
        switch (actionName)
        {
            case "ball":
            case "trophy":
            case "duster":
            case "dust":
                
                GetComponent<Rigidbody2D>().isKinematic = false;

                break;
            case "cupboardDoor":
                gameObject.SetActive(false);
                break;
        }
        Debug.Log("Interacted with: " + gameObject.name);

    }

    public void DogInteract(DogClickController dog)
    {
        switch (actionName)
        {
            case "ball":
                case "trophy":
                case "duster":
                
                if (dog.holdingItem)
                {
                    dog.holdingItem.DogDrop(dog);
                }
            
            
                transform.rotation = quaternion.identity;
                transform.parent = dog.transform;
                transform.position = dog.mouth.position;
                GetComponent<Rigidbody2D>().isKinematic = true;
                GetComponentInChildren<Collider2D>().enabled = false;
                sprite.sortingLayerName = "Dog";
                sprite.sortingOrder = 1;
                
                dog.holdingItem = this;
                break;
            case "dust":
                if (dog.holdingItem && dog.holdingItem.name == "duster")
                {
                    gameObject.SetActive(false);
                    dog.holdingItem.animator.SetTrigger("Use");
                    dog.holdingItem.GetComponentInChildren<HumanRequest>().deliverItem();
                }

                break;
            // case "human":
            //     if (GetComponent<HumanRequest>().isCorrectDelivery(dog.holdingItem.name))
            //     {
            //         
            //     }
            //     else
            //     {
            //         GetComponent<HumanRequest>().wrongDeliver();
            //     }
            //     break;
        }

        if (dog.holdingItem && GetComponent<HumanRequest>())
        {
            if (GetComponent<HumanRequest>().isCorrectDelivery(dog.holdingItem.name))
            {
                GetComponent<HumanRequest>().deliverItem();
                dog.holdingItem.gameObject.SetActive(false);
                dog.holdingItem = null;
            }
            else
            {
                GetComponent<HumanRequest>().wrongDeliver();
            }
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
